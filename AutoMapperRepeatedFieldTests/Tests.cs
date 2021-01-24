using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapperRepeatedFieldTests.Entities;
using AutoMapperRepeatedFieldTests.Protos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapperRepeatedFieldTests
{
    [TestClass]
    public class Tests
    {
        private Mapper GetMapper()
        {
            var conf = new MapperConfiguration(x =>
            {
                x.AddProfile<MyMappingConfig>();
            });

            return new Mapper(conf);
        }

        [TestMethod]
        public void Can_Map()
        {
            var bob = new Person
            {
                Id = 1,
                Pets = new List<Pet>
                {
                    new Pet
                    {
                        Name = "Rex"
                    }
                }
            };

            var mapper = GetMapper();
            var dto = mapper.Map<PersonDto>(bob);

            Assert.IsNotNull(dto);
            Assert.IsNotNull(dto.PetNames);
            Assert.AreEqual(1, dto.PetNames.Count);
            Assert.AreEqual("Rex", dto.PetNames.First());
        }

        [TestMethod]
        public void Can_Map_Projection()
        {
            var bob = new Person
            {
                Id = 1,
                Pets = new List<Pet>
                {
                    new Pet
                    {
                        Name = "Rex"
                    }
                }
            };

            var db = new MyDbContext();
            db.Set<Person>().Add(bob);
            db.SaveChanges();
            db.ChangeTracker.Clear();

            var mapper = GetMapper();
            var query = db.Set<Person>();
            var projectionResult = query.ProjectTo<PersonDto>(mapper.ConfigurationProvider);

            Assert.IsNotNull(projectionResult);
            Assert.AreEqual(1, projectionResult.Count());

            var dto = projectionResult.First();
            Assert.IsNotNull(dto);
            Assert.IsNotNull(dto.PetNames);
            Assert.AreEqual(1, dto.PetNames.Count);
            Assert.AreEqual("Rex", dto.PetNames.First());
        }
    }
}
