using Etch.OrchardCore.UserProfiles;
using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK",
    Category = "Users",
    Description = "Adds profiles for users",
    Name = "User Profiles",
    Version = "0.3.3",
    Website = "https://etchuk.com"
)]

[assembly: Feature(
    Id = Constants.Features.Core,
    Name = "Profiles",
    Category = "Users",
    Description = "Adds profiles for users.",
    Dependencies = new string[] { Constants.Features.Grouping }
)]

[assembly: Feature(
    Id = Constants.Features.Grouping,
    Name = "Profile Groups",
    Category = "Users",
    Description = "Manage a collection of profiles.",
    Dependencies = new string[] { "OrchardCore.ContentFields" }
)]

[assembly: Feature(
    Id = Constants.Features.GroupField,
    Name = "Profile Group Field",
    Category = "Content",
    Description = "Adds a field for picking a Profile Group.",
    Dependencies = new string[] { Constants.Features.Grouping }
)]

[assembly: Feature(
    Id = Constants.Features.GroupOwnership,
    Name = "Profile Group Ownership",
    Category = "Users",
    Description = "Adds ownership of content by Profile Groups, including optional access restriction.",
    Dependencies = new string[] { "OrchardCore.ContentFields", Constants.Features.Core, Constants.Features.Grouping, Constants.Features.GroupField }
)]

[assembly: Feature(
    Id = Constants.Features.Subscriptions,
    Name = "Subscriptions",
    Category = "Users",
    Description = "Manage profile subscriptions.",
    Dependencies = new string[] { "OrchardCore.ContentFields" }
)]

[assembly: Feature(
    Id = Constants.Features.SubscriptionGroups,
    Name = "Subscription Groups",
    Category = "Users",
    Description = "Manage group of subscriptions.",
    Dependencies = new string[] { "OrchardCore.ContentFields" }
)]

[assembly: Feature(
    Id = Constants.Features.SubscriptionAccessGrouping,
    Name = "Subscription Access Profile Groups",
    Category = "Users",
    Description = "Authorises subscription access based on profile groups.",
    Dependencies = new string[] { "OrchardCore.ContentFields", Constants.Features.Subscriptions, Constants.Features.Grouping }
)]