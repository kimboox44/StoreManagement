﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Service.Interfaces;

namespace StoreManagement.Service.Services
{
    public class BrandService : BaseService, IBrandService
    {
        public BrandService(string webServiceAddress) : base(webServiceAddress)
        {

        }
    }
}
