using System;

public class Parking : IEntityIdentifiable
{
	public Parking()
	{
	}

    public Parking(Guid id, string tarif, int nbPlaces) : this()
    {
        this.Id = id;
        this.Tarif = tarif;
        this.NbPlaces = nbPlaces;
    }

    public Guid ID { get; set; }
    public string Tarif { get; set; }
    public int nbPlaces { get; set; }
}
