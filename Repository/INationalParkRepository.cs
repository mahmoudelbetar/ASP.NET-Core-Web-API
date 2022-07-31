using ParkyAPI.Models;

namespace ParkyAPI.Repository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetAllNationalParks();
        NationalPark GetNationalPark(int id);
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();
    }
}
