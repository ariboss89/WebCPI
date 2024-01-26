using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_CPI.Data;
using Web_CPI.Models;
using Web_CPI.SD;
using Web_CPI.ViewModel;

namespace Web_CPI.Controllers
{
    public class KeputusanController : Controller
    {
        private readonly ApplicationDbContext _db;
        List<Bobot> listBobot = new List<Bobot>();
        List<Tren> listTren = new List<Tren>();
        List<Hasil> listHasil = new List<Hasil>();
        Bobot bt = new Bobot();
        Tren tr = new Tren();
        Hasil hs = new Hasil();
      
        public KeputusanController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perhitungan()
        {
            var dataKriteria = _db.Kriterias.ToList();

            int jmlKriteria = dataKriteria.Count;

            //HITUNG ROC
            for(int a=0; a<jmlKriteria;a++)
            {
                double bobot = 0.0;

                bt = new Bobot();

                if (a == 0)
                {
                    for (int b = 1; b < jmlKriteria; b++)
                    {
                        double nilai = b + 1;

                        bobot += 1 / nilai;

                    }

                    double asli = ((bobot + 1)/ jmlKriteria);

                    string sa = asli.ToString("0.##");

                    bt.Nama = dataKriteria[a].Nama;
                    bt.Nilai = Convert.ToDouble(sa);
                    bt.Type = dataKriteria[a].Type;

                    listBobot.Add(bt);
                }
                else
                {
                    for (int b = a; b < jmlKriteria; b++)
                    {
                        double nilai = b + 1;

                        bobot += 1 / nilai;

                    }

                    double asli = (bobot / jmlKriteria);

                    string sa = asli.ToString("0.##");

                    bt.Nama = dataKriteria[a].Nama;
                    bt.Nilai = Convert.ToDouble(sa);
                    bt.Type = dataKriteria[a].Type;

                    listBobot.Add(bt);
                }
            }

            //TAHAP 2 & 3 : HITUNG NILAI TREN + & TREN -

            foreach (var item in listBobot)
            {
                var filterData = _db.Penilaians.Where(x => x.Kriteria == item.Nama).ToList();

                foreach(var data in filterData)
                {
                    tr = new Tren();

                    double hasil = 0.0;
                    double indeksAlt = 0.0;

                    var nilaiMax = filterData.OrderByDescending(x => x.Nilai).First();
                    var nilaiMin = filterData.Min(x=>x.Nilai);

                    if (data.Type.Equals("Tren Positif"))
                    {
                        hasil = (data.Nilai / nilaiMin)*100;
                    }
                    else
                    {
                        hasil = (nilaiMin/data.Nilai)*100;
                    }

                    indeksAlt = hasil * item.Nilai;

                    string decTren = hasil.ToString("0.##");
                    string decIndeks = indeksAlt.ToString("0.##");

                    tr.Alternatif = data.Alternatif;
                    tr.Kriteria = data.Kriteria;
                    tr.Subkriteria = data.Subkriteria;
                    tr.Type = data.Type;
                    tr.NilaiTrend = Convert.ToDouble(decTren);
                    tr.NilaiIndeksAlt = Convert.ToDouble(decIndeks);

                    listTren.Add(tr);
                }
            }

            //TAHAP HASIL AKHIR
            foreach(var lok in _db.Alternatifs)
            {
                var filterHasil = listTren.Where(x => x.Alternatif == lok.NamaAlternatif).ToList();

                double hasilAkhir = 0.0;

                hs = new Hasil();

                foreach (var hitung in filterHasil)
                {
                    hasilAkhir += hitung.NilaiIndeksAlt;
                }

                string decHasil = hasilAkhir.ToString("0.##");

                hs.Alternatif = filterHasil[0].Alternatif;
                hs.HasilAKhir = Convert.ToDouble(decHasil);

                listHasil.Add(hs);

            }

            var checkDataHasil = listHasil.OrderByDescending(x => x.HasilAKhir).ToList();

            //penentuan perankingan
            for(int i=0;i<checkDataHasil.Count;i++)
            {
                hs = new Hasil();

                hs.Alternatif = checkDataHasil[i].Alternatif;
                hs.HasilAKhir = checkDataHasil[i].HasilAKhir;
                hs.Perankingan = "Ranking " + (i + 1);

                listHasil.Add(hs);
            }

            var filterHasilAkhir = listHasil.Where(x => x.Perankingan != null).ToList();

            //pindahkan ke dalam ViewModel
            HasilVM hasilVM = new HasilVM()
            {
                Hasil = new Hasil(),
                ListBobot = listBobot,
                ListTren = listTren,
                ListHasil = filterHasilAkhir
            };

            SD_Hasil.ListHasil = hasilVM.ListHasil;

            return View(hasilVM);
        }

        public IActionResult Print()
        {
            Hasil hasil = new Hasil();

            var hasilList = SD_Hasil.ListHasil;

            return View(hasilList);
        }
    }
}