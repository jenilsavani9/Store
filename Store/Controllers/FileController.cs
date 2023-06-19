using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using Store.Entity.ViewModels;
using Store.Repository.Interface;
using System.Data;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _FileRepository;
        private readonly IConfiguration _configuration;

        public FileController(IFileRepository FileRepository, IConfiguration Configuration)
        {
            _FileRepository = FileRepository;
            _configuration = Configuration;
        }

        [HttpPost("Upload")]
        public ActionResult SaveFile([FromForm] FileModal file)
        {
            try
            {
                var myUniqueFileName = string.Format(@"{0}.csv", file.FormFile.FileName.Split(".csv")[0] + Guid.NewGuid());
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CSV", myUniqueFileName);
                using (FileStream fs = new(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(fs);
                }
                bool FileStatus = _FileRepository.SaveFilePath(myUniqueFileName, file.UserId);
                if (!FileStatus)
                {
                    throw new Exception();
                }
                DataTable dataTable = _FileRepository.GetDataTabletFromCSVFile(path, file.UserId);
                _FileRepository.InsertDataIntoSQLServerUsingSQLBulkCopy(dataTable);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
