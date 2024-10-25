using AutoMapper;
using DEMO.DAL.Entity;
using DEMO.PL.Models;

namespace DEMO.PL.mapper
{
    public class mappingprofile : Profile
    {
        public mappingprofile()
        {
            CreateMap<Employeeviewmodle,Employee>().ReverseMap();
        }
    }
}
