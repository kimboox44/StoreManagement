﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using StoreManagement.Data.GeneralHelper;

namespace StoreManagement.Data.EntitiesWrapper
{
    public abstract class BaseWrapper : Drop
    {
        public String Layout { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String DetailLink { get; set; }
        public bool HasImage { get; set; }
        public int ImageId { get; set; }
        public String ImageLink { get; set; }

        public virtual DotLiquidEngineOutput RenderPage(string templateCode)
        {
            var liquidEngineOutput  = new DotLiquidEngineOutput();
            Template template = Template.Parse(templateCode);
            liquidEngineOutput.TemplateOutput =  template.Render(Hash.FromAnonymousObject(this));
            liquidEngineOutput.Layout = Layout;
            return liquidEngineOutput;
        }
    }
}
