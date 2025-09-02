using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SindautoHub.Domain;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;
using SindautoHub.Infrastructure.Persistance.Database;
using Supabase.Interfaces;

namespace SindautoHub.Infrastructure.Persistance.Repository
{
    public class CargosRepository : ICargoRepository
    {

        private readonly SindautoHubContext _context;



        public CargosRepository(SindautoHubContext context)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Cargo> CreateAsync(Cargo cargo)
        {
            await _context.Set<Cargo>().AddAsync(cargo);
            return new Cargo();
        }

        public async Task<bool> DeleteAsync(Guid CargoId)
        {
            var cargoToDelete = await _context.Cargos.FindAsync(CargoId);
            if (cargoToDelete is null)
            {
                return false;
            }

            _context.Cargos.Remove(cargoToDelete);
            return true;
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync(Guid  CargoId)
        {
            return await _context.Set<Cargo>().ToListAsync();
        }

        public async Task<Cargo> GetByIdAsync(Guid CargoId)
        {
            return await _context.Cargos.FindAsync(CargoId);
        }

        public Task<Cargo> UpdateAsync(Guid CargoId)
        {
            _context.Update(CargoId);
            return (Task<Cargo>)Task.CompletedTask;
        }
    }
}