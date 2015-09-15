﻿using GenericRepository.EntityFramework;
using StoreManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Service.Interfaces;

namespace StoreManagement.Service.Repositories.Interfaces
{
    public interface IContentRepository : IBaseRepository<Content, int>, IContentService, IDisposable 
    {
        List<Content> GetContentsByStoreId(int storeId, string searchKey, string typeName);
        List<Content> GetMainPageContents(int storeId, int? categoryId, string type, int? take);
    }
}
