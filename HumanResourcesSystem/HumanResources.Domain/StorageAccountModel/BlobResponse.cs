namespace HumanResources.Domain.StorageAccountModel
{
    public class BlobResponse
    {
        public bool IsSuccess { get; set; }

        public string FileName { get; set; }  
        
        public string Message { get; set; }
    }
}
