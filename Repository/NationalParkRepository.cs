using ParkyAPI.Models;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            db.NationalPark.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            db.NationalPark.Remove(nationalPark);
            return Save();
        }

        public ICollection<NationalPark> GetAllNationalParks()
        {
            return db.NationalPark.OrderBy(p => p.Name).ToList();
        }

        public NationalPark GetNationalPark(int id)
        {
            return db.NationalPark.FirstOrDefault(p => p.Id == id);
        }

        public bool NationalParkExists(string name)
        {
            bool exists = db.NationalPark.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
            return exists;
        }

        public bool NationalParkExists(int id)
        {
            return db.NationalPark.Any(p => p.Id == id);
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            db.NationalPark.Update(nationalPark);
            return Save();
        }
    }
}
