using AutoMapper;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Web.Areas.Admin.Models;
using HaberSitesi.Web.Models;

namespace HaberSitesi.Web.App_Start
{
    public static class AutomapperConfig
    {
        public static void Initialize()
        {
            CreateModelsToViewModels();
            CreateViewModelsToModels();
        }

        private static void CreateModelsToViewModels()
        {
            Mapper.CreateMap<Kategori, KategoriModel>();
            Mapper.CreateMap<Galeri, GaleriModel>();
            Mapper.CreateMap<Etiket, EtiketModel>();
            Mapper.CreateMap<Rol, RolModel>();
            Mapper.CreateMap<Haber, HaberModel>()
                .ForMember(x => x.Resim, option => option.Ignore())
                .ForMember(x => x.SecilenEtiketler, option => option.Ignore());
            Mapper.CreateMap<Haber, KoseYazisiModel>()
                .ForMember(x => x.Kategoriler, option => option.Ignore())
                .ForMember(x => x.Yazarlar, option => option.Ignore());
            Mapper.CreateMap<Yorum, YorumModel>();
        }

        private static void CreateViewModelsToModels()
        {
            Mapper.CreateMap<KategoriModel, Kategori>()
                .ForMember(x => x.Haberler, option => option.Ignore())
                .ForMember(x => x.SeoAd, option => option.Ignore());
            Mapper.CreateMap<GaleriModel, Galeri>()
                .ForMember(x => x.Resimler, option => option.Ignore());
            Mapper.CreateMap<EtiketModel, Etiket>()
                .ForMember(x => x.Haberler, options => options.Ignore());
            Mapper.CreateMap<RolModel, Rol>()
                .ForMember(x => x.Kullanicilar, option => option.Ignore());
            Mapper.CreateMap<HaberModel, Haber>()
                .ForMember(x => x.Etiketler, option => option.Ignore())
                .ForMember(x => x.Galeriler, option => option.Ignore())
                .ForMember(x => x.Yorumlar, option => option.Ignore())
                .ForMember(x => x.SeoBaslik, option => option.Ignore());
            Mapper.CreateMap<KoseYazisiModel, Haber>()
                .ForMember(x => x.Etiketler, option => option.Ignore())
                .ForMember(x => x.Galeriler, option => option.Ignore())
                .ForMember(x => x.Yorumlar, option => option.Ignore())
                .ForMember(x => x.SeoBaslik, option => option.Ignore());
            Mapper.CreateMap<YorumModel, Yorum>();
        }
    }
}