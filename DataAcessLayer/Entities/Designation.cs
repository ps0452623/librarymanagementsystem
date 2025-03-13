using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAcessLayer.Entities
{
    public class Designation: BaseEntity
    {
         public string Name { get; set; }

    }
}
