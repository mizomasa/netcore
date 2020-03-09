using System;
using DBFirstApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace DBFirstApp.Service
{
    public interface IHumamRepository
    {
        Task<int> FindMaxIdAsync();
    }

    public class HumanReposiotry : MydatabaseContext, IHumamRepository
    {
        private readonly MydatabaseContext _context;
        public HumanReposiotry(DbContextOptions<MydatabaseContext> options)
            : base(options)
        {

        }
        public async Task<int> FindMaxIdAsync()
        {
            return await this.THuman.MaxAsync(e=>e.Id);
        }
    }
}
