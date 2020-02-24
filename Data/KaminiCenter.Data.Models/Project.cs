namespace KaminiCenter.Data.Models
{
    using KaminiCenter.Data.Common.Models;
    using KaminiCenter.Data.Models.Enums;

    public class Project : BaseDeletableModel<string>
    {
        public TypeLocation TypeLocation { get; set; }

        public TypeProject TypeProject { get; set; }

        public string ImagePath { get; set; }
    }
}
