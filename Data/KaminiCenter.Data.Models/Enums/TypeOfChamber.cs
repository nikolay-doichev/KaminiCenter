namespace KaminiCenter.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public enum TypeOfChamber
    {
        [Description("Базови горивни камери")]
        Basic = 1,

        [Description("Камери с вертикално отваряне")]
        VerticalОpening = 2,

        [Description("Камери с водна риза")]
        WaterJacket = 3,

        [Description("Чугунени печки")]
        IronStoves = 4,

        [Description("Чугунени врати")]
        IronDoors = 5,
    }
}
