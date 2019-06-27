using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Repository
{
    public class CategoryRepository : Repository<ecom_Categories>
    {
        #region Contructures

        public CategoryRepository(OnlineStoreMVCEntities context)
            : base(context)
        {

        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get all category
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ecom_Categories> GetAllCategories()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Get categories except which one have status is Delete(Just Active and Deactive)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ecom_Categories> GetAllCategoriesWithoutDelete()
        {
            return dbSet.Where(c => c.Status != (int)Define.Status.Delete && c.Status != (int)Define.Status.WaitingCreate).ToList();
        }
        public IEnumerable<ecom_Categories> GetAllWaitingCategories()
        {
            return dbSet.Where(c => c.Status == (int)Define.Status.WaitingCreate || c.Status == (int)Define.Status.WaitingDelete).ToList();
        }

        /// <summary>
        /// Find category by id with status not equal to Delete
        /// </summary>
        /// <returns></returns>
        public ecom_Categories GetCategoryById(int id)
        {
            return dbSet.Where(c =>c.Id == id && c.Status != (int)Define.Status.Delete).FirstOrDefault();
        }

        /// <summary>
        /// Get all category with status is active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ecom_Categories> GetAllActiveCategory()
        {
            return dbSet.Where(c => c.Status == (int)Define.Status.Active || c.Status == (int)Define.Status.WaitingDelete).OrderByDescending(x=>x.CreatedDate).ToList();
        }

        /// <summary>
        /// Get list top category(root category without children)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ecom_Categories> GetTopCategories()
        {
            return dbSet.Where(c => c.Status == (int)Define.Status.Active && c.ParentId == null).OrderByDescending(x => x.CreatedDate).Take(8).ToList();
        }

        public IEnumerable<ecom_Categories> GetListCategoriesTy()
        {
            return dbSet.Where(c => c.Status == (int)Define.Status.Active  || c.Status == (int)Define.Status.WaitingDelete).OrderByDescending(x => x.CreatedDate).ToList();
        }
        #endregion
    }
}
