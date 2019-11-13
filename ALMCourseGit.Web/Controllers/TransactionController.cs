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
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View(new TransactionViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Withdraw(WithdrawModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Status"] = BankRepository.Withdraw(model.Id.ToString(), model.Amount.ToString());
                return View("Index");
            }
            TempData["Status"] = "One or more fields are empty.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Deposit(DepositModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Status"] = BankRepository.Deposit(model.Id.ToString(), model.Amount.ToString());
                return View("Index");
            }
            TempData["Status"] = "One or more fields are empty.";
            return RedirectToAction("Index");
        }
    }
}
