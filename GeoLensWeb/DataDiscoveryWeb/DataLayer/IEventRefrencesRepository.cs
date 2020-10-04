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
    public interface IEventRefrencesRepository : IDisposable
    {
        Task<List<EventRefrencesViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<EventRefrences> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<EventRefrences> AddAsync(EventRefrences obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(EventRefrences obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<EventRefrencesViewModel>> Index(string eventID , CancellationToken ct = default(CancellationToken));
    }
}
