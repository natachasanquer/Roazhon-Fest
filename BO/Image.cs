using System;

public class Image : IEntityIdentifiable
{
	public Image()
	{
	}

    public Image(Guid id, string url) : this()
    {
        this.Id = id;
        this.Url = url;
    }

    public Guid Id { get; set; }
    public string Url { get; set; }
}
