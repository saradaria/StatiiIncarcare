using StatiiIncarcare.Models.DB;

namespace StatiiIncarcare.Repositories
{
    public interface IStatiiRepository
    {
        public Task Create(Statii statie);
        public Task Delete(Statii statie);

    }

    public class StatiiRepository : IStatiiRepository
    {
        private IncarcareStatiiContext _IncarcareStatiiContext;

        public StatiiRepository(IncarcareStatiiContext context)
        {
            _IncarcareStatiiContext = context;
        }
        public async Task Create(Statii statie)
        {
            _IncarcareStatiiContext.Statiis.Add(statie);
            await _IncarcareStatiiContext.SaveChangesAsync();
        }

        public async Task Delete(Statii statie)
        {
            _IncarcareStatiiContext.Statiis.Remove(statie);
            await _IncarcareStatiiContext.SaveChangesAsync();
        }

    }
}
