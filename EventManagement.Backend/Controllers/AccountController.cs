using apis.Dtos.Account;
using apis.Interfaces;
using apis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apis.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName!.Equals(loginDto.UserName, StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
                return Unauthorized("Invalid username!");

            var result = await signManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Username not found and/or password is incorrect");

            return Ok(new NewUserDto
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Token = tokenService.CreateToken(user)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                AppUser appUser = new ()
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await userManager.CreateAsync(appUser, registerDto.Password!);

                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName!,
                                Email = appUser.Email!,
                                Token = tokenService.CreateToken(appUser)
                            }
                        );
                    else
                        return StatusCode(500, roleResult.Errors);
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
