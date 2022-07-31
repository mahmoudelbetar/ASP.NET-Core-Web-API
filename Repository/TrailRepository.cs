using Microsoft.EntityFrameworkCore;
using ParkyAPI.Models;

namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext db;

        public TrailRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool CreateTrail(Trail trail)
        {
            db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            db.Trails.Remove(trail);
            return Save();
        }

        public ICollection<Trail> GetAllTrails()
        {
            return db.Trails.Include(t => t.NationalPark).OrderBy(p => p.Name).ToList();
        }

        public Trail GetTrail(int id)
        {
            return db.Trails.Include(t => t.NationalPark).FirstOrDefault(p => p.Id == id);
        }

        public bool TrailExists(string name)
        {
            bool exists = db.Trails.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
            return exists;
        }

        public bool TrailExists(int id)
        {
            return db.Trails.Any(p => p.Id == id);
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail trail)
        {
            db.Trails.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int npId)
        {
            return db.Trails.Include(t => t.NationalPark).Where(t => t.NationalParkId == npId).ToList();
        }
    }
}
