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
    public interface IEventImageRepository : IDisposable
    {
        Task<List<EventImageViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<EventImage> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<EventImage> AddAsync(EventImage obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(EventImage obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<EventImageViewModel>> Index(string eventID , CancellationToken ct = default(CancellationToken));
    }
}
