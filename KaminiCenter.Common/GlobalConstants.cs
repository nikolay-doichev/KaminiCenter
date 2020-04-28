namespace KaminiCenter.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "KaminiCenter";

        public const string AdministratorRoleName = "Administrator";

        public const string UserRoleName = "User";

        public const string CloudFolderForFireplacePhotos = "fireplace_photos";

        public const string CloudFolderForAccessoriesPhotos = "accessorie_photos";

        public const string CloudFolderForFinishedModelPhotos = "finishedModel_photos";

        public const string CloudFolderForProjectPhotos = "project_photos";

        public const int ItemsPerPage = 3;

        public const string AdminEmail = "nikolay.doichev@gmail.com";

        public const string NameForSendingEmails = "Екипът на Камини Център";

        public const string SubjectForSendingEmails = "Отговор на Ваш коментар в сайта на Камини Център";

        public const string MaxFileSizeErrorMessage = "Максималния размер на файла не може да превошава {0} mb.";

        public const string AllowedExtensionsErrorMessage = "Видът на файла не е в позволения формат";

        public static readonly string[] AllowedImageExtensions = { ".jpg", ".png", ".jpeg" };
    }
}
