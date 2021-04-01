using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.Models
{
    public class PersonInfo
    {
        public Person Person { get; set; }
        public List<PersonHobby> PersonHobbies { get; set; }
    }
}
