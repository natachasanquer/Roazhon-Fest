using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service;
using System.ComponentModel.DataAnnotations;

namespace Roazhon_Fest.Models
{
    public class UtilisateurViewModel : ViewModel<Utilisateur>
    {
        public UtilisateurViewModel()
        {
            this.Metier = new Utilisateur();

        }


        public UtilisateurViewModel(Utilisateur l)
        {
            this.Metier = l;

        }

        [Display(Name = "Email")]
        public string Email
        {
            get { return Metier.Email; }
            set { Metier.Email= value; }
        }

        [Display(Name = "Nom")]
        public string Nom
        {
            get { return Metier.Nom; }
            set { Metier.Nom = value; }
        }

        [Display(Name = "Password")]
        public string Password
        {
            get
            { return Metier.Password; }
            set { Metier.Password = value; }
        }
        
        public static List<UtilisateurViewModel> GetAll()
        {
            List<UtilisateurViewModel> retour = new List<UtilisateurViewModel>();
            List<Utilisateur> utilisateurs = ServiceUtilisateur.GetAll();
            foreach (Utilisateur li in utilisateurs)
            {
                retour.Add(new UtilisateurViewModel(li));
            }
            return retour;
        }

        public void Save()
        {
            if (this.ID == Guid.Empty)
            {
                //insert
                ServiceUtilisateur.Insert(this.Metier);
            }
            else
            {
                //update
                ServiceUtilisateur.Update(this.Metier);
            }
        }

        /// <summary>
        /// retourne un utilisateur ViewModel
        /// </summary>
        /// <param name="id">Identifiant nullable du utilisateur</param>
        /// <returns>si id null, retourne un viewModel avec un utilisateur initialisé; Si id a une valeur retourne le viewModel avec le utilisateur en BDD
        /// </returns>
        public static UtilisateurViewModel Get(Guid? id)
        {
            UtilisateurViewModel retour = null;

            if (id.HasValue)
            {
                retour = new UtilisateurViewModel(ServiceUtilisateur.Get(id.Value));
            }
            else
            {
                Utilisateur l = new Utilisateur() { ID = Guid.Empty, Nom = "Default" };
                retour = new UtilisateurViewModel(l);
            }

            return retour;
        }
    }
}