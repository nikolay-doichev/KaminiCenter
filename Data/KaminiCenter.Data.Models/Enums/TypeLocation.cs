namespace KaminiCenter.Data.Models.Enums
{
    using System.ComponentModel;

    public enum TypeLocation
    {
        [Description("На ъгъл")]
        Corner = 1,

        [Description("На права стена")]
        StraightWall = 2,
    }
}
