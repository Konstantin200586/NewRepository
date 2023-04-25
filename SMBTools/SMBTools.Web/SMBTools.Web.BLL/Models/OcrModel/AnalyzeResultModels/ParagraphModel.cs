using Newtonsoft.Json;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.ParagraphModels;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels;

public class ParagraphModel
{
    [JsonProperty("spans")]
    public List<SpanModel> Spans { get; set; }

    [JsonProperty("boundingRegions")]
    public List<BoundingRegionModel> BoundingRegions { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }
}