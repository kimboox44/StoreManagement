﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using System.Web.Security;
using MvcPaging;
using StoreManagement.Data.CacheHelper;
using StoreManagement.Data.Entities;
using StoreManagement.Service.DbContext;
using StoreManagement.Service.Repositories.Interfaces;
using StoreManagement.Data.GeneralHelper;
using WebGrease.Css.Ast.Selectors;


namespace StoreManagement.Admin.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        static readonly TypedObjectCache<Store> UserStoreCache = new TypedObjectCache<Store>("UserStoreCache");

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }


        public ActionResult NoAccessPage(int id)
        {
            int storeId = id;
            Logger.Info("NoAccessPage. StoreId:" + storeId);
            return View();
        }

        //<li>
        //                           <a href="@url">Go to frontend <i class="glyphicon glyphicon-share-alt"></i></a>
        //                       </li>



        public ActionResult StoreName()
        {
            if (IsSuperAdmin)
            {
                return PartialView("StoreName", "Store Management Admin Panel");
            }
            else
            {
                return PartialView("StoreName", this.LoginStore.Name);
            }
        }
      //  [OutputCache(CacheProfile = "Cache20Minutes")]
        public ActionResult StoreSearch()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("StoreSearch");
            }
            else
            {
                return new EmptyResult();
            }
        }
        public ActionResult ReturnFrontEndUrl()
        {
            if (IsSuperAdmin)
            {
                return new EmptyResult();
            }
            else
            {
                return PartialView("ReturnFrontEndUrl", this.LoginStore);
            }
        }

        public ActionResult AdminSearch(String adminsearchkey, int page = 1)
        {
            ViewBag.SearchKey = adminsearchkey;
            adminsearchkey = adminsearchkey.Trim().ToLower();
            List<BaseContent> resultList = new List<BaseContent>();
            int storeId = this.LoginStore.Id;

            var contentList = from cus in this.DbContext.Contents
                              where cus.StoreId == storeId
                              && cus.Name.ToLower().Contains(adminsearchkey)
                              orderby cus.Ordering, cus.Id descending
                              select cus;
            resultList.AddRange(contentList.ToList());

            var productList = from cus in this.DbContext.Products
                              where cus.StoreId == storeId
                                     && cus.Name.ToLower().Contains(adminsearchkey)
                              orderby cus.Ordering, cus.Id descending
                              select cus;
            resultList.AddRange(productList.ToList());
            var returnSearchModel = new PagedList<BaseContent>(resultList, page - 1, 20, resultList.Count);
            return View(returnSearchModel);
        }
        public ActionResult SuperAdminSearch(String adminsearchkey, int page = 1)
        {
            ViewBag.SearchKey = adminsearchkey;
            adminsearchkey = adminsearchkey.Trim().ToLower();
            List<BaseContent> resultList = new List<BaseContent>();


            var contentList = from cus in this.DbContext.Contents
                              where cus.Name.ToLower().Contains(adminsearchkey)
                              orderby cus.Ordering, cus.Id descending
                              select cus;
            resultList.AddRange(contentList.ToList());

            var productList = from cus in this.DbContext.Products
                              where cus.Name.ToLower().Contains(adminsearchkey)
                              orderby cus.Ordering, cus.Id descending
                              select cus;

            resultList.AddRange(productList.ToList());
            var returnSearchModel = new PagedList<BaseContent>(resultList, page - 1, 20, resultList.Count);
            return View(returnSearchModel);
        }

     
        public ActionResult LabelsDropDown(int storeId = 0, String labelType = "", int[] selectedLabelsId = null)
        {
            var resultList = new List<Label>();
            storeId = GetStoreId(storeId);
            resultList = LabelRepository.GetStoreLabels(storeId);

            var items = new List<SelectListItem>();
            foreach (var label in resultList)
            {
                items.Add(new SelectListItem { Text = label.Name, Value = label.Id.ToStr(), Selected = selectedLabelsId != null && selectedLabelsId.Contains(label.Id) });
            }

            return PartialView("LabelsDropDown", items);

        }

    }
}
