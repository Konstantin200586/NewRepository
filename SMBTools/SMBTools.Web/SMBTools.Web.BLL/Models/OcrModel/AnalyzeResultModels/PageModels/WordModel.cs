using Newtonsoft.Json;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.PageModels;

public class WordModel
{
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("polygon")]
    public float[] Polygon { get; set; }

    [JsonProperty("confidence")]
    public float Confidence { get; set; }

    [JsonProperty("span")]
    public SpanModel Span { get; set; }
}