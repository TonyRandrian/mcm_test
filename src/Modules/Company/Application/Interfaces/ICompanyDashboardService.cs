namespace Mcm.Company.Application.Interfaces
{
    public interface ICompanyDashboardService
    {
        Task SendViewData();
        Task SendStats(
            // Guid typeContactId,
            TimeEnum timeEnum,
            DateTime startDate,
            DateTime endDate);
    }

    public enum TimeEnum
    {
        Day,
        Week,
        Month,
        Year
    }
}