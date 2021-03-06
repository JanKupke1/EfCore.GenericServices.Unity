﻿using Moq;
using Prism.Regions;
using ShowRoom.Modules.EmployeeManagment.ViewModels;
using ShowRoom.Services.Interfaces;
using Xunit;

namespace ShowRoom.Modules.ModuleName.Tests.ViewModels
{
    public class EmployeeSelectionViewModelFixture
    {
        Mock<IMessageService> _messageServiceMock;
        Mock<IRegionManager> _regionManagerMock;
        const string MessageServiceDefaultMessage = "Some Value";

        public EmployeeSelectionViewModelFixture()
        {
            var messageService = new Mock<IMessageService>();
            messageService.Setup(x => x.GetMessage()).Returns(MessageServiceDefaultMessage);
            _messageServiceMock = messageService;

            _regionManagerMock = new Mock<IRegionManager>();
        }

        [Fact]
        public void MessagePropertyValueUpdated()
        {
            var vm = new EmployeeSelectionViewModel(_regionManagerMock.Object, _messageServiceMock.Object);

            _messageServiceMock.Verify(x => x.GetMessage(), Times.Once);

            Assert.Equal(MessageServiceDefaultMessage, vm.Message);
        }

        [Fact]
        public void MessageINotifyPropertyChangedCalled()
        {
            var vm = new EmployeeSelectionViewModel(_regionManagerMock.Object, _messageServiceMock.Object);
            Assert.PropertyChanged(vm, nameof(vm.Message), () => vm.Message = "Changed");
        }
    }
}
