using AutoMapper;
using BLL.Model;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
           
            CreateMap<UserDetail, UserModel>().ReverseMap();
        }
    }
}
