using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperRepeatedFieldTests.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
    }
}
