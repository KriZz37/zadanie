using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zadanie.Services;
using zadanie.ViewModels;

namespace zadanie.Controllers
{
    public class FormController : Controller
    {
        private readonly FormService formService;

        public FormController(FormService formService)
        {
            this.formService = formService;
        }

        [HttpGet]
        public IActionResult Edit(long id, bool? instantEdit)
        {
            try
            {
                var vm = formService.GetToEdit(id);

                if (instantEdit == true)
                {
                    ViewBag.instantEdit = true;
                }

                return View(vm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(EditFormViewModel data)
        {
            formService.Edit(data);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Create(NewFormViewModel data)
        {
            var errorId = "NewForm";

            if (data.ParentId == -1)
            {
                return RedirectToAction("Index", "Home", new { errorId, errorMsg = "Wybierz rodzica" });
            }

            try
            {
                var newFormId = formService.Create(data);
                return RedirectToAction("Edit", new { id = newFormId, instantEdit = true });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home", new { errorId, errorMsg = e.Message });
            }
        }
    }
}
