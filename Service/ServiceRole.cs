using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceRole : IDisposable
    {

        public static Role getOrga()
        {
            Role role = new Role();
            using (ApplicationContext context = new ApplicationContext())
            {
                role = context.RoleOC.FirstOrDefault(r => r.Libelle == "Orga");
            }
            return role;
        }
        public static Role getConvive()
        {
            Role role = new Role();
            using (ApplicationContext context = new ApplicationContext())
            {
                role = context.RoleOC.FirstOrDefault(r => r.Libelle == "Convive");
            }
            return role;
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
