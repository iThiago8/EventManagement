using apis.Dtos.Symposium;
using apis.Interfaces;
using apis.Models;

namespace apis.Repositories
{
    public class SymposiumRepository : ISymposiumRepository
    {
        public Task<Symposium> CreateAsync(Symposium symposiumModel)
        {
            throw new NotImplementedException();
        }

        public Task<Symposium?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Symposium>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Symposium?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Symposium?> UpdateAsync(int id, UpdateSymposiumRequestDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
