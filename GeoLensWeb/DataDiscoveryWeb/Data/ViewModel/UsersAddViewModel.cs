using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 
namespace Data.ViewModel
{
    public class UsersAddViewModel
    {
        public int UserID { get; set; }
        public Users Users { get; set; }
        public IPagedList<UsersViewModel> UsersAll { get; set; }

    }
    
}
