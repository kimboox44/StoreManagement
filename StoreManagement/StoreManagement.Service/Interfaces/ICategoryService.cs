﻿using System;
using System.Collections.Generic;
using StoreManagement.Data.Entities;
using StoreManagement.Data.HelpersModel;
using StoreManagement.Data.Paging;

namespace StoreManagement.Service.Interfaces
{
    public interface ICategoryService : IService
    {
        List<Category> GetCategoriesByStoreId(int storeId);
        List<Category> GetCategoriesByStoreIdWithContent(int storeId);
        List<Category> GetCategoriesByStoreId(int storeId, String type);
        List<Category> GetCategoriesByStoreIdFromCache(int storeId, String type);
        Category GetSingle(int id);
        StorePagedList<Category> GetCategoryWithContents(int categoryId, int page, int pageSize = 25);
    }

}
