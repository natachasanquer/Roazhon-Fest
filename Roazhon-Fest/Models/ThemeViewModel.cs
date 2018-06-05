using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BO;
using Service;

namespace Roazhon_Fest.Models
{
    public class ThemeViewModel : ViewModel<Theme>
    {
        public ThemeViewModel()
        {
            this.Metier = new Theme();

        }
        public ThemeViewModel(Theme l)
        {
            this.Metier = l;

        }
        
        [Display(Name = "Theme")]
        public string Theme
        {
            get
            { return Metier.Libelle; }
            set { Metier.Libelle = value; }
        }
        public static List<ThemeViewModel> GetAll()
        {
            List<ThemeViewModel> retour = new List<ThemeViewModel>();
            List<Theme> livres = ServiceTheme.GetAll();
            foreach (Theme li in livres)
            {
                retour.Add(new ThemeViewModel(li));
            }
            return retour;
        }

        public void Save()
        {
            if (this.ID == Guid.Empty)
            {
                //insert
                ServiceTheme.Insert(this.Metier);
            }
            else
            {
                //update
                ServiceTheme.Update(this.Metier.ID);
            }
        }

        /// <summary>
        /// retourne un livre ViewModel
        /// </summary>
        /// <param name="id">Identifiant nullable du livre</param>
        /// <returns>si id null, retourne un viewModel avec un livre initialisé; Si id a une valeur retourne le viewModel avec le livre en BDD
        /// </returns>
        public static ThemeViewModel Get(Guid? id)
        {
            ThemeViewModel retour = null;

            if (id.HasValue)
            {
                retour = new ThemeViewModel(ServiceTheme.Get(id.Value));
            }
            else
            {
                Theme l = new Theme() { ID = Guid.Empty, Libelle = "Default" };
                retour = new ThemeViewModel(l);
            }

            return retour;
        }
    }
}