namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itializeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evenements",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Lieu = c.String(),
                        Date = c.DateTime(nullable: false),
                        Duree = c.DateTime(nullable: false),
                        Description = c.String(),
                        Nom = c.String(),
                        Theme_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Themes", t => t.Theme_ID)
                .Index(t => t.Theme_ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Url = c.String(),
                        Evenement_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Evenements", t => t.Evenement_ID)
                .Index(t => t.Evenement_ID);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EvenementUtilisateurs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Evenement_ID = c.Guid(),
                        Role_ID = c.Guid(),
                        Utilisateur_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Evenements", t => t.Evenement_ID)
                .ForeignKey("dbo.Roles", t => t.Role_ID)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_ID)
                .Index(t => t.Evenement_ID)
                .Index(t => t.Role_ID)
                .Index(t => t.Utilisateur_ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nom = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EvenementUtilisateurs", "Utilisateur_ID", "dbo.Utilisateurs");
            DropForeignKey("dbo.EvenementUtilisateurs", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.EvenementUtilisateurs", "Evenement_ID", "dbo.Evenements");
            DropForeignKey("dbo.Evenements", "Theme_ID", "dbo.Themes");
            DropForeignKey("dbo.Images", "Evenement_ID", "dbo.Evenements");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EvenementUtilisateurs", new[] { "Utilisateur_ID" });
            DropIndex("dbo.EvenementUtilisateurs", new[] { "Role_ID" });
            DropIndex("dbo.EvenementUtilisateurs", new[] { "Evenement_ID" });
            DropIndex("dbo.Images", new[] { "Evenement_ID" });
            DropIndex("dbo.Evenements", new[] { "Theme_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Roles");
            DropTable("dbo.EvenementUtilisateurs");
            DropTable("dbo.Themes");
            DropTable("dbo.Images");
            DropTable("dbo.Evenements");
        }
    }
}
