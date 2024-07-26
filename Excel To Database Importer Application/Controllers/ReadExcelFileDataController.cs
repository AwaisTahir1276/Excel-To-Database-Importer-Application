using Excel_To_Database_Importer_Application.ApplicationDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;

namespace Read_ExcelFile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadExcelFileDataController : ControllerBase
    {
        private readonly ReadExcelFileDataDbContext _db;
        private readonly IConfiguration _configuration;
        public ReadExcelFileDataController(ReadExcelFileDataDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        // GET: ReadExcelFileData
        [HttpGet(Name = "ReadExcelFileData")]
        public bool ReadExcelFileData()
        {
            try
            {
                var sqlConString = _configuration["ConnectionStrings:ReadExcelFileData"] ?? "";

                string filePath = "E:\\TestFile.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var cities = _db.Cities.ToList();
                var metroAreaCities = _db.MetroAreaCities.ToList();

                // Open the Excel package
                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    // Get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // Get the dimensions of the worksheet
                    int rows = worksheet.Dimension.Rows;
                    using (SqlConnection con = new SqlConnection(sqlConString))
                    {
                        con.Open();

                        for (int row = 1; row <= rows; row++)
                        {
                            var city = cities.FirstOrDefault(x => x.city.ToLower().Trim().Equals(worksheet.Cells[row, 4].Text.ToLower().Trim())
                                                 && x.state.ToLower().Trim().Equals(worksheet.Cells[row, 3].Text.ToLower().Trim()));
                            if (city != null)
                            {
                                var value = worksheet.Cells[row, 5].Text.Trim();
                                var metroAreaCity = metroAreaCities.FirstOrDefault(x => x.cityID == city.cityID);
                                if (metroAreaCity != null)
                                {
                                    RecordFound(worksheet.Cells[row, 1].Text.Trim(), worksheet.Cells[row, 2].Text.Trim(), worksheet.Cells[row, 3].Text.Trim(), worksheet.Cells[row, 4].Text.Trim(), worksheet.Cells[row, 5].Text.Trim(), con);
                                    metroAreaCity.url_Foreclosure = value;
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    RecordNotFound(worksheet.Cells[row, 1].Text.Trim(), worksheet.Cells[row, 2].Text.Trim(), worksheet.Cells[row, 3].Text.Trim(), worksheet.Cells[row, 4].Text.Trim(), worksheet.Cells[row, 5].Text.Trim(), con);
                                }
                            }
                            else
                            {
                                RecordNotFound(worksheet.Cells[row, 1].Text.Trim(), worksheet.Cells[row, 2].Text.Trim(), worksheet.Cells[row, 3].Text.Trim(), worksheet.Cells[row, 4].Text.Trim(), worksheet.Cells[row, 5].Text.Trim(), con);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void RecordFound(string metroName, string displayMetroName, string state, string City, string url_Foreclosure, SqlConnection con)
        {
            string query = $"INSERT INTO RecordFound (metroName, displayMetroName, state, City, urlForeclosure) VALUES ('{metroName}', '{displayMetroName}', '{state}', '{City}', '{url_Foreclosure}')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        private void RecordNotFound(string metroName, string displayMetroName, string state, string City, string url_Foreclosure, SqlConnection con)
        {
            string query = $"INSERT INTO RecordNotFound (metroName, displayMetroName, state, City, urlForeclosure) VALUES ('{metroName}', '{displayMetroName}', '{state}', '{City}', '{url_Foreclosure}')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }
    }
}
