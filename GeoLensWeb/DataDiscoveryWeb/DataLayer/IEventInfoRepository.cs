using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data ;
using System.Threading;
using Data.ViewModel;

namespace DataLayer
{
    public interface IEventInfoRepository : IDisposable
    {
        Task<List<EventInfoViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<EventInfo> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<EventInfo> AddAsync(EventInfo obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(EventInfo obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<EventInfoViewModel>> Index(string search, string countryID, string eventTypeID, string dateFrom, string dateTo, CancellationToken ct = default(CancellationToken));
        Task<List<EventInfoViewModel>> GetEvent(string number, string eventTypeID, string countryID, string dateFrom, string dateTo, CancellationToken ct = default(CancellationToken));

    }
}
