﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Runtime.Serialization.Json;

namespace GPS201107.Models.Grid
{
    /*public class JsonModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(bindingContext.ModelType);
                return serializer.ReadObject(controllerContext.HttpContext.Request.InputStream);
            }
            catch
            {
                return null;
            }
        }
    }*/
}