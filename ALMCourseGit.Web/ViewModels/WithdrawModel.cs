using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALMCourseGit.Web.ViewModels
{
    public class WithdrawModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
