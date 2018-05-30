using BO;
using System;

namespace BO
{
    public class Role : IEntityIdentifiable
    {
        public Role()
        {
        }

        public Role(Guid id, string libelle) : this()
        {
            this.ID = id;
            this.Libelle = libelle;
        }

        public Guid ID { get; set; }
        public string Libelle { get; set; }
    }

}