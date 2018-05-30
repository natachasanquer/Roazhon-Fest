using System;

public class Evenement : IEntityIdentifiable
{
	public Evenement()
	{
	}

    public Evenement(Guid id, string lieu, DateTime date, DateTime duree, string description, string nom, Theme theme) : this()
    {
        this.Id = id;
        this.Lieu = lieu;
        this.Date = date;
        this.Duree = duree;
        this.Description = description;
        this.Nom = nom;
        this.Theme = theme;
    }

    public Guid Id { get; set; }
    public string Lieu { get; set; }
    public string Date { get; set; }
    public string Duree { get; set; }
    public string Description { get; set; }
    public string Nom { get; set; }
    public Theme Theme { get; set; }
    public virtual ICollection<Image> Images { get; set; }
    public virtual ICollection<EvenementUtilisateur> Utilisateurs { get; set; }
}
