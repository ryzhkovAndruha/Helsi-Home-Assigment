using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Application.Handlers.Commands;
using HelsiTestAssesment.Application.Handlers.Queries;
using HelsiTestAssesment.Domain;
using HelsiTestAssesment.Infrastucture.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace HelsiTestAssessment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskListController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("task-list/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(string id, string userId)
    {
        var query = new GetTaskListByIdQuery(id, userId);
        var taskList = await queryDispatcher.DispatchAsync<GetTaskListByIdQuery, TaskList?>(query);

        if (taskList == null)
        {
            return NotFound();
        }

        return Ok(taskList);
    }

    [HttpGet("task-lists/all")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll(string userId)
    {
        var query = new GetTaskListsQuery(userId);
        var taskList = await queryDispatcher.DispatchAsync<GetTaskListsQuery, IEnumerable<TaskList>?>(query);

        if (taskList == null)
        {
            return NotFound();
        }

        return Ok(taskList);
    }

    [HttpGet("task-lists/{id}/users")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUsers(string taskListId, string userId)
    {
        var query = new GetAccesibleUsersQuery(taskListId, userId);
        var taskList = await queryDispatcher.DispatchAsync<GetAccesibleUsersQuery, IEnumerable<string>?>(query);

        if (taskList == null)
        {
            return NotFound();
        }

        return Ok(taskList);
    }

    [HttpPost("task-list")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create(CreateTaskListDto createTaskListDto, string userId)
    {
        var command = new AddTaskListCommand(createTaskListDto, userId);
        await commandDispatcher.DispatchAsync(command);

        return Created();
    }

    [HttpPut("task-list")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(UpdateTaskListDto updateTaskListDto, string userId)
    {
        var command = new UpdateTaskListCommand(updateTaskListDto, userId);
        await commandDispatcher.DispatchAsync(command);

        return Ok();
    }

    [HttpPatch("task-list/{taskListId}/users/add")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> AddUserToTaskList(string taskListId, string userToAddId, string userId)
    {
        var command = new AddUserToTaskListCommand(taskListId, userToAddId, userId);
        await commandDispatcher.DispatchAsync(command);

        return Ok();
    }

    [HttpPatch("task-list/{taskListId}/users/remove")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> RemoveUserFromTaskList(string taskListId, string userToRemoveId, string userId)
    {
        var command = new RemoveUserFromTaskListCommand(taskListId, userToRemoveId, userId);
        await commandDispatcher.DispatchAsync(command);

        return Ok();
    }

    [HttpDelete("task-list")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteTaskList(string taskListId, string userId)
    {
        var command = new DeleteTaskListCommand(taskListId, userId);
        await commandDispatcher.DispatchAsync(command);

        return Ok();
    }
}
