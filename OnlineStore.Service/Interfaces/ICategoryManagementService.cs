﻿using OnlineStore.Model.Context;
using OnlineStore.Model.MessageModel;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface ICategoryManagementService:IDisposable
    {
        IEnumerable<SummaryCategoryViewModel> GetListCategories();
        IEnumerable<ecom_Categories> GetAllCategories();
        IEnumerable<DetailCategoryViewModel> GetAllWaitingCategories();
        IEnumerable<SummaryCategoryViewModel> GetCategories(int pageNumber, int pageSize, out int totalItems);
        ecom_Categories GetCategoryById(int id);
        DetailCategoryViewModel GetDetailCategory(int id);
        bool AddCategory(CreateCategoryPostRequest category);
        bool UpdateCategory(CategoryViewModel category);
        bool DeleteCategory(int id, string deleteBy,bool isAdmin);
        CategoryViewModel getCategoryViewModel(int id);
        IEnumerable<DetailCategoryViewModel> GetCategoriesAdminTy();
        bool VerifyCategory(int id, int status);
    }
}
