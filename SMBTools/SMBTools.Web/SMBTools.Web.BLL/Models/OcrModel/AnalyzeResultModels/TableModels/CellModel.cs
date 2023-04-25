using Azure.AI.FormRecognizer.DocumentAnalysis;
using Newtonsoft.Json;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.ParagraphModels;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.TableModels;

public class CellModel
{
    [JsonProperty("kind")]
    public string Kind { get; set; }

    [JsonProperty("rowIndex")]
    public int RowIndex { get; set; }

    [JsonProperty("columnIndex")]
    public int ColumnIndex { get; set; }

    [JsonProperty("columnSpan")]
    public int ColumnSpan { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("boundingRegions")]
    public List<BoundingRegionModel> BoundingRegions { get; set; }

    [JsonProperty("spans")]
    public List<SpanModel> Spans { get; set; }
}