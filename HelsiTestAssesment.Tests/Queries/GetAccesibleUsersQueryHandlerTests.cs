using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Application.Handlers.Queries;
using Moq;

namespace HelsiTestAssesment.Tests.Queries;

[TestClass]
public class GetAccesibleUsersQueryHandlerTests
{
    private readonly Mock<ITaskListRepository> _taskListRepositoryMock = new();
    private readonly GetAccesibleUsersQueryHandler _handler;

    public GetAccesibleUsersQueryHandlerTests()
    {
        _handler = new GetAccesibleUsersQueryHandler(_taskListRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Accessible_Users_When_Found()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var query = new GetAccesibleUsersQuery(taskListId, userId);

        var accessibleUsers = new List<string> { "user02", "user03" };

        _taskListRepositoryMock
            .Setup(r => r.GetAccesibleUsers(taskListId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accessibleUsers)
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(accessibleUsers, result.ToList());
    }

    [TestMethod]
    public async Task Handle_Should_Return_Null_When_No_Users_Accessible()
    {
        // Arrange
        var taskListId = "taskList01";
        var userId = "user01";

        var query = new GetAccesibleUsersQuery(taskListId, userId);

        _taskListRepositoryMock
            .Setup(r => r.GetAccesibleUsers(taskListId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<string>?)null)
            .Verifiable();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _taskListRepositoryMock.VerifyAll();
        Assert.IsNull(result);
    }
}
