using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMCourseGit.Web.Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ALMCourseGit.Web.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Withdraw(string id, string amount)
        {
            TempData["Status"] = BankRepository.Withdraw(id, amount);
            return View("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Deposit(string id, string amount)
        {
            TempData["Status"] = BankRepository.Deposit(id, amount);
            return View("Index");
        }
    }
}
