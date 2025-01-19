using AutoMapper;
using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Application.Handlers.Commands;
using HelsiTestAssesment.Application.Mappers;
using HelsiTestAssesment.Domain;
using Moq;

namespace HelsiTestAssesment.Tests.Commands;

[TestClass]
public sealed class AddTaskListCommandHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly AddTaskListCommandHandler _handler;

    public AddTaskListCommandHandlerTests()
    {
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<TaskListMapperProfile>())
            .CreateMapper();

        _handler = new AddTaskListCommandHandler(_taskListRepositoryMock.Object, mapper);
    }

    [TestMethod]
    public async Task Handle_Should_Add_TaskList_And_Return_Success()
    {
        // Arrange
        var createTaskListDto = new CreateTaskListDto
        {
            Name = "Task list to get new job at Helsi",
            Tasks = new List<string>() { "Complete test assignment" }
        };

        var command = new AddTaskListCommand(createTaskListDto, "AndriiRyzhkov");

        _taskListRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);

        var taskList = result.Data as TaskList;
        Assert.AreEqual(createTaskListDto.Name, taskList.Name);
        Assert.AreEqual(command.UserId, taskList.OwnerId);
        Assert.IsTrue(taskList.CreatedAt <= DateTime.UtcNow);
    }
}
