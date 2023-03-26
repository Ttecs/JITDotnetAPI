using AutoMapper;
using Azure;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEfController : ControllerBase
{

    DataContextEF _entityFramwork;
    IUserRepository _userRepository;
    IMapper _mapper;

    public UserEfController(IConfiguration config, IUserRepository userRepository)
    {
        _entityFramwork = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserDto, User>();
        }));
        _userRepository = userRepository;
    }



    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()

    {
        IEnumerable<User> users = _entityFramwork.Users.ToList<User>();

        return users;
    }



    [HttpGet("GetSingleUsers/{userId}")]
    public IActionResult GetSingleUsers(int userId)

    {
       

        User? user = _entityFramwork.Users
                    .Where(u=>u.UserId==userId)
                    .FirstOrDefault<User>();


        if (user == null)
        {
            return NotFound();
        }
        else
        {

            return Ok(user);
        }

    }



    [HttpPut("EditUser")]
    public IActionResult EditUser([FromBody] User user)
    {
        if (user == null || user.UserId == null)
        {
            return BadRequest("Invalid user data");
        }

        User userDB = _entityFramwork.Users.FirstOrDefault(u => u.UserId == user.UserId);

        if (userDB == null)
        {
            return NotFound();
        }
        else
        {
            // Perform validation on user input
            

            userDB.Active = user.Active;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            userDB.Email = user.Email;
            userDB.Gender = user.Gender;

            try
            {
                _userRepository.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log error and return appropriate response
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to edit user: " + ex.Message);
            }
        }
    }



    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user)
    {

        User userDB = _mapper.Map<User>(user);

            _userRepository.AddEntity<User>(userDB);
            if (_userRepository.SaveChanges())
            {
                return Ok(user);
            }

            throw new Exception("failed to edit user");

        
    }


    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {

        User? userDB = _entityFramwork.Users
                            .Where(u => u.UserId == userId)
                            .FirstOrDefault<User>();


        if (userDB == null)
        {
            return NotFound();
        }
        else
        {
            _userRepository.RemoveEntity<User>(userDB);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("failed to delete user");

        }
    }
    }
