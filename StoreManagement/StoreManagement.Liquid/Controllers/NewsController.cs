﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Data.Constants;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Liquid.Helper;

namespace StoreManagement.Liquid.Controllers
{
    public class NewsController : BaseController
    {
        //
        // GET: /Blogs/
        public ActionResult Index(int page = 1)
        {
            try
            {
                if (!IsModulActive(StoreConstants.NewsType))
                {
                    return HttpNotFound("Not Found");
                }

                var newsPageDesignTask = PageDesignService.GetPageDesignByName(Store.Id, "NewsIndex");
                var contentsTask = ContentService.GetContentsCategoryIdAsync(Store.Id, null, StoreConstants.NewsType, true, page, GetSettingValueInt("NewsIndexPageSize", StoreConstants.DefaultPageSize));
                var categories = CategoryService.GetCategoriesByStoreIdAsync(Store.Id, StoreConstants.NewsType, true);
                var dic = ContentHelper.GetContentsIndexPage(contentsTask, newsPageDesignTask, categories, StoreConstants.NewsType);

                return View(dic);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "NewsController:Index:" + ex.StackTrace, page);
                return new HttpStatusCodeResult(500);
            }
        }
        //
        // GET: /Blogs/
        public ActionResult Detail(String id = "")
        {
            try
            {
                if (!IsModulActive(StoreConstants.NewsType))
                {
                    return HttpNotFound("Not Found");
                }
                int newsId = id.Split("-".ToCharArray()).Last().ToInt();
                var blogsPageDesignTask = PageDesignService.GetPageDesignByName(Store.Id, "NewsDetailPage");
                var contentsTask = ContentService.GetContentByIdAsync(newsId);
                var categoryTask = CategoryService.GetCategoryByContentIdAsync(Store.Id, newsId);
                var dic = ContentHelper.GetContentDetailPage(contentsTask, blogsPageDesignTask, categoryTask, StoreConstants.NewsType);

                return View(dic);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "NewsController:News:" + ex.StackTrace);
                return new HttpStatusCodeResult(500);
            }
        }
	}
}