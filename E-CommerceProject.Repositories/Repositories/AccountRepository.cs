namespace E_CommerceProject.Repositories.Repositories;

public interface IAccountRepository
{
    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RegisterAsync(RegisterViewModel model, CancellationToken token);

    /// <summary>
    /// Logs in a user asynchronously.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task LoginAsync(LoginViewModel model, CancellationToken token);

    /// <summary>
    /// Gets the user profile asynchronously.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<UserProfileViewModel> GetUserProfileAsync(string userId, CancellationToken token);

    /// <summary>
    /// Logs out the current user asynchronously.
    /// </summary>
    /// <returns></returns>
    Task LogoutAsync();
}

public class AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager) : IAccountRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;

    public async Task RegisterAsync(RegisterViewModel model, CancellationToken token)
    {
        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FullName = model.FirstName + " " + model.LastName,
            UserName = model.Email.Split('@')[0],
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Constants.Roles.User);

            return;
        }

        foreach (var error in result.Errors)
        {
            throw new Exception(error.Description);
        }
        return;
    }

    public async Task LoginAsync(LoginViewModel model, CancellationToken token)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        user ??= await _userManager.FindByEmailAsync(model.Username) ??
            throw new Exception("Invalid login attempt.");

        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result)
        {
            throw new Exception("Invalid login attempt.");
        }

        await _signInManager.SignInAsync(user, model.RememberMe);
        return;
    }

    public async Task<UserProfileViewModel> GetUserProfileAsync(string userId, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? 
            throw new Exception("User not found.");

        return new UserProfileViewModel
        {
            Id = userId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName!,
            Email = user.Email!
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return;
    }
}