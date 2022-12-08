using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notes.UI.Models
{
    public class PersonModel
    {

        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Age { get; set; }
    }
}