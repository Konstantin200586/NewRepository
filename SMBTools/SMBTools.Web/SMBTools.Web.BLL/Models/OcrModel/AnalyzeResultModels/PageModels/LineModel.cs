using Newtonsoft.Json;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.PageModels;

public class LineModel
{
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("polygon")]
    public float[] Polygon { get; set; }

    [JsonProperty("spans")]
    public List<SpanModel> Spans { get; set; }
}