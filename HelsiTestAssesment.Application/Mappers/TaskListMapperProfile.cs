using AutoMapper;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Mappers;

public class TaskListMapperProfile: Profile
{
    public TaskListMapperProfile()
    {
        CreateMap<CreateTaskListDto, TaskList>();
        CreateMap<UpdateTaskListDto, TaskList>();
        CreateMap<TaskList, GetAllTaskListsDto>();
    }
}
