using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Handlers.Commands;
using HelsiTestAssesment.Domain;
using Moq;

namespace HelsiTestAssesment.Tests.Commands;

[TestClass]
public class RemoveUserFromTaskListCommandHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly RemoveUserFromTaskListCommandHandler _handler;

    public RemoveUserFromTaskListCommandHandlerTests()
    {
        _handler = new RemoveUserFromTaskListCommandHandler(_taskListRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Remove_User_From_TaskList_And_Return_Success()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";
        var userToRemove = "userToRemove01";

        var command = new RemoveUserFromTaskListCommand(taskListId, userToRemove, userId);

        var updatedTaskList = new TaskList
        {
            Id = taskListId,
            Name = "Sample Task List",
            AccessibleUserIds = new List<string> { "user02", "user03" }
        };

        _taskListRepositoryMock
            .Setup(x => x.RemoveUserFromTaskListAsync(taskListId, userToRemove, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedTaskList)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(updatedTaskList, result.Data);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Removal_Fails()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";
        var userToRemove = "userToRemove01";

        var command = new RemoveUserFromTaskListCommand(taskListId, userToRemove, userId);

        _taskListRepositoryMock
            .Setup(x => x.RemoveUserFromTaskListAsync(taskListId, userToRemove, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskList?)null)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
        Assert.AreEqual($"Failed to remove user {userToRemove} to task list {taskListId}", result.ErrorMessage);
    }
}
