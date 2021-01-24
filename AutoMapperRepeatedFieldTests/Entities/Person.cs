using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperRepeatedFieldTests.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
