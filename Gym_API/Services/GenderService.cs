using System;
using System.Net;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;

namespace Gym_API.Services
{
    public class GenderService : IGenderService
    {
        private readonly ApplicationDbContext _db;

        public GenderService()
        {
        }

        public GenderService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public Gender AddGender(GenderDto data)
        {
            var gender = new Gender(data.Name);

            if(data.Id != null)
            {
                gender.Id = data.Id;
            }

            _db.Genders.Add(gender);
            _db.SaveChanges();

            return gender;
        }

        public Response DeleteGender(string id)
        {
            var existingStatus = _db.Genders.Find(id);

            if (existingStatus == null)
            {
                throw new HttpRequestException($"Gender Id {id} not found", null, HttpStatusCode.NotFound);
            }
            
            _db.Genders.Remove(existingStatus);
            _db.SaveChanges();
            return new Response { Message = "Successfully Deleted", Status = "Success" };
        }


        public List<Gender> GetGenders()
        {
            return _db.Genders.ToList();
        }

       
    }
}

