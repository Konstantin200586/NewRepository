using Newtonsoft.Json;

namespace SMBTools.Web.BLL.Models.OcrModel;

public class SpanModel
{
    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }
}