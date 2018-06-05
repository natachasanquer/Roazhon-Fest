using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace Service
{
    public class ServiceEvenement : IDisposable
    {
        public static List<Evenement> GetAll()
        {
            List<Evenement> retour = null;

            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Evenements.Include("Theme").ToList();
            }
            return retour;
        }

        /// <summary>
        /// retoune le livre en BDD
        /// </summary>
        /// <param name="id">identifiant du livre</param>
        /// <returns></returns>
        public static Evenement Get(Guid id)
        {
            Evenement retour = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Evenements.Include("Theme").FirstOrDefault(l => l.ID == id);
            }
            return retour;
        }

        //surcharge, on la met en private car utilisée uniquement par le service
        private static Evenement Get(Guid id, ApplicationContext context)
        {
            return context.Evenements.Include("Theme").FirstOrDefault(l => l.ID == id);
        }

        public static void Insert(Evenement l)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Evenements.Add(l);
                context.SaveChanges();
            }

        }

        public static void Update(Guid Id)
        {
            Evenement evenement = Get(Id);
            using (ApplicationContext context = new ApplicationContext())
            {

                EntityState s = context.Entry(evenement).State;
                //on récupère le livre existant et lui passe les param du l récupéré sur la vue
                //utilisation de la méthode Get surchargée
                Evenement lExistant = Get(evenement.ID, context);
                lExistant.Date = evenement.Date;
                lExistant.Description= evenement.Description;
                lExistant.Duree= evenement.Duree;
                lExistant.Lieu = evenement.Lieu;
                lExistant.Nom = evenement.Nom;
                lExistant.Theme = evenement.Theme;

                context.SaveChanges();
            }
        }

        public void creerEvenement(Evenement evenement)
        {
            using (ApplicationContext context = new ApplicationContext())
            {

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

