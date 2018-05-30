using System;

public class Theme : IEntityIdentifiable
{
	public Theme()
	{
	}

    public Theme(Guid id, string libelle) : this()
    {
        this.Id = id;
        this.Libelle = libelle;
    }

    public Guid Id { get; set; }
    public string Libelle { get; set; }
}
