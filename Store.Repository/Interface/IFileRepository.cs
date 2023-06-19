using Store.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface IFileRepository
    {
        public bool SaveFilePath(string filePath, int UserId);

        public City FindCityId(string CityName);

        public int AddStoreAddress(string AddressLine1, string AddressLine2);

        public DataTable GetDataTabletFromCSVFile(string csv_file_path, int UserId);

        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData);
    }
}
