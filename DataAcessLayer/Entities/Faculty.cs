﻿using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entities
{
    public class Faculty : BaseEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid DesignationId { get; set; }
        public Designation Designation { get; set; }

    }

}


