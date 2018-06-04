using BO;
using Roazhon_Fest.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roazhon_Fest.Controllers
{
    public class UtilisateurController : Controller
    {
        // GET: Utilisateur
        public ActionResult Index()
        {
            List<UtilisateurViewModel> utilisateursVM = new List<UtilisateurViewModel>();


            List<Utilisateur> utilisateurs = ServiceUtilisateur.GetAll();
            foreach (Utilisateur utilisateur in utilisateurs)
            {
                utilisateursVM.Add(new UtilisateurViewModel(utilisateur));
            }

            return View(utilisateursVM);
        }

        // GET: Utilisateur/Details/5
        public ActionResult Details(Guid? id)
        {
            UtilisateurViewModel uVM = new UtilisateurViewModel(ServiceUtilisateur.GetAll().FirstOrDefault(u => u.ID == id));
            return View(uVM);
        }

        // GET: Utilisateur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilisateur/Create
        [HttpPost]
        public ActionResult Create(UtilisateurViewModel uVM)
        {
            Utilisateur utilisateur = new Utilisateur()
            {
                Email = uVM.Email,
                ID = Guid.NewGuid(),
                Nom = uVM.Nom,
                Password = uVM.Password,
            };
            try
            {
                using (ServiceUtilisateur dal = new ServiceUtilisateur())
                {
                    dal.creerUtilisateur(utilisateur);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Utilisateur/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Utilisateur/Edit/5
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

        // GET: Utilisateur/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Utilisateur/Delete/5
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
