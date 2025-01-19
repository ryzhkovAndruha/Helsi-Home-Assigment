using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Handlers.Commands;
using HelsiTestAssesment.Domain;
using Moq;

namespace HelsiTestAssesment.Tests.Commands;

[TestClass]
public class AddUserToTaskListCommandHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly AddUserToTaskListCommandHandler _handler;

    public AddUserToTaskListCommandHandlerTests()
    {
        _handler = new AddUserToTaskListCommandHandler(_taskListRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Add_User_To_TaskList_And_Return_Success()
    {
        // Arrange
        var taskListId = "taskList01";
        var userToAdd = "user02";
        var userId = "user01";

        var taskList = new TaskList
        {
            Id = taskListId,
            Name = "Test Task List",
            OwnerId = userId
        };

        var command = new AddUserToTaskListCommand(taskListId, userToAdd, userId);

        _taskListRepositoryMock
            .Setup(x => x.AddUserToTaskListAsync(taskListId, userToAdd, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskList)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(taskList, result.Data);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_TaskList_Not_Found()
    {
        // Arrange
        var taskListId = "taskList01";
        var userToAdd = "user02";
        var userId = "user01";

        var command = new AddUserToTaskListCommand(taskListId, userToAdd, userId);

        _taskListRepositoryMock
            .Setup(x => x.AddUserToTaskListAsync(taskListId, userToAdd, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskList)null)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
        Assert.AreEqual($"Failed to add user {userToAdd} to task list {taskListId}", result.ErrorMessage);
    }
}
