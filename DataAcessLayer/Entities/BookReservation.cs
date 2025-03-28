﻿using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entities
{
    public class BookReservation : BaseEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid StatusId { get; set; }
        public ReservationStatus Status { get; set; }//Navigation property
        public DateTime ReservationDate { get; set;}
        public DateTime ReturnDate { get; set; }
       
      
    }
}
