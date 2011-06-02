using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Entities
{
    public class MusicCollection
    {
        public virtual int Id { get; private set; }
        public virtual string MusicName { get; set; }

        public virtual IList<MusicFile> Files { get; set; }

        public MusicCollection()
        {
            Files = new List<MusicFile>();
        }

        public virtual void AddFile(MusicFile f)
        {
            f.MusicCollection = this;
            Files.Add(f);
        }
    }
}
