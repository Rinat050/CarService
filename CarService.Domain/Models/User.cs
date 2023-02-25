﻿using CarService.Domain.Enums;

namespace CarService.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Roles Role { get; set; }
    }
}
