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
    public class PenilaianController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PenilaianController(ApplicationDbContext db)
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

            PenilaianVM kr = new PenilaianVM()
            {
                Penilaian = new Penilaian(),
                ListKriteria = _db.Kriterias.Select(i => new SelectListItem
                {
                    Text = i.Nama,
                    Value = i.Id.ToString()
                }),

                ListAlternatif = _db.Alternatifs.Select(i => new SelectListItem
                {
                    Text = i.NamaAlternatif,
                    Value = i.Id.ToString()
                })

            };

            if (id == null)
            {
                return View(kr);
            }

            var data = _db.Subkriterias.Where(x => x.Id == id).ToList();

            kr.Penilaian.Id = data[0].Id;
            kr.Penilaian.Kriteria = data[0].Kriteria;
            kr.Penilaian.Subkriteria = data[0].Pilihan;
            kr.Penilaian.Nilai = data[0].Nilai;

            if (kr.Penilaian == null)
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
        public IActionResult Upsert(PenilaianVM kr)
        {
            if (kr.Penilaian.Alternatif != null || kr.Penilaian.Kriteria != null)
            {
                var dataKriteria = _db.Kriterias.Where(x => x.Id == Convert.ToInt32(kr.Penilaian.Kriteria)).FirstOrDefault();

                var dataAlternatif = _db.Alternatifs.Where(x => x.Id == Convert.ToInt32(kr.Penilaian.Alternatif)).FirstOrDefault();

                var dataSubKriteria = _db.Subkriterias.Where(x => x.Id == Convert.ToInt16(kr.Penilaian.Subkriteria)).FirstOrDefault();

                string namaKriteria = dataKriteria.Nama;
                string namaAlt = dataAlternatif.NamaAlternatif;

                var dataPenilaian = _db.Penilaians.Where(x => x.Kriteria == namaKriteria && x.Alternatif == namaAlt);

                if (dataPenilaian.Count() != 0 && kr.Penilaian.Id == 0)
                {
                    TempData["error"] = $"Kriteria {namaKriteria} Telah di Tambahkan Pada Alternatif {namaAlt}, Silahkan Input Kriteria Lain !";

                    kr = new PenilaianVM()
                    {
                        Penilaian = new Penilaian(),
                        ListAlternatif = _db.Alternatifs.Select(i => new SelectListItem
                        {
                            Text = i.NamaAlternatif,
                            Value = i.Id.ToString()
                        }),
                        ListKriteria = _db.Kriterias.Select(i => new SelectListItem
                        {
                            Text = i.Nama,
                            Value = i.Id.ToString()
                        })
                    };

                    ModelState.Clear();

                    return View(kr);
                }

                if (kr.Penilaian.Id == 0)
                {
                    kr.Penilaian.Kriteria = dataKriteria.Nama;
                    kr.Penilaian.Alternatif = dataAlternatif.NamaAlternatif;
                    kr.Penilaian.Type = dataKriteria.Type;
                    kr.Penilaian.Subkriteria = dataSubKriteria.Pilihan;
                    kr.Penilaian.Nilai = dataSubKriteria.Nilai;

                    _db.Penilaians.Add(kr.Penilaian);
                    _db.SaveChanges();

                    TempData["success"] = "Penilaian berhasil di tambahkan";
                }
                else
                {


                    kr.Penilaian.Kriteria = dataKriteria.Nama;
                    kr.Penilaian.Alternatif = dataAlternatif.NamaAlternatif;
                    kr.Penilaian.Type = dataKriteria.Type;
                    kr.Penilaian.Subkriteria = dataSubKriteria.Pilihan;
                    kr.Penilaian.Nilai = dataSubKriteria.Nilai;

                    _db.Penilaians.Update(kr.Penilaian);
                    TempData["success"] = "Penilaian berhasil di ubah";

                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            TempData["error"] = "Penilaian gagal di tambahkan";

            kr = new PenilaianVM()
            {
                Penilaian = new Penilaian(),
                ListAlternatif = _db.Alternatifs.Select(i => new SelectListItem
                {
                    Text = i.NamaAlternatif,
                    Value = i.Id.ToString()
                }),
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
            List<Penilaian> listPenilaian = _db.Penilaians.ToList();

            return Json(new { data = listPenilaian });
        }

        [HttpGet]
        public IActionResult GetDataKriteria(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var data = _db.Kriterias.Where(x => x.Id == id).FirstOrDefault();

            var data2 = _db.Subkriterias.Where(x => x.Kriteria == data.Nama).ToList();

            PenilaianVM VM = new PenilaianVM()
            {
                Penilaian = new Penilaian(),

                ListAlternatif = _db.Alternatifs.Select(i => new SelectListItem
                {
                    Text = i.NamaAlternatif,
                    Value = i.Id.ToString()
                }),

                ListKriteria = _db.Kriterias.Select(i => new SelectListItem
                {
                    Text = i.Nama,
                    Value = i.Id.ToString()
                })

            };

            //var data = _db.Kriterias.Where(x => x.Id == id).FirstOrDefault();

            //var data2 = _db.Subkriterias.Where(x => x.Kriteria == data.Nama).ToList();

            VM.Penilaian.Type = data.Type;
            VM.ListSub = data2;

            //return Json(new { data = VM.Penilaian });

            return Json(VM);

        }

        [HttpGet]
        public IActionResult GetDataNilai(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var data = _db.Subkriterias.Where(x => x.Id == id).FirstOrDefault();

            PenilaianVM VM = new PenilaianVM()
            {
                Penilaian = new Penilaian(),

                ListAlternatif = _db.Alternatifs.Select(i => new SelectListItem
                {
                    Text = i.NamaAlternatif,
                    Value = i.Id.ToString()
                }),

                ListKriteria = _db.Kriterias.Select(i => new SelectListItem
                {
                    Text = i.Nama,
                    Value = i.Id.ToString()
                })

            };

            //var data = _db.Kriterias.Where(x => x.Id == id).FirstOrDefault();

            //var data2 = _db.Subkriterias.Where(x => x.Kriteria == data.Nama).ToList();

            VM.Penilaian.Nilai = data.Nilai;

            return Json(new { data = VM.Penilaian });

            //return Json(VM);

        }

        #endregion
    }
}