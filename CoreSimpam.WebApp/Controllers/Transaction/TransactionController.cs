﻿using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.Transaction
{
    public class TransactionController : Controller
    {
        public TransactionController()
        {

        }
        public IActionResult Index()
        {
            var model = new TransactionViewModel();
            return View(model);
        }
    }
}
