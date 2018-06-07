using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using BO;
using System.Data.Entity;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {

        // Votre contexte a été configuré pour utiliser une chaîne de connexion « ApplicationContext » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « DAL.ApplicationContext » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « ApplicationContext » dans le fichier de configuration de l'application.
        public ApplicationContext()
            : base("name=ApplicationContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationContext>(new ApplicationContextInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, DAL.Migrations.Configuration>());
        }


        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Evenement> Evenements { get; set; }

        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        public virtual DbSet<Theme> Themes { get; set; }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Role> RoleOC { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}