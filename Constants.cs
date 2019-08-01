namespace Etch.OrchardCore.UserProfiles
{
    public class Constants
    {
        public const string ContentTypeName = "Profile";
        public const string ContentGroupingTypeName = "Grouping";
        public const string ContentSubscriptionTypeName = "Subscription";
        public const string ContentSubscriptionGroupTypeName = "SubscriptionGroup";

        public static class Features
        {
            public const string Core = "Etch.OrchardCore.Profiles";
            public const string Grouping = "Etch.OrchardCore.Profiles.Grouping";
            public const string GroupField = "Etch.OrchardCore.Profiles.GroupField";
            public const string GroupOwnership = "Etch.OrchardCore.Profiles.GroupOwnership";
            public const string Subscriptions = "Etch.OrchardCore.Profiles.Subscriptions";
            public const string SubscriptionGroups = "Etch.OrchardCore.Profiles.SubscriptionGroups";
            public const string SubscriptionAccessGrouping = "Etch.OrchardCore.Profiles.SubscriptionAccessGrouping";
        }
    }
}
