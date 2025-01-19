using AutoMapper;
using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Application.Handlers.Queries;
using HelsiTestAssesment.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelsiTestAssesment.Tests.Queries;

[TestClass]
public class GetTaskListsQueryHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly GetTaskListsQueryHandler _handler;

    public GetTaskListsQueryHandlerTests()
    {
        _handler = new GetTaskListsQueryHandler(_taskListRepositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Paginated_TaskLists_When_Found()
    {
        // Arrange
        var userId = "user01";
        var page = 1;
        var pageSize = 2;

        var query = new GetTaskListsQuery(userId, page, pageSize);

        var taskLists = new List<TaskList>
        {
            new TaskList { Id = "1", Name = "TaskList1", Tasks = new List<string> { "Task1" } },
            new TaskList { Id = "2", Name = "TaskList2", Tasks = new List<string> { "Task2" } },
            new TaskList { Id = "3", Name = "TaskList3", Tasks = new List<string> { "Task3" } }
        };

        var paginatedItems = taskLists.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var mappedItems = new List<GetAllTaskListsDto>
        {
            new GetAllTaskListsDto { Id = "1", Name = "TaskList1" },
            new GetAllTaskListsDto { Id = "2", Name = "TaskList2" }
        };

        _taskListRepositoryMock
            .Setup(r => r.GetAllAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskLists)
            .Verifiable();

        _mapperMock
            .Setup(m => m.Map<IEnumerable<GetAllTaskListsDto>>(paginatedItems))
            .Returns(mappedItems)
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        _mapperMock.VerifyAll();

        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.TotalCount);
        Assert.AreEqual(2, result.Items.Count());
        Assert.AreEqual(page, result.Page);
        Assert.AreEqual(pageSize, result.PageSize);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Null_When_No_TaskLists_Found()
    {
        // Arrange
        var userId = "user01";
        var query = new GetTaskListsQuery(userId, 1, 2);

        _taskListRepositoryMock
            .Setup(r => r.GetAllAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<TaskList>?)null)
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Null_When_TaskLists_Empty()
    {
        // Arrange
        var userId = "user01";
        var query = new GetTaskListsQuery(userId, 1, 2);

        _taskListRepositoryMock
            .Setup(r => r.GetAllAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<TaskList>())
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNull(result);
    }
}
