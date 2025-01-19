using HelsiTestAssesment.Application.Core.Interfaces.CQRS;
using HelsiTestAssesment.Application.DTOs;
using HelsiTestAssesment.Domain;

namespace HelsiTestAssesment.Application.Handlers.Queries;

public record class GetTaskListsQuery(string UserId, int Page, int PageSize) : IQuery<PaginatedResult<GetAllTaskListsDto>?>;
