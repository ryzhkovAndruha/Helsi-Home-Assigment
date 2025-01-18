namespace HelsiTestAssesment.Application.Core.Interfaces;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}
