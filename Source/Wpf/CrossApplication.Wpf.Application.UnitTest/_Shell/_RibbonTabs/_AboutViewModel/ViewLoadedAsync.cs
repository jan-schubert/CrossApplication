﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.About;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CrossApplication.Wpf.Application.UnitTest._Shell._RibbonTabs._AboutViewModel
{
    public class ViewLoadedAsync
    {
        private static AboutViewModel GetInitializedAboutViewModel()
        {
            var aboutServiceMock = new Mock<IAboutService>();
            aboutServiceMock.Setup(x => x.GetVersionAsync()).Returns(Task.FromResult(new Version(1, 2, 3, 4)));
            aboutServiceMock.Setup(x => x.GetModuleInfosAsync()).Returns(Task.FromResult<IEnumerable<ModuleInfo>>(new List<ModuleInfo>
            {
                new ModuleInfo {Name = "MyModule"},
                new ModuleInfo {Name = "MyModule2"}
            }));
            return new AboutViewModel(aboutServiceMock.Object);
        }

        [Fact]
        public async void ShouldSetNotInfrastructureModuleInfos()
        {
            var viewModel = GetInitializedAboutViewModel();

            await viewModel.OnViewActivatedAsync(null);

            viewModel.ModuleInfos.Count.Should().Be(2);
            viewModel.ModuleInfos[0].Should().Be("MyModule");
            viewModel.ModuleInfos[1].Should().Be("MyModule2");
        }

        [Fact]
        public async void ShouldSetVersion()
        {
            var viewModel = GetInitializedAboutViewModel();

            await viewModel.OnViewActivatedAsync(null);

            viewModel.Version.Should().Be(new Version(1, 2, 3, 4));
        }
    }
}