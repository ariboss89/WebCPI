using System;
using Microsoft.EntityFrameworkCore;
using Web_CPI.Models;

namespace Web_CPI.Data
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Kriteria> Kriterias { get; set; }
        public DbSet<Subkriteria> Subkriterias { get; set; }
        public DbSet<Penilaian> Penilaians { get; set; }
        public DbSet<Alternatif> Alternatifs { get; set; }
        public DbSet<Pengguna> Penggunas { get; set; }
    }
}

