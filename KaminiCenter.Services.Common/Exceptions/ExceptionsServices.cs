using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Services.Common.Exceptions
{
    public static class ExceptionsServices
    {
        public const string Null_Project_Id_ErrorMessage = "There isn't such Project with the given Id: {0}";

        public const string Null_Accessories_Id_ErrorMessage = "There isn't such Accessorie with the given Id: {0}";

        public const string Null_Fireplace_Id_ErrorMessage = "There isn't such fireplace with the given Id: {0}";

        public const string Null_FinishedModel_Id_ErrorMessage = "There isn't such Finished Model with the given Id: {0}";

        public const string Null_Product_Id_ErrorMessage = "There isn't such product with the given Id: {0}";

        public const string Null_PropertiesErrorMessage = "There is a property with null or whitespec value";
    }
}
