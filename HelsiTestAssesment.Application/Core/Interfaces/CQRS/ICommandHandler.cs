using HelsiTestAssesment.Application.DTOs;

namespace HelsiTestAssesment.Application.Core.Interfaces.CQRS;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandOperationResultDto> Handle(TCommand command, CancellationToken cancellationToken);
}
