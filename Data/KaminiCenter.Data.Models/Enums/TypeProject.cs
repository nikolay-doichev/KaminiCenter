namespace KaminiCenter.Data.Models.Enums
{
    using System.ComponentModel;

    public enum TypeProject
    {
        [Description("Класически")]
        Classic = 1,

        [Description("Модерен")]
        Modern = 2,

        [Description("Рустикални")]
        Rustic = 3,

        [Description("Тип Рамка")]
        TypeFrame = 4,
    }
}
