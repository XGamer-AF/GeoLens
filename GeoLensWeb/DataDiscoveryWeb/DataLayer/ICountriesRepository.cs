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
    public interface ICountriesRepository : IDisposable
    {
        Task<List<CountriesViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Countries> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Countries> AddAsync(Countries obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(Countries obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<CountriesViewModel>> Index(string search , CancellationToken ct = default(CancellationToken));
    }
}
