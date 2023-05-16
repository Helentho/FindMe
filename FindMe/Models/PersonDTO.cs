namespace FindMe.Models
{
    public class PersonDTO
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public int? TypeOfPerson { get; set; }
        public int? CityId { get; set; }
        public int? Street { get; set; }
        public string? House { get; set; }
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? SecondPhoneNumber { get; set; }

        public int CurrentUserId { get; set; }
    }
}
