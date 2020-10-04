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
    public class EventRefrencesRepository : IEventRefrencesRepository
    {
        private readonly DiscoveryEventEntities _context;

        public EventRefrencesRepository(DiscoveryEventEntities context)
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

        public async Task<List<EventRefrencesViewModel>> Index(string eventID, CancellationToken ct = default(CancellationToken))
        {
            int? fieldNameInt = FunServices.contvertToInt(eventID);
            
            List<EventRefrencesViewModel> li = null;
            li = await _context.EventRefrences.Where(

                s => s.statusID == 1 && s.eventID == fieldNameInt

                ).Select(s => new EventRefrencesViewModel()
            {

                eventRefrencesID = s.eventRefrencesID,
                orderID = s.orderID,
                eventURL = s.eventURL,
                eventID = s.eventID,
                statusID = s.statusID,

            }).OrderBy(s=> s.orderID).ToListAsync<EventRefrencesViewModel>(ct);

            return li;
        }

        public async Task<List<EventRefrencesViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<EventRefrencesViewModel> li = null;
            li = await _context.EventRefrences.Where(s => s.statusID == 1).Select(s => new EventRefrencesViewModel()
            {
                eventRefrencesID = s.eventRefrencesID,
                orderID = s.orderID,
                eventURL = s.eventURL,
                eventID = s.eventID,
                statusID = s.statusID,

            }).ToListAsync<EventRefrencesViewModel>(ct);

            return li;
        }

        public async Task<EventRefrences> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.EventRefrences.FirstOrDefaultAsync(x => x.eventRefrencesID == id);
        }

        public async Task<EventRefrences> AddAsync(EventRefrences obj, CancellationToken ct = default(CancellationToken))
        {
            _context.EventRefrences.Add(obj);
            await _context.SaveChangesAsync(ct);
            return obj;
        }

        public async Task<bool> UpdateAsync(EventRefrences obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.eventRefrencesID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.eventRefrencesID);
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
            var toRemove = await _context.EventRefrences.FirstOrDefaultAsync(x => x.eventRefrencesID == id);
            _context.EventRefrences.Remove(toRemove);
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
