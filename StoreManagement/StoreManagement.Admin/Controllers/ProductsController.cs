﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Data.Entities;
using StoreManagement.Service.DbContext;
using StoreManagement.Service.Repositories.Interfaces;

namespace StoreManagement.Admin.Controllers
{
    //[Authorize]
    public class ProductsController : BaseController
    {
        private IContentRepository contentRepository;
        public ProductsController(IStoreContext dbContext, ISettingRepository settingRepository, IContentRepository contentRepository) : base(dbContext, settingRepository)
        {
            this.contentRepository = contentRepository;
        }

        public ActionResult Index(int storeId=0)
        {
            List<Content> resultList = new List<Content>();
            if (storeId == 0)
            {
                resultList = contentRepository.GetAll().ToList();
            }
            else
            {
                resultList = contentRepository.GetContentByType(storeId,"content");
            }
            return View(resultList);
        }

        //
        // GET: /Content/Details/5

        public ActionResult Details(int id = 0)
        {
            Content content = contentRepository.GetSingle(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        //
        // GET: /Content/Create

        public ActionResult Create()
        {
            var content = new Content();
            content.StoreId = 1;
            content.Type = "product";
            return View(content);
        }

        //
        // POST: /Content/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                contentRepository.Add(content);
                contentRepository.Save();
                return RedirectToAction("Index");
            }

            return View(content);
        }

        //
        // GET: /Content/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Content content = contentRepository.GetSingle(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        //
        // POST: /Content/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                contentRepository.Edit(content);
                contentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(content);
        }

        //
        // GET: /Content/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Content content = contentRepository.GetSingle(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        //
        // POST: /Content/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Content content = contentRepository.GetSingle(id);
            contentRepository.Delete(content);
            contentRepository.Save();
            return RedirectToAction("Index");
        }

        
    }
}