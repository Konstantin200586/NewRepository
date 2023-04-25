using System.Text;
using AutoMapper;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.BLL.Models.OcrModel;
using SMBTools.Web.BLL.Services.Interfaces;
using SMBTools.Web.Common.Enums;

namespace SMBTools.Web.BLL.Services;

public class RecognizerService : IRecognizerService
{
    private const string ApiVersionValue = "2022-08-31";
    private const string StringIndexTypeValue = "utf16CodeUnit";
    private const string StatusValue = "succeeded";
    private const string ExtensionJson = "json";
    private const string NameJson = ".pdf.ocr";

    private readonly IMapper _mapper;
    private readonly KeyEndpointPairSetting _keyEndpointPairSetting;
    private readonly BlobStorageSettings _blobStorageSettings;

    public RecognizerService(IOptions<KeyEndpointPairSetting> keyEndpointPairSetting,
        IOptions<BlobStorageSettings> blobStorageSettings,
        IMapper mapper)
    {
        _mapper = mapper;
        _keyEndpointPairSetting = keyEndpointPairSetting.Value;
        _blobStorageSettings = blobStorageSettings.Value;
    }
    public async Task<FileModel> GetRecognizedFormAsync(FileModel file, ModelId modelId)
    {
        var keyCredential = new AzureKeyCredential(_keyEndpointPairSetting.KeyFormRecognizer);
        var uri = new Uri(_keyEndpointPairSetting.EndpointFormRecognizer);
        var recognizedRequest = new DocumentAnalysisClient(uri, keyCredential);
        var stream = new MemoryStream(file.Data, 0, file.Data.Length);
        var recognizedResponse = await
            recognizedRequest.AnalyzeDocumentAsync(WaitUntil.Completed, $"prebuilt-{modelId}", stream);
        var result = recognizedResponse.Value;

        var analyzeResult = _mapper.Map<AnalyzeResultModel>(result);
        analyzeResult.ApiVersion = ApiVersionValue;
        analyzeResult.StringIndexType = StringIndexTypeValue;

        var ocrModel = CreateOcrModel(analyzeResult);
        var ocrStringModel = JsonConvert.SerializeObject(ocrModel);

        var jsonFile = new FileModel
        {
            Name = GenerateJsonOcrName(file.Name),
            Extension = ExtensionJson,
            Data = Encoding.UTF8.GetBytes(ocrStringModel)
        };

        return jsonFile;
    }

    public BuildDocumentModelOperation CreateModelRecognizer(string nameModel)
    {
        var keyCredential = new AzureKeyCredential(_keyEndpointPairSetting.KeyFormRecognizer);
        var uri = new Uri(_keyEndpointPairSetting.EndpointFormRecognizer);
        var client = new DocumentModelAdministrationClient(uri, keyCredential);
        var blobContainerUri = new Uri(_blobStorageSettings.BlobContainerUri);
        var model = client.BuildDocumentModel(WaitUntil.Completed, blobContainerUri, DocumentBuildMode.Template, nameModel);
        return model;
    }

    public async Task SaveFileAsync(FileModel file)
    {
        var blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.DefaultContainer);
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

        var blobClientName = GenerateFileName(file.Extension, file.Name);
        var blobClient = blobContainerClient.GetBlobClient(blobClientName);

        var stream = new MemoryStream(file.Data, 0, file.Data.Length);
        await blobClient.UploadAsync(stream, true);
        stream.Close();
    }

    private string GenerateFileName(string extension, string fileName)
    {
        return $"{_blobStorageSettings.DefaultFileNamePrefix}_{fileName}.{extension}";
    }

    private string GenerateJsonOcrName(string name)
    {
        var nameJson = new StringBuilder();
        nameJson.Append(name);
        nameJson.Append(NameJson);
        var nameJsonString = nameJson.ToString();

        return nameJsonString;
    }

    private OcrModel CreateOcrModel(AnalyzeResultModel analyzeResult)
    {
        var ocrModel = new OcrModel
        {
            Status = StatusValue,
            CreatedDateTime = DateTime.UtcNow,
            LastUpdatedDateTime = DateTime.UtcNow,
            AnalyzeResult = analyzeResult
        };

        return ocrModel;
    }
}