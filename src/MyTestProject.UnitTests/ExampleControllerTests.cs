using System;
using Xunit;
using Moq;

namespace MyTestProject {

    public class FakeExecutionService : IExecutionService {

        public bool WasEchoCalled = false;

        private readonly string EchoResult;

        public FakeExecutionService(string echoResult) {
            this.EchoResult = echoResult;
        }

        public string Echo(int value) {
            this.WasEchoCalled = true;
            return this.EchoResult;
        }
    }

    public class ExampleControllerTests {

        [Fact]
        public void ExampleController_WithFakeExecEngine_ReturnsActual() {

            // Arrange
            var dummyString = "Some Dummy Value";
            var fakeExec = new FakeExecutionService(dummyString);
            var controller = this.GetController(executionService: fakeExec);

            // Act
            var result = controller.Execute(1);

            // Assert
            Assert.True(fakeExec.WasEchoCalled);
            Assert.Equal(dummyString, result);
        }

        [Fact]
        public void ExampleController_WithMockedMethod_ReturnsBlah() {

            // Arrange
            var execMock = this.GetExecutionServiceMock();
            execMock.Setup(x => x.Echo(It.IsAny<int>())).Returns("Blah");
            var controller = this.GetController(executionService: execMock.Object);

            // Act
            var result = controller.Execute(1);

            // Assert
            Assert.Equal("Blah", result);
        }

        private ExampleController GetController(
            IDatabaseService databaseService = null,
            IExecutionService executionService = null
        ) {

            // If there are any nulls, implement the new Mock
            if (databaseService == null) {
                var dataServiceMock = this.GetDatabaseServiceMock();
                databaseService = dataServiceMock.Object;
            }

            if (executionService == null) {
                var executionServiceMock = this.GetExecutionServiceMock();
                executionService = executionServiceMock.Object;
            }

            var controller = new ExampleController(databaseService, executionService);

            return controller;
        }

        private Mock<IDatabaseService> GetDatabaseServiceMock() {
            return new Mock<IDatabaseService>();
        }

        private Mock<IExecutionService> GetExecutionServiceMock() {
            return new Mock<IExecutionService>();
        }
    }
}
