using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using Store.Entity.Data;
using Store.Entity.Models;
using Store.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly StoreContext _db;

        public FileRepository(StoreContext db)
        {
            _db = db;
        }

        public bool SaveFilePath(string filePath, int UserId)
        {
            try
            {
                if (filePath != null)
                {
                    _db.StoreFiles.Add(new Entity.Models.StoreFile
                    {
                        FilePath = filePath,
                        UserId = UserId
                    });
                    _db.SaveChanges();
                }
                else
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public City FindCityId(string CityName)
        {
            try
            {
                var id = _db.Cities.FirstOrDefault(c => c.CityName == CityName);
                if (id == null)
                {
                    return null;
                }
                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AddStoreAddress(string AddressLine1, string AddressLine2)
        {
            try
            {
                Address address = new Address();
                address.AddressLine1 = AddressLine1;
                address.AddressLine2 = AddressLine2;
                _db.Addresses.Add(address);
                _db.SaveChanges();
                
                return address.AddressId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public DataTable GetDataTabletFromCSVFile(string csv_file_path, int UserId)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadLine().Split(',');
                    if (colFields != null && colFields.Length > 0)
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

                    while (!csvReader.EndOfData)
                    {

                        //string[] fieldData = csvReader.ReadLine().Split(',');
                        string[] fieldData = csvReader.ReadFields();
                        if (fieldData != null)
                        {
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





                    //for address
                    var addressData = csvData.Columns["AddressLine1"];
                    DataColumn AddressDataColumn = new DataColumn("AddressId");
                    csvData.Columns.Add(AddressDataColumn);
                    if (addressData != null && addressData.Table != null)
                    {
                        for (int i = 0; i < addressData.Table.Rows.Count; i++)
                        {
                            var data = csvData.Rows[i][Array.IndexOf(colFields, "AddressLine1")];
                            var id = AddStoreAddress(data.ToString(), "");
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
                        var id = FindCityId(data.ToString()!);
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

        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            try
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
            catch (Exception)
            {
                throw new Exception();
            }
            
        }
    }
}
