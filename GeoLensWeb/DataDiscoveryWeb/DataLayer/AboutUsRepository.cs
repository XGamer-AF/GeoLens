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
    public class AboutUsRepository : IAboutUsRepository
    {
        private readonly DiscoveryEventEntities _context;

        public AboutUsRepository(DiscoveryEventEntities context)
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

        public async Task<List<AboutUsViewModel>> Index(string search, CancellationToken ct = default(CancellationToken))
        {
            
            List<AboutUsViewModel> li = null;
            li = await _context.AboutUs.Where(

                s => s.statusID == 1 
                && (s.aboutUsDescAr.Contains(search) || s.aboutUsDescEn.Contains(search) || string.IsNullOrEmpty(search))

                ).Select(s => new AboutUsViewModel()
            {

                aboutUsID = s.aboutUsID,
                aboutUsDescEn = s.aboutUsDescEn,
                aboutUsDescAr = s.aboutUsDescAr,
                statusID = s.statusID,

            }).ToListAsync<AboutUsViewModel>(ct);

            return li;
        }

        public async Task<List<AboutUsViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<AboutUsViewModel> li = null;
            li = await _context.AboutUs.Where(s => s.statusID == 1).Select(s => new AboutUsViewModel()
            {
                aboutUsID = s.aboutUsID,
                aboutUsDescEn = s.aboutUsDescEn,
                aboutUsDescAr = s.aboutUsDescAr,
                statusID = s.statusID,

            }).ToListAsync<AboutUsViewModel>(ct);

            return li;
        }

        public async Task<AboutUs> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.AboutUs.FirstOrDefaultAsync(x => x.aboutUsID == id);
        }

        public async Task<AboutUs> AddAsync(AboutUs obj, CancellationToken ct = default(CancellationToken))
        {
            _context.AboutUs.Add(obj);
            await _context.SaveChangesAsync(ct);
            return obj;
        }

        public async Task<bool> UpdateAsync(AboutUs obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.aboutUsID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.aboutUsID);
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
            var toRemove = await _context.AboutUs.FirstOrDefaultAsync(x => x.aboutUsID == id);
            _context.AboutUs.Remove(toRemove);
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
