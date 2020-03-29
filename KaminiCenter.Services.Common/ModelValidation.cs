using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Services.Common
{
    public static class ModelValidation
    {
        public static class Fireplace
        {
            //Name Length const
            public const int NameMinLength = 2;
            public const int NameMaxLength = 80;

            //Power Length const
            public const int PowerMinLength = 3;
            public const int PowerMaxLength = 8;

            //Chimney Length const
            public const int ChimneyMinLength = 3;
            public const int ChimneyMaxLength = 5;

            //Size Length const
            public const int SizeMinLength = 3;
            public const int SizeMaxLength = 20;

            //Description max lenght
            public const int DescriptionMaxLength = 1000;

            //Error Message
            public const string DescriptionError = "Описанието на продукта трябва да е между {0} и {1} символи";
            public const string ChimneyError = "Описанието на Димоотвода трябва да е между {0} и {1} символи";
            public const string PowerError = "Мощността трябва да е между {0} и {1} символи";
            public const string NameError = "Името на продукта трябва да е между {0} и {1} символи";
            public const string SizeError = "Описанието на размерите на продукта трябва да е между {0} и {1} символи";
            public const string TypeOfChamberError = "Изберете тип на камерата!";
        }
    }
}
