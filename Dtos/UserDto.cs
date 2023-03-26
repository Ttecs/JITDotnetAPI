﻿namespace DotnetAPI.Dtos
{
    public class UserDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }


        public UserDto()
        {
            FirstName ??= "";
            LastName ??= "";
            Email ??= "";
            Gender ??= "";
        }
    }
}