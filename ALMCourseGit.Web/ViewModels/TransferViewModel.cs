using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALMCourseGit.Web.ViewModels
{
    public class TransferViewModel
    {
        [Required]
        public int SenderAccountId { get; set; }
        [Required]
        public int ReceiverAccountId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
