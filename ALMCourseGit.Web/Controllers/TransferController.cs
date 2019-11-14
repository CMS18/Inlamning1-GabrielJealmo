using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMCourseGit.Web.Database;
using ALMCourseGit.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ALMCourseGit.Web.Controllers
{
    public class TransferController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TempData["Status"] = BankRepository.Transfer(model.SenderAccountId, model.ReceiverAccountId, model.Amount);

            return View();
        }
    }
}
