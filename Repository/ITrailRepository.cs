using ParkyAPI.Models;

namespace ParkyAPI.Repository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetAllTrails();
        ICollection<Trail> GetTrailsInNationalPark(int npId);
        Trail GetTrail(int id);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
