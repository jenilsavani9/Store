using Store.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface IFileRepository
    {
        public bool SaveFilePath(string filePath, int UserId);

        public City FindCityId(string CityName);
    }
}
