﻿using Barangay_Management_Information_System.Classess;
using Barangay_Management_Information_System.Models.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_Management_Information_System.Controllers
{
    public class AdminAccountController : Controller
    {
        private DBEntities entities = new DBEntities();
        // GET: AdminAccount
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateAccount()
        {
            TempData["user-profile-photo"] = DisplayPictureRetriever.GetDisplayPicture(User.Identity.GetUserId(), entities);
            TempData["alert-present"] = "0";
            try
            {
                return View(entities.ResidentsInformations.OrderBy(m => m.LastName).ToArray());
            }
            catch(Exception e)
            {
                TempData["alert-present"] = "1";
                TempData["alert-type"] = "alert-danger";
                TempData["alert-header"] = "Error";
                TempData["alert-msg"] = "Information cannot be retrieved at this time, please try again later";
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult RegisterResident(string residentId)
        {
            return PartialView("_RegisterResident", entities.ResidentsInformations.Where(m => m.ResidentId == residentId).FirstOrDefault());
        }
    }
}