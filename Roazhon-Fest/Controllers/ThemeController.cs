using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roazhon_Fest.Models;
using BO;

namespace Roazhon_Fest.Controllers
{
    public class ThemeController : Controller
    {
        // GET: Theme
        public ActionResult Index()
        {
            List<ThemeViewModel> themesVM = new List<ThemeViewModel>();
            List<Theme> themes = ServiceTheme.getAll();
            foreach (Theme theme in themes)
            {
                themesVM.Add(new ThemeViewModel(theme));
            }

            return View(themesVM);
        }

        // GET: Theme/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Theme/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Theme/Create
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

        // GET: Theme/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Theme/Edit/5
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

        // GET: Theme/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Theme/Delete/5
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
