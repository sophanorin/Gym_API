using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_API.Models.Base
{
	public abstract class Person
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Fullname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string GenderId { get; set; }

        public Gender Gender { get; set; }
        public User UserCredential { get; set; }
    }
}

