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
    public class EventImageRepository : IEventImageRepository
    {
        private readonly DiscoveryEventEntities _context;

        public EventImageRepository(DiscoveryEventEntities context)
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

        public async Task<List<EventImageViewModel>> Index(string eventID, CancellationToken ct = default(CancellationToken))
        {
            int? eventIDInt = FunServices.contvertToInt(eventID);
            List<EventImageViewModel> li = null;
            li = await _context.EventImage.Where(

                s => s.statusID == 1 
                && (s.eventID == eventIDInt )

                ).Select(s => new EventImageViewModel()
            {

                eventImageID = s.eventImageID,
                eventID = s.eventID,
                statusID = s.statusID,

            }).ToListAsync<EventImageViewModel>(ct);

            return li;
        }

        public async Task<List<EventImageViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<EventImageViewModel> li = null;
            li = await _context.EventImage.Where(s => s.statusID == 1).Select(s => new EventImageViewModel()
            {
                eventImageID = s.eventImageID,
                eventID = s.eventID,
                statusID = s.statusID,

            }).ToListAsync<EventImageViewModel>(ct);

            return li;
        }

        public async Task<EventImage> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.EventImage.FirstOrDefaultAsync(x => x.eventImageID == id);
        }

        public async Task<EventImage> AddAsync(EventImage obj, CancellationToken ct = default(CancellationToken))
        {
            _context.EventImage.Add(obj);
            await _context.SaveChangesAsync(ct);
          
            return obj;
        }

        public async Task<bool> UpdateAsync(EventImage obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.eventImageID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.eventImageID);
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
            var toRemove = await _context.EventImage.FirstOrDefaultAsync(x => x.eventImageID == id);
            _context.EventImage.Remove(toRemove);
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
