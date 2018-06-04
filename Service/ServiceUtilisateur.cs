using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceUtilisateur : IDisposable
    {
        private ApplicationContext appliContexte;

        public static List<Utilisateur> GetAll()
        {
            List<Utilisateur> retour = null;

            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Utilisateurs.ToList();
            }
            return retour;
        }

        /// <summary>
        /// retoune le livre en BDD
        /// </summary>
        /// <param name="id">identifiant du livre</param>
        /// <returns></returns>
        public static Utilisateur Get(Guid id)
        {
            Utilisateur retour = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Utilisateurs.FirstOrDefault(l => l.ID == id);
            }
            return retour;
        }

        //surcharge, on la met en private car utilisée uniquement par le service
        private static Utilisateur Get(Guid id, ApplicationContext context)
        {
            return context.Utilisateurs.FirstOrDefault(l => l.ID == id);
        }

        public static void Insert(Utilisateur l)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Utilisateurs.Add(l);
                context.SaveChanges();
            }

        }

        public static void Update(Utilisateur l)
        {
            using (ApplicationContext context = new ApplicationContext())
            {

                EntityState s = context.Entry(l).State;
                //on récupère le livre existant et lui passe les param du l récupéré sur la vue
                //utilisation de la méthode Get surchargée
                Utilisateur lExistant = Get(l.ID, context);
                lExistant.Email = l.Email;
                lExistant.Evenements= l.Evenements;
                lExistant.ID= l.ID;
                lExistant.Nom= l.Nom;
                lExistant.Password= l.Password;

                context.SaveChanges();
            }
        }

        public void creerUtilisateur(Utilisateur evenement)
        {
            appliContexte.Utilisateurs.Add(evenement);
            appliContexte.SaveChanges();
        }
        public void Dispose()
        {
            appliContexte.Dispose();
        }
    }
}
