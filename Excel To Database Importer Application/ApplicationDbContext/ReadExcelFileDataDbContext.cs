using Microsoft.EntityFrameworkCore;
using Read_ExcelFile.Models;

namespace Excel_To_Database_Importer_Application.ApplicationDbContext
{
    public class ReadExcelFileDataDbContext : DbContext
    {
        public ReadExcelFileDataDbContext(DbContextOptions<ReadExcelFileDataDbContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<MetroAreaCity> MetroAreaCities { get; set; }
    }
}
