using System;
using System.Net;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;

namespace Gym_API.Services
{
	public class StatusService: IStatusService
	{
		private ApplicationDbContext _db;

        public StatusService()
        {
        }

		public StatusService(ApplicationDbContext db)
		{
			this._db = db;
		}

        public Status AddStatus(StatusDto data)
        {
            var status = new Status(data.Name);
            _db.Statuses.Add(status);
            _db.SaveChanges();

            return status;
        }

        public async Task<Status> GetStatus(string Id)
        {
            var status = await _db.Statuses.FindAsync(Id);
            if (status == null)
            {
                throw new HttpRequestException($"Status Id {Id} not found",null, HttpStatusCode.NotFound);
            }
            return status;
        }

        public List<Status> GetStatuses()
        {
            return _db.Statuses.ToList();
        }

        public Response DeleteStatus(string id)
        {
            var existingStatus = _db.Statuses.Find(id);
            if(existingStatus != null)
            {
                _db.Statuses.Remove(existingStatus);
                _db.SaveChanges();
                return new Response { Message = "Successfully Deleted", Status = "Success" };
            }
            throw new HttpRequestException($"Status Id {id} not found", null, HttpStatusCode.NotFound);

        }
        public Status UpdateStatus(string id, StatusDto data)
        {
            var existingStatus = _db.Statuses.Find(id);
            if (existingStatus != null)
            {
                existingStatus.Name = data.Name;
                _db.SaveChanges();
                return existingStatus;
            }
            throw new HttpRequestException($"Status Id {id} not found", null, HttpStatusCode.NotFound);
        }

    }
}

