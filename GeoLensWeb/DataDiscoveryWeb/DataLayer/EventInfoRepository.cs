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
    public class EventInfoRepository : IEventInfoRepository
    {
        private readonly DiscoveryEventEntities _context;

        public EventInfoRepository(DiscoveryEventEntities context)
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
        
        public async Task<List<EventInfoViewModel>> Index(string search, string countryID, string eventTypeID, string dateFrom, string dateTo, CancellationToken ct = default(CancellationToken))
        {
            int? countryIDInt = FunServices.contvertToInt(countryID);
            int? eventTypeInt = FunServices.contvertToInt(eventTypeID);

            DateTime? dateFromDate = FunServices.contvertToDate(dateFrom);
            DateTime? dateToDate = FunServices.contvertToDate(dateTo);

            List<EventInfoViewModel> li = null;
            li = await _context.EventInfo.Where(

                s => s.statusID == 1
                && (s.eventNameAr.Contains(search) || s.eventNameEn.Contains(search) || string.IsNullOrEmpty(search))
                && (s.countryID == countryIDInt || countryIDInt == null)
                && (s.eventTypeID == eventTypeInt || eventTypeInt == null)
                && ((s.eventDate >= dateFromDate && s.eventDate <= dateToDate) || dateFromDate == null || dateToDate == null)

                ).Select(s => new EventInfoViewModel()
            {

                eventID = s.eventID,
                eventNameEn = s.eventNameEn,
                eventNameAr = s.eventNameAr,
                eventDescEn = s.eventDescEn,
                eventDescAr = s.eventDescAr,
                latitude = s.latitude,
                longitude = s.longitude,
                eventTypeID = s.eventTypeID,
                countryID = s.countryID,
                eventDate = s.eventDate,
                userID = s.userID,dateAdd = s.dateAdd,
                statusID = s.statusID,eventImage = s.eventImage,
                 EventTypes = new EventTypesViewModel()
                 {
                     eventTypeNameAr = s.EventTypes.eventTypeNameAr ,
                     eventTypeNameEn = s.EventTypes.eventTypeNameEn 
                 } ,
                 Countries = new CountriesViewModel()
                 {
                     countryNameAr = s.Countries.countryNameAr ,
                     countryNameEn = s.Countries.countryNameEn
                 }
            }).OrderByDescending(s=> s.dateAdd).ToListAsync<EventInfoViewModel>(ct);

            return li;
        }
        public async Task<List<EventInfoViewModel>> GetEvent(string number, string eventTypeID,string countryID, string dateFrom, string dateTo, CancellationToken ct = default(CancellationToken))
        {
            int? countryIDInt = FunServices.contvertToInt(countryID);
            int? eventTypeInt = FunServices.contvertToInt(eventTypeID);

            DateTime? dateFromDate = FunServices.contvertToDate(dateFrom);
            DateTime? dateToDate = FunServices.contvertToDate(dateTo);

            List<EventInfoViewModel> li = null;
           if(number == "all")
            {
                li = await _context.EventInfo.Where(

               s => s.statusID == 1
               && (s.countryID == countryIDInt || countryIDInt == null)
               && (s.eventTypeID == eventTypeInt || eventTypeInt == null)
               && ((s.eventDate >= dateFromDate && s.eventDate <= dateToDate) || dateFromDate == null || dateToDate == null)

               ).Select(s => new EventInfoViewModel()
               {

                   eventID = s.eventID,
                   eventNameEn = s.eventNameEn,
                   eventNameAr = s.eventNameAr,
                   eventDescEn = s.eventDescEn,
                   eventDescAr = s.eventDescAr,
                   latitude = s.latitude,
                   longitude = s.longitude,
                   eventTypeID = s.eventTypeID,
                   countryID = s.countryID,
                   eventDate = s.eventDate,
                   userID = s.userID,
                   statusID = s.statusID,eventImage= s.eventImage,dateAdd= s.dateAdd,
                   EventTypes = new EventTypesViewModel()
                   {
                       eventTypeNameAr = s.EventTypes.eventTypeNameAr,
                       eventTypeNameEn = s.EventTypes.eventTypeNameEn
                   },
                   Countries = new CountriesViewModel()
                   {
                       countryNameAr = s.Countries.countryNameAr,
                       countryNameEn = s.Countries.countryNameEn
                   }
               }).OrderByDescending(s=> s.eventDate).ToListAsync<EventInfoViewModel>(ct);
            }
           else
            {
                li = await _context.EventInfo.Where(

               s => s.statusID == 1
               && (s.countryID == countryIDInt || countryIDInt == null)
               && (s.eventTypeID == eventTypeInt || eventTypeInt == null)
               && ((s.eventDate >= dateFromDate && s.eventDate <= dateToDate) || dateFromDate == null || dateToDate == null)

               ).Select(s => new EventInfoViewModel()
               {

                   eventID = s.eventID,
                   eventNameEn = s.eventNameEn,
                   eventNameAr = s.eventNameAr,
                   eventDescEn = s.eventDescEn,
                   eventDescAr = s.eventDescAr,
                   latitude = s.latitude,
                   longitude = s.longitude,
                   eventTypeID = s.eventTypeID,
                   countryID = s.countryID,
                   eventDate = s.eventDate,
                   userID = s.userID,
                   statusID = s.statusID,eventImage= s.eventImage,dateAdd = s.dateAdd,
                   EventTypes = new EventTypesViewModel()
                   {
                       eventTypeNameAr = s.EventTypes.eventTypeNameAr,
                       eventTypeNameEn = s.EventTypes.eventTypeNameEn
                   },
                   Countries = new CountriesViewModel()
                   {
                       countryNameAr = s.Countries.countryNameAr,
                       countryNameEn = s.Countries.countryNameEn
                   }
               }).OrderByDescending(s=> s.eventDate).Take(10).ToListAsync<EventInfoViewModel>(ct);
            }
            return li;
        }

        public async Task<List<EventInfoViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<EventInfoViewModel> li = null;
            li = await _context.EventInfo.Where(s => s.statusID == 1).Select(s => new EventInfoViewModel()
            {
                eventID = s.eventID,
                eventNameEn = s.eventNameEn,
                eventNameAr = s.eventNameAr,
                eventDescEn = s.eventDescEn,
                eventDescAr = s.eventDescAr,
                latitude = s.latitude,
                longitude = s.longitude,
                eventTypeID = s.eventTypeID,
                countryID = s.countryID,
                eventDate = s.eventDate,
                userID = s.userID,
                statusID = s.statusID,eventImage = s.eventImage,

            }).ToListAsync<EventInfoViewModel>(ct);

            return li;
        }

        public async Task<EventInfo> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.EventInfo.FirstOrDefaultAsync(x => x.eventID == id);
        }

        public async Task<EventInfo> AddAsync(EventInfo obj, CancellationToken ct = default(CancellationToken))
        {
            _context.EventInfo.Add(obj);
            await _context.SaveChangesAsync(ct);
            return obj;
        }

        public async Task<bool> UpdateAsync(EventInfo obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.eventID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.eventID);
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
            var toRemove = await _context.EventInfo.FirstOrDefaultAsync(x => x.eventID == id);
            _context.EventInfo.Remove(toRemove);
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
