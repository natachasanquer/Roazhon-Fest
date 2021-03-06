﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Service
{
    public class ServiceEvenement : IDisposable
    {
        public static List<Evenement> GetAll()
        {
            List<Evenement> retour = null;

            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Evenements.Include("Theme").Include("Images").ToList();
            }
            return retour;
        }


        public static Evenement Get(Guid id)
        {
            Evenement retour = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Evenements.Include("Theme").Include("Images").FirstOrDefault(l => l.ID == id);
            }
            return retour;
        }

        //surcharge, on la met en private car utilisée uniquement par le service
        private static Evenement Get(Guid id, ApplicationContext context)
        {
            return context.Evenements.Include("Theme").Include("Images").FirstOrDefault(l => l.ID == id);
        }

        public static void supprimerEvenement(Evenement evenement)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                ServiceImage.supprimerImageParEvenement(evenement,context);
                context.Evenements.Remove(evenement);
                context.SaveChanges();
            }

        }

        public static void Update(Evenement evenement)
        {
            Guid Id = evenement.ID;
            using (ApplicationContext context = new ApplicationContext())
            {

                EntityState s = context.Entry(evenement).State;
                //on récupère le livre existant et lui passe les param du l récupéré sur la vue
                //utilisation de la méthode Get surchargée
                Evenement eExistant = Get(evenement.ID, context);
                eExistant.Date = evenement.Date;
                eExistant.Description= evenement.Description;
                eExistant.Duree= evenement.Duree;
                eExistant.Lieu = evenement.Lieu;
                eExistant.Nom = evenement.Nom;
                Theme theme = ServiceTheme.Get(evenement.Theme.ID, context);

                eExistant.Theme = theme;

                context.SaveChanges();
            }
        }

        public static void CreerEvenement(Evenement evenement)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                string user = HttpContext.Current.User.Identity.GetUserId();


                Theme theme = context.Themes.FirstOrDefault(t => t.ID == evenement.Theme.ID);
                evenement.Theme = theme;

                Role role = context.RoleOC.FirstOrDefault(r => r.Libelle == "Orga");

                EvenementUtilisateur utilisateurEvenement = new EvenementUtilisateur();
                utilisateurEvenement.Role = role;
                utilisateurEvenement.Evenement = evenement;
                utilisateurEvenement.ID = Guid.NewGuid();

                evenement.Utilisateurs.Add(utilisateurEvenement);

                context.Evenements.Add(evenement);
                context.SaveChanges();
            }
        }
        public void Dispose()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Dispose();
            }
        }
    }
}

