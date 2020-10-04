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
    public interface IAboutUsRepository : IDisposable
    {
        Task<List<AboutUsViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<AboutUs> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<AboutUs> AddAsync(AboutUs obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(AboutUs obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<AboutUsViewModel>> Index(string search, CancellationToken ct = default(CancellationToken));
    }
}
