namespace ActuallyStandard.Localization
{
    public class SharedResources
    {
        public const string Hello = "Hello";
        public const string Bye = "Bye";
        public const string Language = "Language";
        public const string CookieValue = "CookieValue";
        public const string ResourceValue = "ResourceValue";

        public static class Sitemap {
            public const string ApplicationName = "Sitemap.ApplicationName";
            public const string Home = "Sitemap.Home";
            public const string Changelog = "Sitemap.Changelog";
            public const string ChangelogCreate = "Sitemap.ChangelogCreate";
            public const string ChangelogEdit = "Sitemap.ChangelogEdit";
            public const string ChangelogDetails = "Sitemap.ChangelogDetails";
        }

        public static class WorkItemTypeDto {
            public const string Bug = "WorkItemTypeDto.Bug";
            public const string Feature = "WorkItemTypeDto.Feature";
            public const string Miscellaneous = "WorkItemTypeDto.Miscellaneous";
        }

        public static class Release {
            public const string ReleaseVersion = "Release.ReleaseVersion";
            public const string ReleaseDate = "Release.ReleaseDate";
            public const string Authors = "Release.Authors";
            public const string WorkItems = "Release.WorkItems";
            public const string RecordVersion = "Release.RecordVersion";
        }

        public static class Error {
            public const string VersionNumberUnableToParse = "Error.VersionNumberUnableToParse";
            public const string VersionNumberIsRequired = "Error.VersionNumberIsRequired";
            public const string VersionNumberHasFewerThanTwoOrMoreThanFour = "Error.VersionNumberHasFewerThanTwoOrMoreThanFour";
            public const string VersionNumberIncorrectFormat = "Error.VersionNumberIncorrectFormat";
            public const string VersionNumberLessThanZero = "Error.VersionNumberLessThanZero";
            public const string VersionNumberGreaterThanMaximum = "Error.VersionNumberGreaterThanMaximum";
            public const string WorkItemIsRequired = "Error.WorkItemIsRequired";
            public const string WorkItemIdMustBePositive = "Error.WorkItemIdMustBePositive";
            public const string WorkItemIdUnknownError = "Error.WorkItemIdUnknownError";
            public const string WorkItemTypeInvalidValueProvided = "Error.WorkItemTypeInvalidValueProvided";
            public const string WorkItemDescriptionIsRequired = "Error.WorkItemDescriptionIsRequired";
            public const string WorkItemDescriptionMustNotBeShorterThan10Chars = "Error.WorkItemDescriptionMustNotBeShorterThan10Chars";
            public const string WorkItemDescriptionMustNotBeLongerThan100Chars = "Error.WorkItemDescriptionMustNotBeLongerThan100Chars";
            public const string ReleaseIsRequired = "Error.ReleaseIsRequired";
            public const string ReleaseIdIsRequired = "Error.ReleaseIdIsRequired";
            public const string ReleaseDateMustBeNewerThan2017 = "Error.ReleaseDateMustBeNewerThan2017";
            public const string ReleaseDateMustBeEqualToOrOlderThanToday = "Error.ReleaseDateMustBeEqualToOrOlderThanToday";
            public const string ReleaseAuthorIsRequired = "Error.ReleaseAuthorIsRequired";
            public const string ReleaseAuthorUnknownError = "Error.ReleaseAuthorUnknownError";
        }
    }
}
