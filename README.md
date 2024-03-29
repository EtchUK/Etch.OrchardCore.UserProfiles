﻿# Etch.OrchardCore.UserProfiles

Module for [Orchard Core](https://github.com/OrchardCMS/OrchardCore) that provides [various features](#features) to aid with extending users functionality within Orchard Core.

## Build Status

[![Build Status](https://secure.travis-ci.org/etchuk/Etch.OrchardCore.UserProfiles.png?branch=master)](http://travis-ci.org/etchuk/Etch.OrchardCore.UserProfiles) [![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.UserProfiles.svg)](https://www.nuget.org/packages/Etch.OrchardCore.UserProfiles)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.1.0`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.1.0)).

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

When enabled this feature will make two new parts available `SubscriptionLevelPart` and `SubscriptionAccessPart`. `SubscriptionLevelPart` can be attached to either profile or groups to set a subscription level. Attaching the `SubscriptionAccessPart` to a content type allows content editors to restrict which subscription levels have access to view specific content items.

### Subscription Access Profile Groups

When enabled this feature will extend the subscriptions module to manage access to content items based on grouping subscription.

### Subscription Groups

When enabled this feature will extend the subscriptions module to allow assign subscription to subscription group.
This will then allow to add the groups to the user profile and limit the access to the content type by subscription group.
