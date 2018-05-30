using BO;
using System;

public class Theme : IEntityIdentifiable
{
	public Theme()
	{
	}

    public Theme(Guid id, string libelle) : this()
    {
        this.ID = id;
        this.Libelle = libelle;
    }

    public Guid ID { get; set; }
    public string Libelle { get; set; }
}
