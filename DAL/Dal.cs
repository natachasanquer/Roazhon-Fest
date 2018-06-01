using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public class Dal : IDisposable
    {

        private ApplicationContext appliContexte;

        public Dal()
        {
            appliContexte = new ApplicationContext();
        }

        public List<Evenement> ObtientTousLesRestaurants()
        {
            return appliContexte.Evenements.ToList();
        }
        public void creerEvenement(Evenement evenement)
        {
            appliContexte.Evenements.Add(evenement);
            appliContexte.SaveChanges();
        }
        public void Dispose()
        {
            appliContexte.Dispose();
        }
    }
}
