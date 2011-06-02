using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Models.Entities;

namespace Models.Mappings
{
    public class MusicFileMap : ClassMap<MusicFile>
    {
        public MusicFileMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Path);
            Map(x => x.Artist);
            Map(x => x.Title);
            Map(x => x.Album);
            Map(x => x.Date);
            Map(x => x.Genre);
            References(x => x.MusicCollection);
        }
    }
}
