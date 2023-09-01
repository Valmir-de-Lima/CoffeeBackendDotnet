using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;

namespace Coffee.Domain.Handlers.UserHandlers;

public class UserHandler : Handler,
    IHandler<RegisterUserCommand>,
    IHandler<ActiveUserCommand>,
    IHandler<RecoveryPasswordUserCommand>,
    IHandler<ConfirmRecoveryPasswordUserCommand>,
    IHandler<UpdateRecoveryPasswordUserCommand>,
    IHandler<CreateUserCommand>,
    IHandler<CreateManagerCommand>,
    IHandler<UpdateUserCommand>,
    IHandler<UpdatePasswordUserCommand>,
    IHandler<UpdateTypeUserCommand>,
    IHandler<DeleteUserCommand>,
    IHandler<LoginUserCommand>,
    IHandler<GetUserByEmailCommand>,
    IHandler<GetUserCommand>,
    IHandler<RefreshLoginUserCommand>

{
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public UserHandler(IUserRepository repository, ITokenService tokenService, IEmailService emailService)
    {
        _repository = repository;
        _tokenService = tokenService;
        _emailService = emailService;
    }
    public async Task<ICommandResult> HandleAsync(RegisterUserCommand command)
    {
        return await new RegisterUserHandler(_repository, _emailService).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(RecoveryPasswordUserCommand command)
    {
        return await new RecoveryPasswordUserHandler(_repository, _emailService).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(ConfirmRecoveryPasswordUserCommand command)
    {
        return await new ConfirmRecoveryPasswordUserHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdateRecoveryPasswordUserCommand command)
    {
        return await new UpdateRecoveryPasswordUserHandler(_repository, _emailService).HandleAsync(command);
    }


    public async Task<ICommandResult> HandleAsync(ActiveUserCommand command)
    {
        return await new ActiveUserHandler(_repository).HandleAsync(command);
    }


    public async Task<ICommandResult> HandleAsync(CreateUserCommand command)
    {
        return await new CreateUserHandler(_repository, _emailService).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(CreateManagerCommand command)
    {
        return await new CreateManagerHandler(_repository, _emailService).HandleAsync(command);
    }
    public async Task<ICommandResult> HandleAsync(GetUserCommand command)
    {
        return await new GetUserHandler(_repository).HandleAsync(command);
    }
    public async Task<ICommandResult> HandleAsync(GetUserByEmailCommand command)
    {
        return await new GetUserByEmailHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdateUserCommand command)
    {
        return await new UpdateUserHandler(_repository).HandleAsync(command);
    }
    public async Task<ICommandResult> HandleAsync(UpdatePasswordUserCommand command)
    {
        return await new UpdatePasswordUserHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdateTypeUserCommand command)
    {
        return await new UpdateTypeUserHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeleteUserCommand command)
    {
        return await new DeleteUserHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(LoginUserCommand command)
    {
        return await new LoginUserHandler(_repository, _tokenService, _emailService).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(RefreshLoginUserCommand command)
    {
        return await new RefreshLoginUserHandler(_tokenService).HandleAsync(command);
    }
}