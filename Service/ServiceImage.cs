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
    }
}
