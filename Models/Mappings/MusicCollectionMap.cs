using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Entities;
using FluentNHibernate.Mapping;

namespace Models.Mappings
{
    public class MusicCollectionMap : ClassMap<MusicCollection>
    {
        public MusicCollectionMap()
        {
            Id(x => x.Id);
            Map(x => x.MusicName);
            HasMany(x => x.Files)
                .Cascade.AllDeleteOrphan();
                //.All()
                //.Inverse();
        }
    }
}
