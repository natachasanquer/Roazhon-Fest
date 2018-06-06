﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using Service;

namespace Roazhon_Fest.Models
{
    public class EvenementViewModel : ViewModel<Evenement>
    {
        public EvenementViewModel()
        {
            this.Metier = new Evenement();
           

            

        }
        public EvenementViewModel(Evenement l)
        {
            this.Metier = l;

        }

        [Display(Name = "Date")]
        public DateTime Date
        {
            get { return Metier.Date; }
            set { Metier.Date = value; }
        }

        [Display(Name = "Description")]
        public string Description
        {
            get { return Metier.Description; }
            set { Metier.Description = value; }
        }

        [Display(Name = "Date de fin")]
        public DateTime Duree
        {
            get
            { return Metier.Duree; }
            set { Metier.Duree = value; }
        }

        [Display(Name = "Lieu")]
        public string Lieu
        {
            get
            { return Metier.Lieu; }
            set { Metier.Lieu = value; }
        }

        [Display(Name = "Nom")]
        public string Nom
        {
            get
            { return Metier.Nom; }
            set { Metier.Nom = value; }
        }
        [Display(Name = "Theme")]
        public Theme Theme
        {
            get
            { return Metier.Theme; }
            set { Metier.Theme= value; }
        }
        public static List<EvenementViewModel> GetAll()
        {
            List<EvenementViewModel> retour = new List<EvenementViewModel>();
            List<Evenement> livres = ServiceEvenement.GetAll();
            foreach (Evenement li in livres)
            {
                retour.Add(new EvenementViewModel(li));
            }
            return retour;
        }

        public void Save()
        {
            if (this.ID == Guid.Empty)
            {
                //insert
                ServiceEvenement.CreerEvenement(this.Metier);
            }
            else
            {
                //update
                ServiceEvenement.Update(this.Metier.ID);
            }
        }

        /// <summary>
        /// retourne un livre ViewModel
        /// </summary>
        /// <param name="id">Identifiant nullable du livre</param>
        /// <returns>si id null, retourne un viewModel avec un livre initialisé; Si id a une valeur retourne le viewModel avec le livre en BDD
        /// </returns>
        public static EvenementViewModel Get(Guid? id)
        {
            EvenementViewModel retour = null;

            if (id.HasValue)
            {
                retour = new EvenementViewModel(ServiceEvenement.Get(id.Value));
            }
            else
            {
                Evenement l = new Evenement() { ID = Guid.Empty, Nom = "Default" };
                retour = new EvenementViewModel(l);
            }

            return retour;
        }

        public IEnumerable<SelectListItem> themes { get; set; }
    }
}