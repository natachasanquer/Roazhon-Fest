using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;

namespace Roazhon_Fest.Models
{
    public class ViewModel<T> where T : IEntityIdentifiable
    {
        public T Metier { get; protected set; }

        public Guid ID
        {
            get { return this.Metier.ID; }

            set
            {
                this.Metier.ID = value;
            }
        }

    }
}