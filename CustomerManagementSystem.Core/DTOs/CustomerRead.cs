namespace CustomerManagementSystem.Core.DTOs
{
    public class CustomerRead
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? Telephone { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
