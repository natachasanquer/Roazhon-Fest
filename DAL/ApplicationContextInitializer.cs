using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BO;

namespace DAL
{
    public class ApplicationContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext contexte)
        {
            Utilisateur a = new Utilisateur(Guid.NewGuid(), "GROUSSARD", "titi@gmail.com", "mdp");
            contexte.Utilisateurs.Add(a);

            a = new Utilisateur(Guid.NewGuid(), "GABILLAUD", "jeje@gmail.com", "mdp");
            contexte.Utilisateurs.Add(a);

            a = new Utilisateur(Guid.NewGuid(), "HUGON", "jeje@hotmail.com", "mdp");
            contexte.Utilisateurs.Add(a);
            a = new Utilisateur(Guid.NewGuid(), "ALESSANDRI", "oliv@gmail.com", "mdp");
            contexte.Utilisateurs.Add(a);
            a = new Utilisateur(Guid.NewGuid(), "de QUAJOUX", "bebe@gmail.com", "mdp");
            contexte.Utilisateurs.Add(a);
        }
    }
}
