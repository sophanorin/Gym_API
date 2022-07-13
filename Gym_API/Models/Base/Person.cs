using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_API.Models.Base
{
	public abstract class Person
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname {
            get {
                return Firstname + " " + Lastname;
            }
        }

        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual string GenderId { get; set; }

        public virtual Gender Gender { get; set; }
        public User UserCredential { get; set; }
    }
}

