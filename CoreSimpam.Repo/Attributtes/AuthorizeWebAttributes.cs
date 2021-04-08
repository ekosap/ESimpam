﻿using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeWebAttributes : Attribute, IAuthorizationFilter
    {
        public int AccessLevel { get; set; }
        /// <summary>  
        /// This will Authorize User  
        /// </summary> 
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                IRoleScreenAccessRepo services = (IRoleScreenAccessRepo)context.HttpContext.RequestServices.GetService(typeof(IRoleScreenAccessRepo));
                var controller = context.RouteData.Values["controller"].ToString();
                var action = context.RouteData.Values["action"].ToString();
                var reqMethode = context.HttpContext.Request.Method;
                var result = services.AllowPermission(controller, action, reqMethode, AccessLevel);
                if (result.status)
                    return;
            }
            context.Result = new OkObjectResult(new Metadata() { status = false, data = "Your Access Limited" });
        }
    }
}