using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class RegionProvinceCityRepository : IRegionProvinceCityRepository
    {
        public List<Region> GetRegionList()
        {
            using (var db = new NationalUnionContext())
            {
                return db.Regions.ToList();
            }
        }

        public Region GetRegionById(int rid)
        {
            using (var db = new NationalUnionContext())
            {
                return db.Regions.FirstOrDefault(r => r.RId == rid);
            }
        }

        public List<Province> GetProvinceList()
        {
            using (var db = new NationalUnionContext())
            {
                return db.Provinces.ToList();
            }
        }

        public Province GetProvinceById(int pid)
        {
            using (var db = new NationalUnionContext())
            {
                return db.Provinces.FirstOrDefault(p => p.PId == pid);
            }
        }

        public List<City> GetCityList()
        {
            using (var db = new NationalUnionContext())
            {
                return db.Cities.ToList();
            }
        }

        public List<City> GetCityListByProvId(int pid)
        {
            using (var db = new NationalUnionContext())
            {
                return db.Cities.Where(c => c.PId == pid).ToList();
            }
        }

        public City GetCityById(int cid)
        {
            using (var db = new NationalUnionContext())
            {
                return db.Cities.FirstOrDefault(c => c.CId == cid);
            }
        }
    }
}
