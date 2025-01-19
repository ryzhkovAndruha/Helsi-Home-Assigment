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
    [HttpGet("task-lists/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(string id, string userId)
    {
        var query = new GetTaskListByIdQuery(id, userId);
        var taskList = await queryDispatcher.DispatchAsync<GetTaskListByIdQuery, TaskList?>(query);

        if (taskList == null)
        {
            return NotFound($"Task list with id {id} is not found for user {userId}");
        }

        return Ok(taskList);
    }

    [HttpGet("task-lists")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll(string userId)
    {
        var query = new GetTaskListsQuery(userId);
        var taskList = await queryDispatcher.DispatchAsync<GetTaskListsQuery, IEnumerable<TaskList>?>(query);

        if (taskList == null)
        {
            return NotFound($"No task lists found for user {userId}");
        }

        return Ok(taskList);
    }

    [HttpGet("task-lists/{id}/users")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUsers(string id, string userId)
    {
        var query = new GetAccesibleUsersQuery(id, userId);
        var accesibleUsers = await queryDispatcher.DispatchAsync<GetAccesibleUsersQuery, IEnumerable<string>?>(query);

        if (accesibleUsers == null 
            || accesibleUsers.Count() == 0)
        {
            return NotFound($"No accessible users found for tsak list {id}");
        }

        return Ok(accesibleUsers);
    }

    [HttpPost("task-lists")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create(CreateTaskListDto createTaskListDto, string userId)
    {
        var command = new AddTaskListCommand(createTaskListDto, userId);
        var result = await commandDispatcher.DispatchAsync(command);
        var taskList = result.Data as TaskList;

        return CreatedAtAction(nameof(GetById), new { id = taskList?.Id }, taskList);
    }

    [HttpPut("task-lists/{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(string id, [FromBody]UpdateTaskListDto updateTaskListDto, string userId)
    {
        var command = new UpdateTaskListCommand(id, updateTaskListDto, userId);
        var result = await commandDispatcher.DispatchAsync(command);

        if (!result.Success)
        {
            return NotFound(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [HttpPatch("task-lists/{id}/users")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateUserInTaskList(string id, UpdateTaskListUsersDto updateTaskListUsersDto, string userId)
    {
        var result = true;

        if (updateTaskListUsersDto.UserToAdd != null)
        {
            var addCommand = new AddUserToTaskListCommand(id, updateTaskListUsersDto.UserToAdd, userId);
            result &= (await commandDispatcher.DispatchAsync(addCommand)).Success;
        }
        if (updateTaskListUsersDto.UserToRemove != null)
        {
            var removeCommand = new RemoveUserFromTaskListCommand(id, updateTaskListUsersDto.UserToRemove, userId);
            result &= (await commandDispatcher.DispatchAsync(removeCommand)).Success;
        }

        if (!result)
        {
            return NotFound($"Unable to find task list with id {id}");
        }

        return NoContent();
    }

    [HttpDelete("task-lists/{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteTaskList(string id, string userId)
    {
        var command = new DeleteTaskListCommand(id, userId);
        var result = await commandDispatcher.DispatchAsync(command);

        if (!result.Success)
        {
            return NotFound(result.ErrorMessage);
        }

        return NoContent();
    }
}
