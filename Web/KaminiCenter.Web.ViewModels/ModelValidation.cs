namespace KaminiCenter.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class ModelValidation
    {
        public const string EmptyFieldLengthError = "Моля попълнете данните";

        public static class Fireplace
        {
            // Name Length const
            public const int NameMinLength = 2;
            public const int NameMaxLength = 80;

            // Power Length const
            public const int PowerMinLength = 3;
            public const int PowerMaxLength = 8;

            // Chimney Length const
            public const int ChimneyMinLength = 3;
            public const int ChimneyMaxLength = 5;

            // Size Length const
            public const int SizeMinLength = 3;
            public const int SizeMaxLength = 20;

            // Description max lenght
            public const int DescriptionMaxLength = 2000;

            // Error Message
            public const string DescriptionError = "Описанието на продукта трябва да е съставен максимум от {1} символа";
            public const string ChimneyError = "Описанието на Димоотвода трябва да е между {2} и {1} символи";
            public const string PowerError = "Мощността трябва да е между {2} и {1} символи";
            public const string NameError = "Името на продукта трябва да е между {2} и {1} символи";
            public const string SizeError = "Описанието на размерите на продукта трябва да е между {2} и {1} символи";
            public const string TypeOfChamberError = "Изберете тип на камерата!";
        }

        public static class Comment
        {
            // Content max lenght
            public const int ContentMaxLength = 1000;

            // Name Length const
            public const int NameMinLength = 2;
            public const int NameMaxLength = 80;

            // Error Message
            public const string ContentError = "Съдържанието на коментара може да е от максимум {1} символа";
            public const string NameError = "Името трябва да е между {2} и {1} символи";
        }

        public static class Finished_Model
        {
            // Name Length const
            public const int NameMinLength = 2;
            public const int NameMaxLength = 80;

            // Error Message
            public const string NameError = "Името на продукта трябва да е между {2} и {1} символи";
            public const string TypeOfChamberError = "Изберете тип на камерата!";
        }
    }
}
