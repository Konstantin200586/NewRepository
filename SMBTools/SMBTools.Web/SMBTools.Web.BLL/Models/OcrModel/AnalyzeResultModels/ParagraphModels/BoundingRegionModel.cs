using Newtonsoft.Json;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.ParagraphModels;

public class BoundingRegionModel
{
    [JsonProperty("pageNumber")]
    public string PageNumber { get; set; }

    [JsonProperty("polygon")]
    public float[] Polygon { get; set; }
}