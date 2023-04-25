using Newtonsoft.Json;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels;

public class StyleModel
{
    [JsonProperty("confidence")]
    public int Confidence { get; set; }

    [JsonProperty("spans")]
    public List<SpanModel> Spans { get; set; }

    [JsonProperty("isHandwritten")]
    public bool IsHandwritten { get; set; }

}