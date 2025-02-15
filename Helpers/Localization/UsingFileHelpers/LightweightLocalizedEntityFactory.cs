﻿using System;
using UniversalUnity.Helpers.Localization.Enums;
using UniversalUnity.Helpers.Logs;
using UniversalUnity.Helpers.Parsing.UsingFileHelpers;

namespace UniversalUnity.Helpers.Localization.UsingFileHelpers
{
    public class LightweightLocalizedEntityFactory :
        IParsedLightweightEntityFactory<ParsedLocalizedEntity,
            LightweightLocalizedEntityFactory.LocalizedLightweightEntity>
    {
        private readonly ETextLanguage _language;

        public LightweightLocalizedEntityFactory(ETextLanguage language)
        {
            _language = language;
        }

        public LocalizedLightweightEntity MakeLightweight(ParsedLocalizedEntity csvParsedEntity)
        {
            return new LocalizedLightweightEntity(csvParsedEntity, _language);
        }

        public sealed class LocalizedLightweightEntity : AParsedLightweightEntity
        {
            public string Text { get; }

            public LocalizedLightweightEntity(ParsedLocalizedEntity parsedEntity, ETextLanguage language) : base(
                parsedEntity)
            {
                switch (language)
                {
                    case ETextLanguage.Undefined:
                        LogHelper.LogWarning("Cannot make lightweight entity due undefined localization.",
                            nameof(LocalizedLightweightEntity));
                        return;
                    case ETextLanguage.Russian:
                        Text = parsedEntity.RussianLocale;
                        break;
                    case ETextLanguage.English:
                        Text = parsedEntity.EnglishLocale;
                        break;
                    case ETextLanguage.Japan:
                        Text = parsedEntity.JapanLocale;
                        break;
                    case ETextLanguage.Chinese:
                        Text = parsedEntity.ChineseLocale;
                        break;
                    case ETextLanguage.ChineseTraditional:
                        Text = parsedEntity.ChineseTraditionalLocale;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public override string ToString()
            {
                return $"{base.ToString()} Text: {Text}";
            }
        }
    }
}