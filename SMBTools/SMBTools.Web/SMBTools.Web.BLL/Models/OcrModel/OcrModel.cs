using Newtonsoft.Json;

namespace SMBTools.Web.BLL.Models.OcrModel;

public class OcrModel
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("createdDateTime")]
    public DateTime CreatedDateTime { get; set; }

    [JsonProperty("lastUpdatedDateTime")]
    public DateTime LastUpdatedDateTime { get; set; }

    [JsonProperty("analyzeResult")]
    public AnalyzeResultModel AnalyzeResult { get; set; }
}