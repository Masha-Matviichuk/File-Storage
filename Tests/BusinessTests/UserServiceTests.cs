using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Entities;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace Tests.BusinessTests
{
    public class UserServiceTests
    {
        [Test]
        public async Task BookService_GetAll_ReturnsBookModels()
        {
            var expected = GetTestUserModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserManager = new Mock<UserManager<UserProfile>>();
            mockUnitOfWork
                .Setup(m => m.UserRepository.GetAllAsync())
                .ReturnsAsync(GetTestUserEnteiies().AsQueryable);
            var userService = new UserService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfileForBll(), mockUserManager.Object);

            var actual = ( await userService.GetAllAsync()).ToList();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Email, actual[i].Email);
                Assert.AreEqual(expected[i].Password, actual[i].Password);
                Assert.AreEqual(expected[i].Roles, actual[i].Roles);
                Assert.AreEqual(expected[i].FirstName, actual[i].FirstName);
                Assert.AreEqual(expected[i].LastName, actual[i].LastName);
                Assert.AreEqual(expected[i].IsBanned, actual[i].IsBanned);
                Assert.AreEqual(expected[i].EndOfBan, actual[i].EndOfBan);
                Assert.AreEqual(expected[i].PhoneNumber, actual[i].PhoneNumber);
            }
            
            
            
            #region data for tests

            IEnumerable<User> GetTestUserEnteiies()
            {
                return new List<User>()
                {
                    new User()
                    {
                        Id = 1, Email = "a@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue
                    },
                    new User()
                    {
                        Id = 2, Email = "l@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue
                    },
                    new User
                    {
                        Id = 3, Email = "n@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue
                    }
                };
            }
            
            IEnumerable<UserInfoDto> GetTestUserModels()
            {
                return new List<UserInfoDto>()
                {
                    new UserInfoDto()
                    {
                        Id = 1, Email = "a@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue,
                        FirstName = "First", LastName = "Last", Password = "12345Hh!", PhoneNumber =
                            "235475896",
                        Roles = new List<string>() {"user"}
                    },
                    new UserInfoDto()
                    {
                        Id = 2, Email = "l@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue,
                        FirstName = "First", LastName = "Last", Password = "12345Hh!", PhoneNumber =
                            "235475896",
                        Roles = new List<string>() {"user"}
                    },
                    new UserInfoDto
                    {
                        Id = 3, Email = "n@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue,
                        FirstName = "First", LastName = "Last", Password = "12345Hh!", PhoneNumber =
                            "235475896",
                        Roles = new List<string>() {"user"}
                    }
                };
            }
            #endregion
        }
    }
}