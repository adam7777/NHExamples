using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Entities
{
    public class MusicFile : File
    {
        public virtual string Artist { get; set; }
        public virtual string Title { get; set; }       
        public virtual string Album { get; set; }
        public virtual string Date { get; set; }
        public virtual string Genre { get; set; }
        public virtual MusicCollection MusicCollection { get; set; }
    }
}
