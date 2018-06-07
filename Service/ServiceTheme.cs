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
    public class ServiceTheme : IDisposable
    {
        public static List<Theme> GetAll()
        {
            List<Theme> retour = null;

            using (ApplicationContext context = new ApplicationContext())
            {
                retour = context.Themes.ToList();
            }
            return retour;
        }

        /// <summary>
        /// retoune le livre en BDD
        /// </summary>
        /// <param name="id">identifiant du livre</param>
        /// <returns></returns>
        public static Theme Get(Guid id)
        {
            Theme retour = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                retour = Get(id, context);
            }
            return retour;
        }

        public static Theme Get(Guid id, ApplicationContext contexte)
        {
            Theme retour = null;
            retour = contexte.Themes.FirstOrDefault(l => l.ID == id);
            return retour;
        }
        //surcharge, on la met en private car utilisée uniquement par le service

        public static void Insert(Theme l)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Themes.Add(l);
                context.SaveChanges();
            }

        }

        public static void Update(Guid Id)
        {
            Theme theme = Get(Id);
            using (ApplicationContext context = new ApplicationContext())
            {

                EntityState s = context.Entry(theme).State;
                //on récupère le livre existant et lui passe les param du l récupéré sur la vue
                //utilisation de la méthode Get surchargée
                Theme lExistant = Get(theme.ID, context);
                lExistant.Libelle = theme.Libelle;

                context.SaveChanges();
            }
        }

        public void creerTheme(Theme theme)
        {
            using (ApplicationContext context = new ApplicationContext())
            {

                context.Themes.Add(theme);
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
