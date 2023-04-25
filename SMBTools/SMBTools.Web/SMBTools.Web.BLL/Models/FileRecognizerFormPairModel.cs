using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace SMBTools.Web.BLL.Models;

public class FileRecognizerFormPairModel
{
    public FileModel FileModel { get; set; }
    public object AnalyzeResult { get; set; }
}