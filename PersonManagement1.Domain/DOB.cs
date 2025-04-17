﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement1.Domain
{
    public class DOB
    {
        public int Id { get; set; }
        public int Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Person Person { get; set; }
        public int PersonId { get; set; }

    }
}
