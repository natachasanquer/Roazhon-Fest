using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using DAL;
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


            List<Evenement> evenements = ServiceEvenement.GetAll();
            foreach (Evenement evenement in evenements)
            {
                listes.Add(new EvenementViewModel(evenement));
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
            SelectList themesBO = new SelectList(ServiceTheme.GetAll(), "ID", "Libelle");
            EvenementViewModel evm = new EvenementViewModel();
            evm.themes = themesBO;
            return View(evm);
        }

        // POST: Evenement/Create
        [HttpPost]
        public ActionResult Create(EvenementViewModel eVM)
        {
            
            Image img = new Image();
            var fileI = Request.Files[0];
            if (fileI != null && fileI.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileI.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                if (!System.IO.File.Exists(path))
                {
                    fileI.SaveAs(path);
                    img.Url = fileName;
                }
            }

            img.ID = Guid.NewGuid();

            System.Diagnostics.Debug.WriteLine("Entrée dans la méthode de création de la classe Evenement.");
            Evenement evenement = eVM.Metier;
            evenement.Images = new List<Image>();
            evenement.Images.Add(img);
            evenement.ID = Guid.NewGuid();

            System.Security.Principal.IPrincipal u = System.Web.HttpContext.Current.User;
            EvenementUtilisateur evenementutilisateur = new EvenementUtilisateur() { Utilisateur = new Utilisateur() { Email = u.Identity.Name, ID = Guid.NewGuid() } };
            evenement.Utilisateurs = new List<EvenementUtilisateur>();
            evenement.Utilisateurs.Add(evenementutilisateur);

            try
            {
               ServiceEvenement.CreerEvenement(evenement);
                   
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
          
        }

        // GET: Evenement/Edit/5
        public ActionResult Edit(Guid id)
        {
            SelectList themesBO = new SelectList(ServiceTheme.GetAll(), "ID", "Libelle");
            EvenementViewModel evm = new EvenementViewModel(ServiceEvenement.GetAll().FirstOrDefault(l => l.ID == id));
            evm.themes = themesBO;
            return View(evm);
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
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Evenement/Delete/5
        public ActionResult Delete(Guid id)
        {
            Evenement evenement = ServiceEvenement.GetAll().FirstOrDefault(l => l.ID == id);
            return RedirectToAction("Index");
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
