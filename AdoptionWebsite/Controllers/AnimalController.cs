using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdoptionWebsite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdoptionWebsite.Models.Repo;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace AdoptionWebsite.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        CommTool Comm = new CommTool();
        private IAnimalRepository MyDB = new AnimalRepository();
        private IAnimalCateRepository MyDBCate = new AnimalCateRepository();

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _accessor;

        public AnimalController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
        }

        public IActionResult Index(int p = 1)
        {
            IQueryable<Animal_AnimalCateViewModel> itemList = MyDB.ListAllAnimal().OrderBy(o => o.Animals.Idno).Skip((p - 1) * 20).Take(20);

            int TotalCount = itemList.Count();
            ViewBag.TotalCount = TotalCount;
            ViewBag.NowPage = p;

            return View(itemList.ToList());
        }

        public IActionResult AnimalCreat()
        {
            string[,] itemList = { { "公", "M" }, { "母", "F" } };
            List<SelectListItem> ts = Comm.GetSelectLiist(itemList, false);
            ViewBag.List = ts;

            IQueryable<AnimalCate> CateList = MyDBCate.ListAllCate();
            List<SelectListItem> tsCate = CateList.Select(s => new SelectListItem { Text = s.CateName, Value = s.Idno.ToString() }).ToList();
            ViewBag.ListCate = tsCate;

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AnimalCreat(Animal item, List<IFormFile> Files)
        {
            if (item != null && ModelState.IsValid)
            {
                int newid = MyDB.AddAnimal(item);
                if (newid <= 0)
                {
                    ModelState.AddModelError("Error", "新增失敗");
                    return View();
                }
                else
                {
                    if (Files.Count <= 0) { return RedirectToAction("Index"); }
                    else
                    {
                        List<Files> itemFiles = new List<Files>();
                        foreach (var file in Files)
                        {
                            if (file.Length <= 0) continue;
                            string webRootPath = _hostingEnvironment.ContentRootPath;
                            string FilePath = "Files\\" + DateTime.Now.ToString("HHmmssfff") + "_" + file.FileName;
                            string FullPath = webRootPath + "\\" + FilePath;
                            using (var stream = new FileStream(FullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            if (System.IO.File.Exists(FullPath))
                            {
                                itemFiles.Add(new Models.Files
                                {
                                    XTable = "Animal",
                                    TableId = newid,
                                    FileName = file.FileName,
                                    FilePath = FilePath
                                });
                            }
                        }

                        if (itemFiles.Count > 0) MyDB.AddPhoto(itemFiles);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id = 1)
        {
            Animal_FilesViewModel item = MyDB.GetAnimalByID(id);
            return View(item);
        }

        public IActionResult Edit(int id = 1)
        {
            string[,] itemList = { { "公", "M" }, { "母", "F" } };
            List<SelectListItem> ts = Comm.GetSelectLiist(itemList, false);
            ViewBag.List = ts;

            string[,] itemIsAdoption = { { "未領養", "0" }, { "已領養", "1" } };
            List<SelectListItem> tsIsAdoption = Comm.GetSelectLiist(itemIsAdoption, false);
            ViewBag.ListIsAdoption = tsIsAdoption;

            IQueryable<AnimalCate> CateList = MyDBCate.ListAllCate();
            List<SelectListItem> tsCate = CateList.Select(s => new SelectListItem { Text = s.CateName, Value = s.Idno.ToString() }).ToList();
            ViewBag.ListCate = tsCate;

            Animal_FilesViewModel item = MyDB.GetAnimalByID(id);
            return View(item);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(Animal_FilesViewModel item, List<IFormFile> Files)
        {
            if (item != null && ModelState.IsValid)
            {
                int c = MyDB.EditAnimal(item.Animals);
                if (c <= 0)
                {
                    ModelState.AddModelError("Error", "更新失敗");
                    return View();
                }
                else
                {
                    MyDB.DelPhoto(item.Animals.Idno);
                    if (Files.Count <= 0) { return RedirectToAction("Index"); }
                    else
                    {
                        List<Files> itemFiles = new List<Files>();
                        foreach (var file in Files)
                        {
                            if (file.Length <= 0) continue;
                            string webRootPath = _hostingEnvironment.ContentRootPath;
                            string FilePath = "Files\\" + DateTime.Now.ToString("HHmmssfff") + "_" + file.FileName;
                            string FullPath = webRootPath + "\\" + FilePath;
                            using (var stream = new FileStream(FullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            if (System.IO.File.Exists(FullPath))
                            {
                                itemFiles.Add(new Models.Files
                                {
                                    XTable = "Animal",
                                    TableId = item.Animals.Idno,
                                    FileName = file.FileName,
                                    FilePath = FilePath
                                });
                            }
                        }

                        if (itemFiles.Count > 0) MyDB.AddPhoto(itemFiles);
                    }

                }

            }
            return RedirectToAction("Index");
        }

    }
}
