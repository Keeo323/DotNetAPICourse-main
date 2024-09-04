using DotnetAPI.Data;
using DotnetAPI.Dto;
using DotnetAPI.Models;
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

    [HttpGet("TestConection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers")]
    // public IActionResult Test()
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
        SELECT [UserId],
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        FROM TutorialAppSchema.Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }

    [HttpGet("GetSingleUsers/{userId}")]
    // public IActionResult Test()
    public User GetSingleUsers(int userId)
    {
        string sql = @"
        SELECT [UserId],
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        FROM TutorialAppSchema.Users
            WHERE UserId = " + userId.ToString(); //"Will be a string eg:7"
        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = $@"UPDATE [DTCodeDatabase1].[TutorialAppSchema].[Users]
     SET [FirstName]= '{user.FirstName}'
      ,[LastName]= '{user.LastName}'
      ,[Email] = '{user.Email}'
      ,[Gender] = '{user.Gender}'
      ,[Active] = '{user.Active}'
  WHERE [UserId]= '{user.UserId}'";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        };
        throw new Exception("Failed to Update user ");
    }

    [HttpPost("AddUser")]

    public IActionResult AddUser(UserDto userDto)
    {
        string sql = $@"INSERT INTO [DTCodeDatabase1].[TutorialAppSchema].[Users]( 
      [FirstName]
      ,[LastName]
      ,[Email]
      ,[Gender]
      ,[Active]
  )VALUES(
    '{userDto.FirstName}',
    '{userDto.LastName}',
    '{userDto.Email}',
    '{userDto.Gender}',
    '{userDto.Active}'
    )";
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        };
        throw new Exception("Failed to create user ");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteeUser(string userId)
    {
        string sql = $@"DELETE FROM [DTCodeDatabase1].[TutorialAppSchema].[Users] WHERE [UserId]= '{userId}'";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        };
        throw new Exception("Failed to Delete user ");
    }

}