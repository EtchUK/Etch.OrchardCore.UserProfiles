using Etch.OrchardCore.UserProfiles;
using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK",
    Category = "Users",
    Description = "Adds profiles for users",
    Name = "User Profiles",
    Version = "0.1.0",
    Website = "https://etchuk.com"
)]

[assembly: Feature(
    Id = Constants.Features.Core,
    Name = "Profiles",
    Category = "Users",
    Description = "Adds profiles for users."
)]

[assembly: Feature(
    Id = Constants.Features.Grouping,
    Name = "Profile Groups",
    Category = "Users",
    Description = "Manage a collection of profiles.",
    Dependencies = new string[] { "OrchardCore.ContentFields" }
)]

[assembly: Feature(
    Id = Constants.Features.Subscriptions,
    Name = "Subscriptions",
    Category = "Users",
    Description = "Manage profile subscriptions.",
    Dependencies = new string[] { "OrchardCore.ContentFields" }
)]