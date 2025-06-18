namespace TechApi.Models
{
    public class CustomerRequestDto
    {
        public int CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}
