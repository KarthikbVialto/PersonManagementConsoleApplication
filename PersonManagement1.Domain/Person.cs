using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement1.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int DobId { get; set; }
        public DOB DateOfBirth { get; set; }


    }
}
