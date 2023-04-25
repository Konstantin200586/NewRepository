namespace SMBTools.Web.BLL.Models;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; }
    public string DefaultContainer { get; set; }
    public string DefaultFileNamePrefix { get; set; }
    public string BlobContainerUri { get; set; }
}