using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using zadanie.Models;
using zadanie.Services;
using zadanie.ViewModels;

namespace zadanie.Controllers
{
    public class HomeController : Controller
    {
        private readonly TreeService treeService;

        public HomeController(TreeService treeService)
        {
            this.treeService = treeService;
        }

        [HttpGet]
        public IActionResult Index(string errorId, string errorMsg)
        {
            var vm = treeService.GetTree();

            if (errorId != null)
            {
                ModelState.AddModelError(errorId, errorMsg);
                return View(vm);
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult NewFolder(NewFolderViewModel data)
        {
            if (data.ParentId == -1)
            {
                return RedirectToAction("Index", new { errorId = "NewFolder", errorMsg = "Wybierz rodzica" });
            }

            var error = treeService.AddNewFolder(data);

            if (error != null)
            {
                return RedirectToAction("Index", new { errorId = "NewFolder", errorMsg = error });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MoveFolder(MoveFolderViewModel data)
        {
            if (data.FolderId == -1 || data.NewParentId == -1)
            {
                return RedirectToAction("Index", new { errorId = "MoveFolder", errorMsg = "Wybierz foldery" });
            }

            var error = treeService.MoveFolder(data);

            if (error != null)
            {
                return RedirectToAction("Index", new { errorId = "MoveFolder", errorMsg = error });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeFileName(ChangeFileNameViewModel data)
        {
            if (data.FileId == -1)
            {
                return RedirectToAction("Index", new { errorId = "ChangeName", errorMsg = "Wybierz plik" });
            }

            var error = treeService.ChangeFileName(data);

            if (error != null)
            {
                return RedirectToAction("Index", new { errorId = "ChangeName", errorMsg = error });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFile(long fileIdToDelete)
        {
            if (fileIdToDelete == -1)
            {
                return RedirectToAction("Index", new { errorId = "FileIdToDelete", errorMsg = "Wybierz plik" });
            }

            treeService.DeleteFile(fileIdToDelete);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
