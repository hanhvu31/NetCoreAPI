public class Person
{
    [Key]
    public string PersonId { get; set; }
    
    [Required]
    public string FullName { get; set; }
    
    public string Address { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public int? Age { get; set; }
}