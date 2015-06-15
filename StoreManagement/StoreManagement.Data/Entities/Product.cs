﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StoreManagement.Data.Entities
{
    public class Product: BaseEntity
    {

        [Display(Name = "Category")]
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [IgnoreDataMember]
        public string Type { get; set; }
        [IgnoreDataMember]
        public Boolean MainPage { get; set; }
        [IgnoreDataMember]
        public Boolean ImageState { get; set; }

        public virtual ICollection<ProductFile> ProductFiles { get; set; }

    }
}