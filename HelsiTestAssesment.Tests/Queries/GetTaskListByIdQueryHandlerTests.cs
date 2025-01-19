using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Handlers.Queries;
using HelsiTestAssesment.Domain;
using Moq;

namespace HelsiTestAssesment.Tests.Queries;

[TestClass]
public class GetTaskListByIdQueryHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly GetTaskListByIdQueryHandler _handler;

    public GetTaskListByIdQueryHandlerTests()
    {
        _handler = new GetTaskListByIdQueryHandler(_taskListRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Return_TaskList_When_Found()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var query = new GetTaskListByIdQuery(taskListId, userId);

        var taskList = new TaskList
        {
            Id = taskListId,
            Name = "Sample Task List",
            Tasks = new List<string> { "Task1", "Task2" }
        };

        _taskListRepositoryMock
            .Setup(r => r.GetByIdAsync(taskListId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskList)
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.AreEqual(taskList, result);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Null_When_TaskList_Not_Found()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var query = new GetTaskListByIdQuery(taskListId, userId);

        _taskListRepositoryMock
            .Setup(r => r.GetByIdAsync(taskListId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskList?)null)
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNull(result);
    }
}
