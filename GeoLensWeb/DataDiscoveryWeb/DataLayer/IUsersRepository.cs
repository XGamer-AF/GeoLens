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
    public interface IUsersRepository : IDisposable
    {
        Task<List<UsersViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Users> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Users> AddAsync(Users obj, CancellationToken ct = default (CancellationToken));
        Task<bool> UpdateAsync(Users obj, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        Task<bool> ChangeStatusAsync(int id, int statusID, CancellationToken ct = default(CancellationToken));
        Task<List<UsersViewModel>> Index(string search, CancellationToken ct = default(CancellationToken));
    }
}
