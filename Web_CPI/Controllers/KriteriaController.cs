using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_CPI.Data;
using Web_CPI.Models;

namespace Web_CPI.Controllers
{
    public class KriteriaController : Controller
    {
        Kriteria kr = new Kriteria();

        private readonly ApplicationDbContext _db;

        public KriteriaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            kr = new Kriteria();

            if (Id == null)
            {
                return View(kr);
            }

            var data = _db.Kriterias.Where(x => x.Id == Id).ToList();

            kr.Id = data[0].Id;
            kr.Nama = data[0].Nama;
            kr.Type = data[0].Type;

            if (kr == null)
            {
                return NotFound();
            }

            return View(kr);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(Kriteria kr)
        {
            if (ModelState.IsValid)
            {
                if (kr.Id == 0)
                {
                    _db.Kriterias.Add(kr);
                    _db.SaveChanges();

                    TempData["success"] = "Data Kriteria berhasil di tambahkan";
                }
                else
                {
                    _db.Kriterias.Update(kr);
                    _db.SaveChanges();

                    TempData["success"] = "Data Kriteria berhasil di ubah";
                }

                return RedirectToAction("Index");
            }

            TempData["error"] = "Data Kriteria gagal di tambahkan";

            return View(kr);

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Kriteria> kriteriaList = _db.Kriterias.ToList();

            return Json(new { data = kriteriaList });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _db.Kriterias.Find(Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            _db.Kriterias.Remove(objFromDb);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}