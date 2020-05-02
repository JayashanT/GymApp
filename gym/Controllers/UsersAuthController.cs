using AutoMapper;
using gym.Dtos;
using gym.Entity;
using gym.Repositories;
using gym.Services;
using gym.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAuthController:Controller
    {
        private ICommonRepository<User> _userRepository;
        private IUserServices _userServices;
        public UsersAuthController(ICommonRepository<User> userRepository,IUserServices userServices)
        {
            _userRepository = userRepository;
            _userServices = userServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var allUsers = _userRepository.GetAll().ToList();
            var allUsersDto = allUsers.Select(x => Mapper.Map<UserDto>(x));

            return Ok(allUsersDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(int id)
        {
            User userFromRepo = _userRepository.Get(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
        
            return Ok(Mapper.Map<UserDto>(userFromRepo));
        }

        /*[HttpPost]
        [Route("AddNewMember")]
        public IActionResult Add([FromBody] UserDto user)
        {
            User toAdd = Mapper.Map<User>(user);
           _userRepository.Add(toAdd);
            bool Result = _userRepository.Save();

            if (!Result)
            {
                return new StatusCodeResult(400);
            }
            return Ok(Mapper.Map<UserDto>(toAdd));
        }*/

        [HttpPost]
        [Route("Signup")]
        public IActionResult SignUp([FromBody]MemberVM memberVM)
        {
            var member = Mapper.Map<MemberDto>(memberVM);

            try
            {
                var Result = _userServices.SignUp(member, memberVM.Password);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }  
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn([FromBody]LoginVM loginVM)
        {
            try
            {
                return Ok(_userServices.Authenticate(loginVM.ContactNo, loginVM.Password));
            }
            catch (Exception e) { return BadRequest(new { e.Message }); }
        }

    }
}