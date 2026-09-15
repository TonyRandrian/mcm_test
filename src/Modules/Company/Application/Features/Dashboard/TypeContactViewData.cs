namespace Mcm.Company.Application.Features.Dashboard
{
    public class TypeContactViewData
    {
        public Guid TypeContactId { get; set; }
        public string TypeContactName { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        public double Pourcent { get; set; } = 0;
    }
}