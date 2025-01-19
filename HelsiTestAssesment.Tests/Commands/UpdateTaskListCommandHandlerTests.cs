using AutoMapper;
using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Application.Handlers.Commands;
using HelsiTestAssesment.Domain;
using Moq;

namespace HelsiTestAssesment.Tests.Commands;

[TestClass]
public class UpdateTaskListCommandHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly UpdateTaskListCommandHandler _handler;

    public UpdateTaskListCommandHandlerTests()
    {
        _handler = new UpdateTaskListCommandHandler(_taskListRepositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Update_TaskList_And_Return_Success()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var updateTaskListDto = new UpdateTaskListDto
        {
            Name = "Updated Task List",
        };

        var command = new UpdateTaskListCommand(taskListId, updateTaskListDto, userId);

        var mappedTaskList = new TaskList
        {
            Id = taskListId,
            Name = updateTaskListDto.Name,
        };

        var updatedTaskList = new TaskList
        {
            Id = taskListId,
            Name = "Updated Task List",
        };

        _mapperMock
            .Setup(m => m.Map<TaskList>(updateTaskListDto))
            .Returns(mappedTaskList);

        _taskListRepositoryMock
            .Setup(r => r.UpdateAsync(mappedTaskList, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedTaskList)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<TaskList>(updateTaskListDto), Times.Once);
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(updatedTaskList, result.Data);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Update_Fails()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var updateTaskListDto = new UpdateTaskListDto
        {
            Name = "Updated Task List",
        };

        var command = new UpdateTaskListCommand(taskListId, updateTaskListDto, userId);

        var mappedTaskList = new TaskList
        {
            Id = taskListId,
            Name = updateTaskListDto.Name,
        };

        _mapperMock
            .Setup(m => m.Map<TaskList>(updateTaskListDto))
            .Returns(mappedTaskList);

        _taskListRepositoryMock
            .Setup(r => r.UpdateAsync(mappedTaskList, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskList?)null)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<TaskList>(updateTaskListDto), Times.Once);
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
        Assert.AreEqual($"Failed to update task list {taskListId} by user {userId}", result.ErrorMessage);
    }
}
