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
    public class EventTypesRepository : IEventTypesRepository
    {
        private readonly DiscoveryEventEntities _context;

        public EventTypesRepository(DiscoveryEventEntities context)
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

        public async Task<List<EventTypesViewModel>> Index(string search, CancellationToken ct = default(CancellationToken))
        {


            List<EventTypesViewModel> li = null;
            li = await _context.EventTypes.Where(

                s => s.statusID == 1
                && (s.eventTypeNameAr.Contains(search) || s.eventTypeNameEn.Contains(search) || string.IsNullOrEmpty(search))

                ).Select(s => new EventTypesViewModel()
            {

                eventTypeID = s.eventTypeID,
                eventTypeNameEn = s.eventTypeNameEn,
                eventTypeNameAr = s.eventTypeNameAr,
                statusID = s.statusID,

            }).ToListAsync<EventTypesViewModel>(ct);

            return li;
        }

        public async Task<List<EventTypesViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<EventTypesViewModel> li = null;
            li = await _context.EventTypes.Where(s => s.statusID == 1).Select(s => new EventTypesViewModel()
            {
                eventTypeID = s.eventTypeID,
                eventTypeNameEn = s.eventTypeNameEn,
                eventTypeNameAr = s.eventTypeNameAr,
                statusID = s.statusID,

            }).ToListAsync<EventTypesViewModel>(ct);

            return li;
        }

        public async Task<EventTypes> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.EventTypes.FirstOrDefaultAsync(x => x.eventTypeID == id);
        }

        public async Task<EventTypes> AddAsync(EventTypes obj, CancellationToken ct = default(CancellationToken))
        {
            _context.EventTypes.Add(obj);
            await _context.SaveChangesAsync(ct);
            return obj;
        }

        public async Task<bool> UpdateAsync(EventTypes obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.eventTypeID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.eventTypeID);
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
            var toRemove = await _context.EventTypes.FirstOrDefaultAsync(x => x.eventTypeID == id);
            _context.EventTypes.Remove(toRemove);
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
