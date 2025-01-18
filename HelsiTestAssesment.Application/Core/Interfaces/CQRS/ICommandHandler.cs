namespace HelsiTestAssesment.Application.Core.Interfaces.CQRS;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}
