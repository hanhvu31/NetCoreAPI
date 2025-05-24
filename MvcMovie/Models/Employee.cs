namespace MvcMovie.Models
{
    public class Employee : Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public string Email { get; set; } 
        public DateTime HireDate { get; set; }
        public int EmployeeId { get; set; } = 0;
        public string ChucVu { get; set; } = "";
    }
}