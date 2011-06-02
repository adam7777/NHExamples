using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Models.Entities;

namespace Models
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasManyToMany(x => x.Products)
               .Cascade.All()
               .Table("StoreProduct");
            HasMany(x => x.Staff)
                .Cascade.All()
                .Inverse();
        }
    }
}
