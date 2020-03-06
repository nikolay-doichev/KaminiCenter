using AutoMapper;
using KaminiCenter.Data.Models;
using KaminiCenter.Services.Models.Fireplace;
using KaminiCenter.Web.ViewModels.Fireplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaminiCenter.Web.MappingConfiguration
{
    public class KaminiCenterProfile : Profile
    {
        public KaminiCenterProfile()
        {
            this.CreateMap<FireplaceInputModel, AddFireplaceInputModel>();
            this.CreateMap<AllFireplaceViewModel, AllFireplaceViewModel>();
        }
    }
}
