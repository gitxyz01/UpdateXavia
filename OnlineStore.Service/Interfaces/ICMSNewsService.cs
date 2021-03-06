﻿using OnlineStore.Model.Context;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface ICMSNewsService
    {
        IList<CMSNewsView> GetCMSNews(int pageNumber, int pageSize, out int totalItems);
        IList<CMSNewsView> GetCMSNewsByCategoryId(int categoryId, int pageNumber, int pageSize, out int totalItems);
        IList<CMSNewsView> GetRecentCMSNews();
        IList<CMSNewsView> GetCMSNewsForHomePage();
        IList<CMSNewsView> GetRelatedCMSNews(int id);
        bool AddCMSNews(CMSNewsView cmsNewsView);
        bool EditCMSNews(CMSNewsView cmsNewsView);
        bool DeleteCMSNews(int id, string deleteBy, bool isAdmin);
        bool UpdateCMSNewsCountView(int? newsId);
        CMSNewsView GetCMSNewsById(int? newsId);
        IList<CMSNewsView> GetCMSNewsTy(int categoryId = 0);
        IList<CMSNewsView> GetWaitingCMSNews();
        bool VerifyCMSNews(int id, int status);
    }
}
