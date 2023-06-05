using Store.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interface
{
    public interface IHomeRepository
    {
        public object GetStores(int UserId);

        public object AddStores(StoresModel obj);

        public object GetCities();

        public object GetStates();

        public object GetCountry();
    }
}
