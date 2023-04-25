using Newtonsoft.Json;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels;

namespace SMBTools.Web.BLL.Models.OcrModel;

public class AnalyzeResultModel
{
    [JsonProperty("apiVersion")]
    public string ApiVersion { get; set; }

    [JsonProperty("modelId")]
    public string ModelId { get; set; }

    [JsonProperty("stringIndexType")]
    public string StringIndexType { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("pages")]
    public List<PageModel> Pages { get; set; }

    [JsonProperty("tables")]
    public List<TableModel> Tables { get; set; }

    [JsonProperty("paragraphs")]
    public List<ParagraphModel> Paragraphs { get; set; }

    [JsonProperty("styles")]
    public List<StyleModel> Styles { get; set; }
}