using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Entities
{
    public class File
    {
        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual string Path { get; set; }
    }
}
