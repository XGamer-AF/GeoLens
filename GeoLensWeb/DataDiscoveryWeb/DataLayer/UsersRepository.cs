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
    public class UsersRepository : IUsersRepository
    {
        private readonly DiscoveryEventEntities _context;

        public UsersRepository(DiscoveryEventEntities context)
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

        public async Task<List<UsersViewModel>> Index(string search, CancellationToken ct = default(CancellationToken))
        {
            
            List<UsersViewModel> li = null;
            li = await _context.Users.Where(

                s => s.statusID == 1 

                ).Select(s => new UsersViewModel()
            {

                userID = s.userID,
                userNameEn = s.userNameEn,
                userNameAR = s.userNameAR,
                userTel = s.userTel,
                userEmail = s.userEmail,
                userTypeID = s.userTypeID,
                statusID = s.statusID,

            }).ToListAsync<UsersViewModel>(ct);

            return li;
        }

        public async Task<List<UsersViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            List<UsersViewModel> li = null;
            li = await _context.Users.Where(s => s.statusID == 1).Select(s => new UsersViewModel()
            {
                userID = s.userID,
                userNameEn = s.userNameEn,
                userNameAR = s.userNameAR,
                userTel = s.userTel,
                userEmail = s.userEmail,
                userTypeID = s.userTypeID,
                statusID = s.statusID,

            }).ToListAsync<UsersViewModel>(ct);

            return li;
        }

        public async Task<Users> GetByIDAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.userID == id);
        }

        public async Task<Users> AddAsync(Users obj, CancellationToken ct = default(CancellationToken))
        {
            _context.Users.Add(obj);
            await _context.SaveChangesAsync(ct);
            return obj;
        }

        public async Task<bool> UpdateAsync(Users obj, CancellationToken ct = default(CancellationToken))
        {
            if (!await IfExists(obj.userID, ct))
            {
                return false;
            }
            var oldObj = await GetByIDAsync(obj.userID);
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
            var toRemove = await _context.Users.FirstOrDefaultAsync(x => x.userID == id);
            _context.Users.Remove(toRemove);
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
