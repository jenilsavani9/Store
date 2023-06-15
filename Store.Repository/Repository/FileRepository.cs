using Store.Entity.Data;
using Store.Entity.Models;
using Store.Repository.Interface;
using System;
using System.Collections.Generic;
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
    }
}
