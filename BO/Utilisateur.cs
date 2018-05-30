using System;

public class Utilisateur : IEntityIdentifiable
{
	public Utilisateur()
	{
	}

    public Utilisateur(Guid id, string nom, string email, string password) : this()
    {
        this.Id = id;
        this.Nom = nom;
        this.Email = email;
        this.Password = password;
    }

    public Guid Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual ICollection<EvenementUtilisateur> Evenements { get; set; }

}
