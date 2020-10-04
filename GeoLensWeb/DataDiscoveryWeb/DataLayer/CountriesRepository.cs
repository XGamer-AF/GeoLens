using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using Data.ViewModel;

namespace DataLayer
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly DiscoveryEventEntities _context;

        public CountriesRepository(DiscoveryEventEntities context)
        {
            _context = context;
            _context.Configuration.LazyLoadingEnabled = false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task<bool> IfExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIDAsync(id, ct) != null;
        }

        public async Task<List<CountriesViewModel>> Index(string search, CancellationToken ct = default(CancellationToken))
        {
         
            
            List<CountriesViewModel> li = null;
            li = await _context.Countries.Where(

                s => s.statusID == 1 
                &&(s.countryNameAr.Contains(search) || s.countryNameEn.Contains(search) || string.IsNullOrEmpty(search))

                ).Select(s => new CountriesViewModel()
            {

                countryID = s.countryID,
                countryNameEn = s.countryNameEn,
                countryNameAr = s.countryNameAr,
                orderID = s.orderID,
                statusID = s.statusID,

            }).ToListAsync<CountriesViewModel>(ct);

            return li;
        }

        public async Task<List<CountriesViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<CountriesViewModel> li = null;
            li = await _context.Countries.Where(s => s.statusID == 1).Select(s => new CountriesViewModel()
            {
                countryID = s.countryID,
                countryNameEn = s.countryNameEn,
                countryNameAr = s.countryNameAr,
                orderID = s.orderID,
                statusID = s.statusID,

            }).ToListAsync<CountriesViewModel>(ct);

            return li;
        }

        public async Task<Countries> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.countryID == id);
        }

        public async Task<Countries> AddAsync(Countries obj, CancellationToken ct = default(CancellationToken))
        {
            _context.Countries.Add(obj);
            await _context.SaveChangesAsync(ct);
            return obj;
        }

        public async Task<bool> UpdateAsync(Countries obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.countryID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.countryID);
            _context.Entry(oldObj).CurrentValues.SetValues(obj);

            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(id, ct))
            {
                return false;
            }
            var toRemove = await _context.Countries.FirstOrDefaultAsync(x => x.countryID == id);
            _context.Countries.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken))
        {
            var obj = await GetByIDAsync(id);
            if (obj != null)
            {
                obj.statusID = statusID;
                if (await UpdateAsync(obj))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
