﻿using Ninject;
using StoreManagement.Data.Entities;
using StoreManagement.Service.DbContext;
using StoreManagement.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Admin.Controllers
{
    [Authorize]
    public class PageDesignsController : BaseController
    {

        //
        // GET: /PageDesigns/
        public ViewResult Index(int storeId = 0, String search = "")
        {
            storeId = GetStoreId(storeId);
            var resultList = new List<PageDesign>();
            resultList = PageDesignRepository.GetPageDesignByStoreId(storeId, search);
            return View(resultList);
        }

        //
        // GET: /PageDesigns/Details/5

        public ViewResult Details(int id)
        {
            PageDesign pagedesign = PageDesignRepository.GetSingle(id);
            return View(pagedesign);
        }


        //
        // GET: /PageDesigns/Edit/5

        public ActionResult SaveOrEdit(int id = 0)
        {
            var pagedesign = new PageDesign();
            if (id == 0)
            {
                pagedesign.CreatedDate = DateTime.Now;
                pagedesign.State = true;
                pagedesign.UpdatedDate = DateTime.Now;

            }
            else
            {
                pagedesign = PageDesignRepository.GetSingle(id);
                pagedesign.State = true;
                pagedesign.UpdatedDate = DateTime.Now;
            }

            return View(pagedesign);
        }

        //
        // POST: /PageDesigns/Edit/5

        [HttpPost]
        public ActionResult SaveOrEdit(PageDesign pagedesign)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (pagedesign.Id > 0)
                    {
                        PageDesignRepository.Edit(pagedesign);
                    }
                    else
                    {
                        PageDesignRepository.Add(pagedesign);
                    }

                    PageDesignRepository.Save();

                    if (IsSuperAdmin)
                    {
                        return RedirectToAction("Index", new { storeId = pagedesign.StoreId });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Unable to save changes:" + pagedesign, ex);
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(pagedesign);
        }

        //
        // GET: /PageDesigns/Delete/5

        public ActionResult Delete(int id)
        {
            PageDesign pagedesign = PageDesignRepository.GetSingle(id);
            return View(pagedesign);
        }

        //
        // POST: /PageDesigns/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            PageDesign pagedesign = PageDesignRepository.GetSingle(id);
            try
            {
                PageDesignRepository.Delete(pagedesign);
                PageDesignRepository.Save();

                if (IsSuperAdmin)
                {
                    return RedirectToAction("Index", new { storeId = pagedesign.StoreId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Unable to save changes:" + pagedesign, ex);
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
        }

    }
}