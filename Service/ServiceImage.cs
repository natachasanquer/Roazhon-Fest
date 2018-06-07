using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceImage : IDisposable
    {

        public static void CreerImage(Image image)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Images.Add(image);
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

        internal static void supprimerImageParEvenement(Guid evenement, ApplicationContext context)
        {
            using (context)
            {
                List<Image> images = getAllParEvenement(evenement,context);
                foreach (Image image in images)
                {
                    context.Images.Remove(image);
                }
            }
        }

        private static List<Image> getAllParEvenement(Guid idEvenement, ApplicationContext context)
        {
            List<Image> images = new List<Image>();
            using (context)
            {
                images = context.Images.Where(b => b.EvenementID == idEvenement).ToList();
            }
            return images; 
        }
    }
}
