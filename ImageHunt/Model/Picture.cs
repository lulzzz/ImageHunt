using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHunt.Model
{
    public class Picture : DbObject
    {
      public byte[] Image { get; set; }
    }
}
