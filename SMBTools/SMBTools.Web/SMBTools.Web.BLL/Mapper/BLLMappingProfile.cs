using AutoMapper;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using SMBTools.Contract.Models.Requests;
using SMBTools.Web.BLL.Models;
using SMBTools.Web.BLL.Models.OcrModel;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.PageModels;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.ParagraphModels;
using SMBTools.Web.BLL.Models.OcrModel.AnalyzeResultModels.TableModels;
using SMBTools.Web.DAL.DataModels;
using System.Drawing;

namespace SMBTools.Web.BLL.Mapper
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {
            CreateMap<AccountModel, AccountDataModel>().ReverseMap();
            CreateMap<LoginRequestDto, LoginModel>().ReverseMap();
            CreateMap<AnalyzeResult, AnalyzeResultModel>();
            CreateMap<DocumentTable, TableModel>();
            CreateMap<DocumentStyle, StyleModel>();
            CreateMap<DocumentParagraph, ParagraphModel>();
            CreateMap<DocumentPage, PageModel>()
                .ForMember(p=>p.Kind, d=>d.MapFrom(k => "document"));
            CreateMap<DocumentTableCell, CellModel>();
            CreateMap<DocumentWord, WordModel>()
                .ForMember(w => w.Polygon, d => d.MapFrom(s => s.BoundingPolygon.SelectMany(b => new[] { b.X, b.Y })));
            CreateMap<DocumentLine, LineModel>()
                .ForMember(w => w.Polygon, d => d.MapFrom(s => s.BoundingPolygon.SelectMany(b => new[] { b.X, b.Y })));
            CreateMap<DocumentSpan, SpanModel>()
                .ForMember(s => s.Offset, d => d.MapFrom(w => w.Index));
            CreateMap<BoundingRegion, BoundingRegionModel>()
                .ForMember(b => b.Polygon,
                    d => d.MapFrom(p => p.BoundingPolygon.SelectMany(bou => new[] { bou.X, bou.Y })));
        }
    }
}