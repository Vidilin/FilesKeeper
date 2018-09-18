using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FilesKeeperWeb.Services;
using FileKeeper.DAL.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace FilesKeeperWeb.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private DataBase db;

        public FilesController(DataBase dataBase)
        {
            db = dataBase;
        }

        public IActionResult SendFiles()
        {
            return View();
        }

        public IActionResult Index()
        {
            var user = db.MRUserManager.GetUser(User.Identity.Name);
            ViewBag.Title = user.Login;
            var files = db.MRFilesManager.GetByUser(user);
            return View(files);
        }

        [HttpPost]
        //[RequestSizeLimit(50 * 1024 * 1024)]
        public IActionResult Upload(IFormFile file)
        {
            if (file.Length > (50 * 1024 * 1024)) return new BadRequestObjectResult("Слишком большой файл");

            var user = db.MRUserManager.GetUser(User.Identity.Name);
            UserFile newFile = new UserFile
            {
                Filename = file.FileName,
                UserId = user.Id,
                ContentType = file.ContentType
            };

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                newFile.Bin = binaryReader.ReadBytes((int)file.Length);
            }

            db.MRFilesManager.AddUserFile(user, newFile);
            return new OkResult();
        }

        [HttpPost]
        //[RequestSizeLimit(50 * 1024 * 1024)]
        public IActionResult AddFiles(IFormFileCollection uploads)
        {
            var user = db.MRUserManager.GetUser(User.Identity.Name);
            foreach (var file in uploads)
            {
                if (file.Length > (50 * 1024 * 1024)) return new BadRequestObjectResult(string.Format("{0} слишком большой", file.Name));

                UserFile newFile = new UserFile
                {
                    Filename = file.FileName,
                    UserId = user.Id,
                    ContentType = file.ContentType
                };

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    newFile.Bin = binaryReader.ReadBytes((int)file.Length);
                }

                db.MRFilesManager.AddUserFile(user, newFile);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id, string hash)
        {
            if (id != null)
            {
                db.MRFilesManager.DelFile((int)id, hash);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }          
        }

        [HttpPost]
        public IActionResult LoadFile(int? id, string hash)
        {
            if (id != null)
            {
                var file = db.MRFilesManager.GetFile((int)id, hash);
                return File(file.Bin, file.ContentType, file.Filename);
            }
            else
            {
                return NotFound();
            }
        }
    }
}