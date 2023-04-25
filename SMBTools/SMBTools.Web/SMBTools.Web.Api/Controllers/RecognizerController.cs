using Microsoft.AspNetCore.Mvc;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.BLL.Services.Interfaces;
using SMBTools.Web.Common.Enums;

namespace SMBTools.Web.Api.Controllers
{
    public class RecognizerController : BaseController
    {
        private readonly IRecognizerService _recognizerService;

        public RecognizerController(ILogger<BaseController> logger,
            IRecognizerService recognizerService) : base(logger)
        {
            _recognizerService = recognizerService;
        }

        [HttpPost("recognizedForm")]
        public async Task<IActionResult> GetRecognizedFormAsync([FromBody] FileModel file, ModelId modelId)
        {
            return await ProcessRequest<object>(() => _recognizerService.GetRecognizedFormAsync(file, modelId));
        }

        [HttpPost("allSave")]
        public async Task<IActionResult> SaveRecognizedFormAsync([FromBody] FileRecognizerFormPairModel fileRecognizerFormPairModel)
        {
            return await ProcessRequest<object>(() => _recognizerService.SaveFileAsync(fileRecognizerFormPairModel.FileModel));
        }

        [HttpGet("createRecognizerModel")]
        public IActionResult DocumentModelAdministrationClientFactory(string nameModel)
        {
            var doc = _recognizerService.CreateModelRecognizer(nameModel);
            return Ok(doc);
        }
    }
}