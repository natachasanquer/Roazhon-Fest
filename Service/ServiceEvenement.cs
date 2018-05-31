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
    public static class ServiceEvenement
    {
        public static List<Evenement> GetAll()
        {
            List<Evenement> retour = null;


            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Evenements.ToList();

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
                retour = context.Evenements.FirstOrDefault(l => l.ID == id);

            }
            return retour;
        }

        //surcharge, on la met en private car utilisée uniquement par le service
        private static Evenement Get(Guid id, ApplicationContext context)
        {
            return context.Evenements.FirstOrDefault(l => l.ID == id);
        }

        public static void Insert(Evenement l)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Evenements.Add(l);
                context.SaveChanges();
            }

        }

        public static void Update(Evenement l)
        {
            using (ApplicationContext context = new ApplicationContext())
            {

                EntityState s = context.Entry(l).State;
                //on récupère le livre existant et lui passe les param du l récupéré sur la vue
                //utilisation de la méthode Get surchargée
                Evenement lExistant = Get(l.ID, context);
                lExistant.Date = l.Date;
                lExistant.Description= l.Description;
                lExistant.Duree= l.Duree;
                lExistant.Lieu = l.Lieu;
                lExistant.Nom = l.Nom;

                context.SaveChanges();
            }
        }
    }
}

