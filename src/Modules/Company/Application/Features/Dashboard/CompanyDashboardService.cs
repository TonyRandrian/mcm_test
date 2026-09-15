using System.Globalization;
using System.Reflection.Metadata;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Company.Application.Features.Dashboard
{
    public class CompanyDashboardService(
        ICompanyRepository companyRepository,
        ICurrentUserService currentUserService,
        IHubContext<CompanyHub> hubContext)
        : ICompanyDashboardService
    {
        private readonly IHubContext<CompanyHub> _hubContext = hubContext;
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task SendViewData()
        {
            var types = await _companyRepository.GetDashboardData(_currentUserService.CompanyId);
            var result = new ContactStatsDto
            {
                TypeContacts = types,
                Total = types.Sum(x => x.Count)
            };
            await _hubContext.Clients.Group($"company:{_currentUserService.CompanyId}")
                .SendAsync("ReceiveContactCount", result);
        }

        public async Task SendStats(
            // Guid typeContactId,
            TimeEnum timeEnum,
            DateTime startDate,
            DateTime endDate)
        {
            var companies = await _companyRepository.GetAllAsync(
                predicate: c =>
                    c.IsContact && 
                    c.CompanyId == _currentUserService.CompanyId && 
                    // c.TypeContactId == typeContactId &&
                    c.CreatedAt >= startDate && c.CreatedAt <= endDate    
            );
            var result = timeEnum switch
            {
                TimeEnum.Day   => BuildDayStats(companies, startDate),
                TimeEnum.Week  => BuildWeekStats(companies, startDate),
                TimeEnum.Month => BuildMonthStats(companies, startDate),
                TimeEnum.Year  => BuildYearStats(companies, startDate, endDate),
                _ => BuildDayStats(companies, startDate)
            };

            await _hubContext.Clients.Group($"company:{_currentUserService.CompanyId}").SendAsync("ReceiveContactStats", result);
        }

        private const string DateFormat = "dddd | dd/MM/yyyy | HH:mm";
        private static DateTime StartOfWeek(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.Date.AddDays(-diff);
        }

        private static IEnumerable<object> BuildDayStats(IEnumerable<Domain.Entities.Company> companies, DateTime startDate)
        {
            var counts = companies
                .GroupBy(c => c.CreatedAt.Date)
                .ToDictionary(g => g.Key, g => g.Count());
            foreach (var date in counts.Keys)
            {
                Console.WriteLine($"Date: {date}, Count: {counts[date]}");
            }
            var weekStart = StartOfWeek(startDate);
            return Enumerable.Range(0, 7).Select(i =>
            {
                var date = weekStart.AddDays(i);
                return new
                {
                    Date = date.ToString(DateFormat),
                    Count = counts.TryGetValue(date, out var count) ? count : 0
                };
            });
        }
        
        private static IEnumerable<object> BuildWeekStats(IEnumerable<Domain.Entities.Company> companies, DateTime startDate)
        {
            static int WeekOfMonth(DateTime date) => ((date.Day - 1) / 7) + 1;
            var firstOfMonth = new DateTime(startDate.Year, startDate.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            var weeksInMonth = ((daysInMonth - 1) / 7) + 1;
            var counts = companies
                .GroupBy(c => WeekOfMonth(c.CreatedAt))
                .ToDictionary(g => g.Key, g => g.Count());
            return Enumerable.Range(1, weeksInMonth).Select(week =>
            {
                var weekStartDate = firstOfMonth.AddDays((week - 1) * 7);
                return new
                {
                    Date = weekStartDate.ToString(DateFormat),
                    Count = counts.TryGetValue(week, out var count) ? count : 0
                };
            });
        }

        private static IEnumerable<object> BuildMonthStats(IEnumerable<Domain.Entities.Company> companies, DateTime startDate)
        {
            var counts = companies
                .Where(c => c.CreatedAt.Year == startDate.Year)
                .GroupBy(c => c.CreatedAt.Month)
                .ToDictionary(g => g.Key, g => g.Count());
            return Enumerable.Range(1, 12).Select(month =>
            {
                var date = new DateTime(startDate.Year, month, 1);
                return new
                {
                    Date = date.ToString(DateFormat),
                    Count = counts.TryGetValue(month, out var count) ? count : 0
                };

            });

        }

        private static IEnumerable<object> BuildYearStats(IEnumerable<Domain.Entities.Company> companies, DateTime startDate, DateTime endDate)
        {
            var counts = companies
                .GroupBy(c => c.CreatedAt.Year)
                .ToDictionary(g => g.Key, g => g.Count());
            return Enumerable.Range(startDate.Year, endDate.Year - startDate.Year + 1).Select(year =>
            {
                var date = new DateTime(year, 1, 1);
                return new
                {
                    Date = date.ToString(DateFormat),
                    Count = counts.TryGetValue(year, out var count) ? count : 0
                };
            });
        }

        public class ContactStatsDto
        {
            public List<TypeContactViewData> TypeContacts { get; set; } = [];
            public int Total { get; set; }
        }
    }
}
