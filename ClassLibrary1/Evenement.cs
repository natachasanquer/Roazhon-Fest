using BO;
using System;
using System.Collections.Generic;

public class Evenement : IEntityIdentifiable
{
	public Evenement()
	{
	}

    public Evenement(Guid id, string lieu, DateTime date, DateTime duree, string description, string nom, Theme theme) : this()
    {
        this.ID = id;
        this.Lieu = lieu;
        this.Date = date;
        this.Duree = duree;
        this.Description = description;
        this.Nom = nom;
        this.Theme = theme;
    }

    public Guid ID { get; set; }
    public string Lieu { get; set; }
    public DateTime Date { get; set; }
    public DateTime Duree { get; set; }
    public string Description { get; set; }
    public string Nom { get; set; }
    public Theme Theme { get; set; }
    public virtual ICollection<Image> Images { get; set; }
    public virtual ICollection<EvenementUtilisateur> Utilisateurs { get; set; }
}
