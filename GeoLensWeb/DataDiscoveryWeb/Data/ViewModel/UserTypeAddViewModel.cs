using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList; 
namespace Data.ViewModel
{
    public class UserTypeAddViewModel
    {
        public int UserTypeID { get; set; }
        public UserType UserType { get; set; }
        public IPagedList<UserTypeViewModel> UserTypeAll { get; set; }

    }
    
}
