using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Controllers
{
    public abstract class WebBaseController : Controller
    {

        protected void ShowMessage(bool result, string message)
        {
            TempData["message-type"] = result;
            TempData["message-content"] = message;
        }

    }
}
