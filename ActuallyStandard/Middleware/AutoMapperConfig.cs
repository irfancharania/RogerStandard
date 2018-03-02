using System;
using ActuallyStandard.Localization;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Changelog;
using Microsoft.Extensions.Localization;

namespace ActuallyStandard.Middleware
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Dtos.WorkItemDto, WorkItemViewModel>()
                .ForMember(dest => dest.WorkItemTypeString
                        , opt => opt.ResolveUsing<TranslateResolver, string>(
                                                    src => GetLocalizationKey (typeof(Dtos.WorkItemTypeDto),  
                                                                        src.WorkItemType)));
        }

        private string GetLocalizationKey(Type type, int value) 
            => $"{type.Name}.{Enum.GetName(type, value)}";

        public class TranslateResolver : IMemberValueResolver<object, object, string, string>
        {
            private readonly IStringLocalizer<SharedResources> _localizer;
            public TranslateResolver(IStringLocalizer<SharedResources> localizer)
            {
                _localizer = localizer;
            }

            public string Resolve(object source
                                , object destination
                                , string sourceMember
                                , string destMember
                                , ResolutionContext context) 
                => _localizer[sourceMember];
        }
    }
}
