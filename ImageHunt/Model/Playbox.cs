using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHunt.Services;

namespace ImageHunt.Model
{
    public class Playbox : DbObject
    {
      public Game Game { get; set; }
      public LatLng[] Vectrex { get; set; }
      public Node.Node[] Nodes { get; set; }
    }
}
