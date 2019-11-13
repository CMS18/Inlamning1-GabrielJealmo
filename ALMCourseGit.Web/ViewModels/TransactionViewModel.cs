using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALMCourseGit.Web.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public DepositModel DepositModel { get; set; }
        [Required]
        public WithdrawModel WithdrawModel { get; set; }
    }
}
