﻿namespace FindMe.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}