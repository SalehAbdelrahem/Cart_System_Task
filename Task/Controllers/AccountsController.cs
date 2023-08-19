using Microsoft.AspNetCore.Mvc;
using Reposotries;
using ViewModels;
using ViewModels.Userr;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IUserRepository UserRepository;
        ResultViewModel result = new ResultViewModel();
        public AccountsController(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            UserRepository = userRepository;

        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signupModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.SignUp(signupModel);
            if (!result.IsAuthenticated)
                return Ok(result);

            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.Login(loginModel);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
