using System.Collections.Generic;
using AutoMapper;
using Digipolis.Web.Api;
using Digipolis.Web.SampleApi.Data.Entiteiten;
using Digipolis.Web.SampleApi.Models;

namespace Digipolis.Web.SampleApi.Logic.Mapping
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : base(nameof(AutoMapperProfileConfiguration))
        {
            CreateMap<Value, ValueDto>().ReverseMap();
            CreateMap<ValueType, ValueTypeDto>().ReverseMap();
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}