using BO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BO
{
    public class Image : IEntityIdentifiable
    {
        public Image()
        {
        }

        public Image(Guid id, string url) : this()
        {
            this.ID = id;
            this.Url = url;
        }

        public Guid ID { get; set; }
        public string Url { get; set; }
    }
}