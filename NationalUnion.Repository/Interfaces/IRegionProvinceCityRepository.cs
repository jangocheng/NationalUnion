using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IRegionProvinceCityRepository
    {
        #region Methods

        /// <summary>
        /// 获取大区列表
        /// </summary>
        /// <returns></returns>
        List<Region> GetRegionList();

        /// <summary>
        /// 根据大区Id，获取大区
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        Region GetRegionById(int rid);

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns></returns>
        List<Province> GetProvinceList();

        /// <summary>
        /// 根据省份Id，获取省份
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Province GetProvinceById(int pid);

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns></returns>
        List<City> GetCityList();

        /// <summary>
        /// 根据省份Id，获取城市列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        List<City> GetCityListByProvId(int pid);

        /// <summary>
        /// 根据城市Id，获取城市
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        City GetCityById(int cid);

        #endregion
    }
}
