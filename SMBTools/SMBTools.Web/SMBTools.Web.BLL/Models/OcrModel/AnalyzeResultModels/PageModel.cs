using Newtonsoft.Json;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.PageModels;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels;

public class PageModel
{
    [JsonProperty("pageNumber")]
    public int PageNumber { get; set; }

    [JsonProperty("angle")]
    public float Angle { get; set; }

    [JsonProperty("width")]
    public float Width { get; set; }

    [JsonProperty("height")]
    public float Height { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("kind")]
    public string Kind { get; set; }

    [JsonProperty("words")]
    public List<WordModel> Words { get; set; }

    [JsonProperty("lines")]
    public List<LineModel> Lines { get; set; }

    [JsonProperty("spans")]
    public List<SpanModel> Spans { get; set; }
}