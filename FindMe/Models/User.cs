using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMe.Models
{
    public class User
    {
        public int Id { get; set; }
        //[EmailAddress]
        public string? Email { get; set; }
        //[DataType(DataType.Password)]
        public string Password { get; set; }
        //[Phone]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<Person>? People { get; set; }
    }
}
