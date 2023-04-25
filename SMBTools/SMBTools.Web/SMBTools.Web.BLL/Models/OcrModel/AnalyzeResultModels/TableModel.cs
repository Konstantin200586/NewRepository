using Newtonsoft.Json;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.TableModels;

namespace SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels;

public class TableModel
{
    [JsonProperty("rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("columnCount")]
    public string ColumnCount { get; set; }

    [JsonProperty("cells")]
    public List<CellModel> Cells { get; set; }
}