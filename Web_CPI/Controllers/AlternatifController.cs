using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_CPI.Data;
using Web_CPI.Models;

namespace Web_CPI.Controllers
{
    public class AlternatifController : Controller
    {
        Alternatif ar = new Alternatif();

        private readonly ApplicationDbContext _db;

        public AlternatifController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            ar = new Alternatif();

            if (Id == null)
            {
                return View(ar);
            }

            var data = _db.Alternatifs.Where(x => x.Id == Id).ToList();

            ar.Id = data[0].Id;
            ar.NamaAlternatif = data[0].NamaAlternatif;
            ar.Alamat = data[0].Alamat;

            if (ar == null)
            {
                return NotFound();
            }

            return View(ar);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(Alternatif ar)
        {
            if (ModelState.IsValid)
            {
                if (ar.Id == 0)
                {
                    _db.Alternatifs.Add(ar);
                    _db.SaveChanges();

                    TempData["success"] = "Data Alternatif berhasil di tambahkan";
                }
                else
                {
                    _db.Alternatifs.Update(ar);
                    _db.SaveChanges();

                    TempData["success"] = "Data Alternatif berhasil di ubah";
                }

                return RedirectToAction("Index");
            }

            TempData["error"] = "Data Alternatif gagal di tambahkan";

            return View(ar);

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Alternatif> alternatifList = _db.Alternatifs.ToList();

            return Json(new { data = alternatifList });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _db.Alternatifs.Find(Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            _db.Alternatifs.Remove(objFromDb);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}