using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMCourseGit.Web.Database.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
