using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_CPI.Data;
using Web_CPI.Models;
using Web_CPI.ViewModel;

namespace Web_CPI.Controllers
{
    public class SubkriteriaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubkriteriaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //if (StaticDetails.Role == null)
            //{
            //    TempData["error"] = "Silahkan login untuk melanjutkan !!";

            //    return RedirectToAction("Login", "Home");
            //}

            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            SubkriteriaVM kr = new SubkriteriaVM()
            {
                Subkriteria = new Subkriteria(),
                ListKriteria = _db.Kriterias.Select(i => new SelectListItem
                {
                    Text = i.Nama,
                    Value = i.Id.ToString()
                }),

            };

            if (id == null)
            {
                return View(kr);
            }

            var data = _db.Subkriterias.Where(x => x.Id == id).ToList();

            kr.Subkriteria.Id = data[0].Id;
            kr.Subkriteria.Kriteria = data[0].Kriteria;
            kr.Subkriteria.Pilihan = data[0].Pilihan;
            kr.Subkriteria.Nilai = data[0].Nilai;

            if (kr.Subkriteria == null)
            {
                return NotFound();
            }

            return View(kr);

        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _db.Subkriterias.Find(Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            _db.Subkriterias.Remove(objFromDb);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpPost]
        public IActionResult Upsert(SubkriteriaVM kr)
        {
            if (kr.Subkriteria.Kriteria != null)
            {
                var dataKriteria = _db.Kriterias.Where(x => x.Id == Convert.ToInt32(kr.Subkriteria.Kriteria)).FirstOrDefault();

                string namaKriteria = dataKriteria.Nama;

                var dataPenilaian = _db.Subkriterias.Where(x => x.Kriteria == namaKriteria);

                kr.Subkriteria.Kriteria = namaKriteria;

                if (kr.Subkriteria.Id == 0)
                {
                    kr.Subkriteria.Kriteria = dataKriteria.Nama;
                    kr.Subkriteria.Pilihan = kr.Subkriteria.Pilihan;
                    kr.Subkriteria.Nilai = kr.Subkriteria.Nilai;

                    _db.Subkriterias.Add(kr.Subkriteria);
                    _db.SaveChanges();

                    TempData["success"] = "Subkriteria berhasil di tambahkan";
                }
                else
                {
                    _db.Subkriterias.Update(kr.Subkriteria);
                    TempData["success"] = "Subkriteria berhasil di ubah";

                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            TempData["error"] = "Penilaian gagal di tambahkan";

            kr = new SubkriteriaVM()
            {
                Subkriteria = new Subkriteria(),

                ListKriteria = _db.Kriterias.Select(i => new SelectListItem
                {
                    Text = i.Nama,
                    Value = i.Id.ToString()
                })
            };

            return View(kr);
        }

        #region API CALLS

        public JsonResult GetAll()
        {
            List<Subkriteria> listSubkriteria = _db.Subkriterias.ToList();

            return Json(new { data = listSubkriteria });
        }

        //[HttpGet]
        //public IActionResult GetDataKriteria(int? id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    SubkriteriaVM VM = new SubkriteriaVM()
        //    {
        //        Subkriteria = new Subkriteria(),


        //        ListKriteria = _db.Kriterias.Select(i => new SelectListItem
        //        {
        //            Text = i.Nama,
        //            Value = i.Id.ToString()
        //        })

        //    };

        //    var data = _db.Kriterias.Where(x => x.Id == id).FirstOrDefault();

        //    VM.Subkriteria.Jenis = data.Jenis;

        //    return Json(new { data = VM.Penilaian });

        //}

        #endregion
    }
}