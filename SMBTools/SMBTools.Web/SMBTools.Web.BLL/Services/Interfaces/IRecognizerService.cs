using Azure.AI.FormRecognizer.DocumentAnalysis;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.Common.Enums;

namespace SMBTools.Web.BLL.Services.Interfaces;

public interface IRecognizerService
{
    Task<FileModel> GetRecognizedFormAsync(FileModel file, ModelId modelId);
    Task SaveFileAsync(FileModel file);
    BuildDocumentModelOperation CreateModelRecognizer(string nameModel);
}