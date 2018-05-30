using BO;
using System;

public class EvenementUtilisateur : IEntityIdentifiable
{
	public EvenementUtilisateur()
	{
	}

    public EvenementUtilisateur(Guid id, Evenement evenement, Utilisateur utilisateur, Role role) : this()
    {
        this.ID = id;
        this.Evenement = evenement;
        this.Utilisateur = utilisateur;
        this.Role = role;
    }

    public Guid ID { get; set; }
    public Evenement Evenement { get; set; }
    public Utilisateur Utilisateur { get; set; }
    public Role Role { get; set; }
}
