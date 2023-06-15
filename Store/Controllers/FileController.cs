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
                DataTable dataTable = GetDataTabletFromCSVFile(path, file.UserId);
                InsertDataIntoSQLServerUsingSQLBulkCopy(dataTable);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private DataTable GetDataTabletFromCSVFile(string csv_file_path, int UserId)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields()!;
                    if(colFields!=null && colFields.Length > 0)
                    {
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    


                    for(int j=0; j<csvReader.LineNumber; j++)
                    {
                        if (csvReader.ReadFields() != null)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            if (fieldData != null)
                            {
                                //Making empty value as null
                                for (int i = 0; i < fieldData.Length; i++)
                                {
                                    if (fieldData[i] == "")
                                    {
                                        fieldData[i] = null!;
                                    }
                                }
                                csvData.Rows.Add(fieldData);
                            }

                        }
                        else
                        {
                            throw new Exception();
                        }
                    }



                    //for address
                    var addressData = csvData.Columns["AddressLine1"];
                    DataColumn AddressDataColumn = new DataColumn("AddressId");
                    csvData.Columns.Add(AddressDataColumn);
                    if (addressData != null && addressData.Table!=null)
                    {
                        for (int i = 0; i < addressData.Table.Rows.Count; i++)
                        {
                            var data = csvData.Rows[i][Array.IndexOf(colFields, "AddressLine1")];
                            var id = _FileRepository.AddStoreAddress(data.ToString(), "");
                            csvData.Rows[i][csvData.Columns.IndexOf("AddressId")] = id;

                        }
                    }
                    
                    csvData.Columns.Remove("AddressLine1");



                    var cityData = csvData.Columns["CityName"];
                    DataColumn CityDataColumn = new DataColumn("CityId");
                    csvData.Columns.Add(CityDataColumn);

                    DataColumn CountryDataColumn = new DataColumn("CountryId");
                    csvData.Columns.Add(CountryDataColumn);

                    DataColumn StateDataColumn = new DataColumn("StateId");
                    csvData.Columns.Add(StateDataColumn);


                    for (int i = 0; i < cityData!.Table!.Rows.Count; i++)
                    {
                        var data = csvData.Rows[i][Array.IndexOf(colFields, "CityName")];
                        var id = _FileRepository.FindCityId(data.ToString()!);
                        csvData.Rows[i][csvData.Columns.IndexOf("CityId")] = id.CityId;
                        csvData.Rows[i][csvData.Columns.IndexOf("CountryId")] = id.CountryId;
                        csvData.Rows[i][csvData.Columns.IndexOf("StateId")] = id.StateId;
                    }
                    csvData.Columns.Remove("CityName");

                    //for userid column
                    DataColumn userIdDataColumn = new DataColumn("UserId");
                    userIdDataColumn.DefaultValue = UserId;
                    csvData.Columns.Add(userIdDataColumn);

                    //for status column
                    DataColumn StatusDataColumn = new DataColumn("Status");
                    StatusDataColumn.DefaultValue = true;
                    csvData.Columns.Add(StatusDataColumn);

                    //for CreatedAt column
                    DataColumn CreatedAtDataColumn = new DataColumn("CreatedAt");
                    CreatedAtDataColumn.DefaultValue = DateTime.Now;
                    csvData.Columns.Add(CreatedAtDataColumn);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return csvData;
        }

        static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Data Source=PCI114\\SQL2017;DataBase=Store;User ID=sa;Password=tatva123;TrustServerCertificate=True;MultipleActiveResultSets=true"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "UserStores";
                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(csvFileData);
                }
            }
        }
    }
}
