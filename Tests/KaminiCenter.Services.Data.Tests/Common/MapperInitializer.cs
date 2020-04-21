namespace KaminiCenter.Services.Data.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(AllFireplaceViewModel).Assembly);
        }
    }
}
