﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using Ninject;
using StoreManagement.Data.Constants;
using StoreManagement.Data.EmailHelper;
using StoreManagement.Data.Entities;
using StoreManagement.Helper;
using StoreManagement.Models;
using StoreManagement.Service.DbContext;
using StoreManagement.Service.Interfaces;
using StoreManagement.Service.Repositories.Interfaces;
using StoreManagement.Data;
using StoreManagement.Data.GeneralHelper;

namespace StoreManagement.Controllers
{
    public abstract class BaseController : Controller
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IFileManagerService FileManagerService { get; set; }

        [Inject]
        public IContentFileService ContentFileService { set; get; }

        [Inject]
        public IContentService ContentService { set; get; }

        [Inject]
        public ICategoryService CategoryService { set; get; }

        [Inject]
        public IStoreService StoreService { set; get; }

        [Inject]
        public INavigationService NavigationService { set; get; }

        [Inject]
        public IPageDesignService PageDesignService { set; get; }

        [Inject]
        public IStoreUserService StoreUserService { set; get; }

        [Inject]
        public ISettingService SettingService { set; get; }

        [Inject]
        public IEmailSender EmailSender { set; get; }


        [Inject]
        public IProductService ProductService { set; get; }

        [Inject]
        public IProductFileService ProductFileService { set; get; }

        [Inject]
        public IProductCategoryService ProductCategoryService { set; get; }

        protected Store Store { set; get; }



        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            GetStoreByDomain(requestContext);

        }
        private void GetStoreByDomain(RequestContext requestContext)
        {
            var sh = new StoreHelper();
            var store = sh.GetStoreByDomain(StoreService, requestContext.HttpContext.Request);
            this.Store = store;
            if (store == null)
            {
                throw new Exception("Store cannot be NULL");
            }
        }

        protected new HttpNotFoundResult HttpNotFound(string statusDescription = null)
        {
            return new HttpNotFoundResult(statusDescription);
        }
        protected bool IsModulActive(String controllerName)
        {
            return NavigationService.GetStoreActiveNavigations(Store.Id).Any(r => r.ControllerName.StartsWith(controllerName.ToLower()));
        }
        protected bool CheckRequest(BaseEntity entity)
        {
            return entity.StoreId == Store.Id;
        }

        protected BaseController()
        {
            ViewBag.MetaDescription = GetSettingValue(StoreConstants.MetaTagDescription);
            ViewBag.MetaKeywords = GetSettingValue(StoreConstants.MetaTagKeywords);
        }


        protected String GetSettingValue(String key)
        {
            try
            {
                var item = SettingService.GetStoreSettingsFromCache(Store.Id).FirstOrDefault(r => r.SettingKey.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                return item != null ? item.SettingValue : "";
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Store= " + Store.Domain + " Key=" + key, ex);
                return "";
            }


        }


    }
}