﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using Roazhon_Fest.Models;
using Service;

namespace Roazhon_Fest.Controllers
{
    public class EvenementController : Controller
    {

        // GET: Evenement
        public ActionResult Index()
        {
            List<EvenementViewModel> listes = new List<EvenementViewModel>();


            List<Evenement> livres = ServiceEvenement.GetAll();
            foreach (Evenement li in livres)
            {
                listes.Add(new EvenementViewModel(li));
            }

            return View(listes);
        }

        // GET: Evenement/Details/5
        public ActionResult Details(Guid? id)
        {
            EvenementViewModel lVM = new EvenementViewModel(ServiceEvenement.GetAll().FirstOrDefault(l => l.ID == id));
            return View(lVM);
        }

        // GET: Evenement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evenement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evenement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Evenement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(EvenementViewModel lVM)
        {
            try
            {
                //c'est le livreViewModel qui se save tout seul
                //il sait dans quel état il est : new ou existant
                //et le framework se charge du reste !
                lVM.Save();
                //pour reporter l'intelligence sur le ServiceLivres
                //il faut faire une fonction InsertOrUpdate dans LVM et le service 
                //et dans le service on fait if/else

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evenement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evenement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
