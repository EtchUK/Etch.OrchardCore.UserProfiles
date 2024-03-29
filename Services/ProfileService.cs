﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Indexes;
using Etch.OrchardCore.UserProfiles.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Environment.Shell;
using OrchardCore.Users;
using YesSql;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public class ProfileService : IProfileService
    {
        #region Dependencies

        private readonly ISession _session;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly UserManager<IUser> _userManager;

        #endregion

        #region Constructor

        public ProfileService(ISession session, IShellHost shellHost, ShellSettings shellSettings, UserManager<IUser> userManager)
        {
            _session = session;
            _shellSettings = shellSettings;
            _shellHost = shellHost;
            _userManager = userManager;
        }

        #endregion

        #region Implementation

        public async Task<ContentItem> CreateAsync(IUser user)
        {
            var shellScope = await _shellHost.GetScopeAsync(_shellSettings);
            ContentItem contentItem = null;

            await shellScope.UsingAsync(async scope =>
            {
                var contentManager = scope.ServiceProvider.GetRequiredService<IContentManager>();
                contentItem = await contentManager.NewAsync(Constants.ContentTypeName);
                contentItem.DisplayText = user.UserName;

                var profile = contentItem.As<ProfilePart>();
                profile.UserIdentifier = await _userManager.GetUserIdAsync(user);
                contentItem.Apply(nameof(ProfilePart), profile);

                ContentExtensions.Apply(contentItem, contentItem);

                await contentManager.CreateAsync(contentItem);
                await contentManager.PublishAsync(contentItem);
            });

            return contentItem;
        }

        public async Task<ContentItem> GetAsync(IUser user)
        {
            var userIdentifier = await _userManager.GetUserIdAsync(user);

            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.Latest && x.ContentType == Constants.ContentTypeName)
                                      .With<ProfilePartIndex>(x => x.UserIdentifier == userIdentifier)
                                      .FirstOrDefaultAsync();

            return contentItems;
        }

        public async Task<List<ContentItem>> GetAllAsync()
        {
            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.ContentType == Constants.ContentTypeName)
                                      .ListAsync();

            return contentItems.ToList();
        }

        public async Task<IList<ContentItem>> GetAllByGroupAsync(ContentItem contentItem)
        {

            if (contentItem == null)
            {
                return null;
            }

            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.ContentType == Constants.ContentTypeName)
                                      .With<ProfileGroupedPartIndex>(x => x.GroupContentItemId == contentItem.ContentItemId)
                                      .ListAsync();

            return contentItems.ToList();
        }

        #endregion
    }
}
