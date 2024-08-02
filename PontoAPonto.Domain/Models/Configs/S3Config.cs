namespace PontoAPonto.Domain.Models.Configs
{
    public class S3Config
    {
        public string BucketName { get; set; }
        public string ProfilePicturesDir { get; set; }
        public string FaceValidationPicturesDir { get; set; }
        public string DocumentPicturesDir { get; set; }
        public string CarLicenseDir { get; set; }
        public string DriversDir { get; set; }
        public string UsersDir { get; set; }
    }
}
