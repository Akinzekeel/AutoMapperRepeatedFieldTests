using AutoMapper;
using AutoMapperRepeatedFieldTests.Entities;
using AutoMapperRepeatedFieldTests.Protos;
using Google.Protobuf.Collections;
using System.Linq;

namespace AutoMapperRepeatedFieldTests
{
    public class MyMappingConfig : Profile
    {
        public MyMappingConfig()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(x => x.PetNames, m => m.MapFrom(x => x.Pets.Select(pet => pet.Name)))
                .ReverseMap();

            // Supposed solution from https://github.com/AutoMapper/AutoMapper/issues/3325#issuecomment-593076419
            ForAllPropertyMaps(
               map => map.DestinationType.IsGenericType && map.DestinationType.GetGenericTypeDefinition() == typeof(RepeatedField<>),
               (map, options) => options.UseDestinationValue());
        }
    }
}
