namespace KaminiCenter.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public enum GroupType
    {
        [Description("Камери")]
        Fireplace = 1,

        [Description("Проекти")]
        Project = 2,

        [Description("Модели")]
        Finished_Models = 3,

        [Description("Аксесоари")]
        Accessories = 4,
    }
}
