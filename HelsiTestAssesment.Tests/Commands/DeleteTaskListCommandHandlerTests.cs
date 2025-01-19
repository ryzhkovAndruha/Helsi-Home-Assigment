using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Handlers.Commands;
using Moq;

namespace HelsiTestAssesment.Tests.Commands;

[TestClass]
public class DeleteTaskListCommandHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly DeleteTaskListCommandHandler _handler;

    public DeleteTaskListCommandHandlerTests()
    {
        _handler = new DeleteTaskListCommandHandler(_taskListRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Delete_TaskList_And_Return_Success()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var command = new DeleteTaskListCommand(taskListId, userId);

        _taskListRepositoryMock
            .Setup(x => x.DeleteAsync(taskListId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Delete_Fails()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var command = new DeleteTaskListCommand(taskListId, userId);

        _taskListRepositoryMock
            .Setup(x => x.DeleteAsync(taskListId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
        Assert.AreEqual($"Failed to delete task list {taskListId} by user {userId}", result.ErrorMessage);
    }
}
