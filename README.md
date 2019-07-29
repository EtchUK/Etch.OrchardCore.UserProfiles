# Etch.OrchardCore.UserProfiles

Module for [Orchard Core](https://github.com/OrchardCMS/OrchardCore) that maps users to a profile content type. This enables content parts/fields to be defined for a user via their profile.

## Build Status

_Internal module that isn't currently publically available._

## Orchard Core Reference

This module is referencing the beta 3 build of Orchard Core ([`1.0.0-beta3-71077`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.0.0-beta3-71077)).

## Installing

This module is available on our private NuGet feed, search "Etch.OrchardCore.UserProfiles" within NuGet package manager in order to install the module within your Orchard Core site project. Your project must be configured to use our private NuGet feed otherwise no results will be returned.

## Features
By default when enabling the module a new "Profile" content type will be defined. The current stable version of Orchard Core doesn't have a way to identify when users have been created, however this will be [available in the next release](https://github.com/OrchardCMS/OrchardCore/commit/58045f241c3bc0fb6692ae873fbca340098eb944).

### Profile Groups

When enabled this feature will make a new "ProfileGroup" part available. When attached to a content type the content item can manage a list of profiles. Profiles can only be associated to a single profile group.

### Profile Group Field

This feature adds a new field which allows the editor to pick from content items with `ProfileGroupPart`.

### Profile Group Ownership

This feature adds the ability for Profile Groups to own Content Items through `ProfileGroupOwnershipPart` which uses a `ProfileGroupField` as `OwnedByGroup` to manage this relationship.

It has settings which allow configuration at the Type level for access restriction features with 3 different settings:

- None: no access restriction
- Type: all items of this type will check that the user belongs to the group(s) specified
- Item: adds a checkbox for the editor on the item to enabled/disable access restriction

### Subscriptions

When enabled this feature will make two new parts available "SubscriptionLevelPart" and "SubscriptionAccessPart".   
"SubscriptionLevelPart" can be attached to either profile or groups to set a subscription level.   
Attaching the "SubscriptionAccessPart" to a content type allows content editors to restrict which subscription levels have access to view specific content items.

### Subscription Access Profile Groups

When enabled this feature will extend the subscriptions module to manage access to content items based on grouping subscription.

