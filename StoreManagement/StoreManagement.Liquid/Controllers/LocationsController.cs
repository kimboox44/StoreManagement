﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Data.Constants;

namespace StoreManagement.Liquid.Controllers
{
    public class LocationsController : BaseController
    {
        //
        // GET: /Locations/
        public ActionResult Index()
        {

            try
            {
                
                var pageDesignTask = PageDesignService.GetPageDesignByName(StoreId, "LocationsIndex");
                var locationsTask = LocationService.GetLocationsAsync(StoreId, null, true);

                LocationHelper.StoreSettings = GetStoreSettings();
                LocationHelper.ImageWidth = GetSettingValueInt("LocationsIndex_ImageWidth", 50);
                LocationHelper.ImageHeight = GetSettingValueInt("LocationsIndex_ImageHeight", 50);
                var pageOutput = LocationHelper.GetLocationIndexPage(pageDesignTask, locationsTask);


                return View(pageOutput);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Index:" + ex.StackTrace);
                return new HttpStatusCodeResult(500);
            }
        }
	}
}