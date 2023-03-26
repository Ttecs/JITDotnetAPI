using Azure;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);

    }

    

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()

    {
        string sql = @"  SELECT 
                         UserId,
                         FirstName,
                         LastName,
                         Email,
                         Gender,
                         Active 
                         FROM Users";
       
        IEnumerable<User> users = _dapper.LoadData<User>(sql);

        return users;
    }



    [HttpGet("GetSingleUsers/{userId}")]
    public IActionResult GetSingleUsers(int userId)

    {
        string sql = @" SELECT 
                         UserId,
                         FirstName,
                         LastName,
                         Email,
                         Gender,
                         Active 
                         FROM Users WHERE UserId=" + userId.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);


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
    public IActionResult EditUser(User user)
    {
        
        string sql = @"UPDATE Users
                       SET [FirstName]='" + user.FirstName +
                       "', [LastName]='" + user.LastName +
                       "', [Email]='" + user.Email +
                       "', Gender='" + user.Gender +
                       "', Active='" + user.Active +
                       "'WHERE UserId=" + user.UserId;
       
        bool count = _dapper.ExecuteSql(sql);

        return Ok();
    }

   

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user)
    {
        string  sql = @"INSERT INTO Users (FirstName, LastName, Email, Gender, Active)
                        VALUES ('" + user.FirstName +
                        "', '" + user.LastName +
                        "', '" + user.Email +
                        "', '" + user.Gender +
                        "', '" + user.Active + "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(user);
        }
        throw new Exception("failed to add user");
    }


    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {
        string sql = "DELETE FROM Users WHERE UserID=" + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();

        }
        throw new Exception("delete user is not successfull");
    }

}
