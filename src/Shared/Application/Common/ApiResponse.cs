namespace Mcm.Shared.Application.Common
{
    public class ApiResponse<T>
    {
        public bool? Success { get; set; }
        public T? Data { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
        public Meta? Meta { get; set; }
    }
}