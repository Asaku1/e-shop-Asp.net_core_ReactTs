﻿using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;

        private readonly TokenService _tokenService;

        public AccountController(UserManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;

            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username); //find username
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            //Creates a new user, by the user typing a username & email.
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            //Creates password, i.e., user types a password.
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            //if the password failed, then return the problem
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member"); //add the user to the dB

            return StatusCode(201);
        }
    }
}