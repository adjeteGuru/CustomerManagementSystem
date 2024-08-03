namespace CustomerManagementSystem.Core.DTOs
{
    public class CustomerUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? Telephone { get; set; }      
        public string? DepartmentName { get; set; }
    }
}
