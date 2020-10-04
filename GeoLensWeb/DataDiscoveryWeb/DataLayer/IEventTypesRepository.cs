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
    public interface IEventTypesRepository : IDisposable
    {
        Task<List<EventTypesViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<EventTypes> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<EventTypes> AddAsync(EventTypes obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(EventTypes obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<EventTypesViewModel>> Index(string search , CancellationToken ct = default(CancellationToken));
    }
}
