﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

#nullable enable

using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices.Implementation.Venus;
using Microsoft.VisualStudio.LanguageServices.LiveShare.Client.Debugging;
using Microsoft.VisualStudio.LiveShare.WebEditors.ContainedLanguage;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.VisualStudio.LanguageServices.LiveShare.Client.Razor
{
    [Export(typeof(IContainedLanguageProvider))]
    internal class CSharpLspContainedLanguageProvider : IContainedLanguageProvider
    {
        private readonly IContentTypeRegistryService _contentTypeRegistry;
        private readonly SVsServiceProvider _serviceProvider;
        private readonly CSharpLspRazorProject _razorProject;

        [ImportingConstructor]
        public CSharpLspContainedLanguageProvider(IContentTypeRegistryService contentTypeRegistry,
            SVsServiceProvider serviceProvider,
            CSharpLspRazorProject razorProject)
        {
            _contentTypeRegistry = contentTypeRegistry ?? throw new ArgumentNullException(nameof(contentTypeRegistry));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _razorProject = razorProject ?? throw new ArgumentNullException(nameof(razorProject));
        }

        public IContentType GetContentType(string filePath)
        {
            return _contentTypeRegistry.GetContentType(StringConstants.CSharpLspContentTypeName);
        }

        public IVsContainedLanguage GetLanguage(string filePath, IVsTextBufferCoordinator bufferCoordinator)
        {
            var componentModel = (IComponentModel)_serviceProvider.GetService(typeof(SComponentModel));
            var project = _razorProject.GetProject(filePath);

            return new ContainedLanguage(
                bufferCoordinator,
                componentModel,
                project.ProjectTracker.Workspace,
                project.Id,
                project.VisualStudioProject,
                filePath,
                CSharpLspLanguageService.LanguageServiceGuid);
        }
    }
}
