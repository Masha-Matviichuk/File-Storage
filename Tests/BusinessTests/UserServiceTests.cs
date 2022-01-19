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
using Microsoft.EntityFrameworkCore;
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
            var mockUserManager = new Mock<UserManager<UserProfile>>(Mock.Of<IUserStore<UserProfile>>(), null, null, null, null, null, null, null, null);
            mockUnitOfWork
                .Setup(m => m.UserRepository.GetAllAsync())
                .ReturnsAsync(GetTestUserEntities().AsQueryable);
            var userService = new UserService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfileForBll(), mockUserManager.Object);

            var actual = ( await userService.GetAllAsync()).ToList();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Email, actual[i].Email);
                Assert.AreEqual(expected[i].IsBanned, actual[i].IsBanned);
                Assert.AreEqual(expected[i].EndOfBan, actual[i].EndOfBan);
            }
        }
        
        [Test]
        public async Task UserService_UserBan()
        {
            //Arrange
            var user = new User()
            {
                Id = 1, Email = "a@gmail.com", IsBanned = true, EndOfBan = DateTime.Now.AddDays(1)
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserManager = new Mock<UserManager<UserProfile>>(Mock.Of<IUserStore<UserProfile>>(), null, null, null, null, null, null, null, null);
            mockUnitOfWork.Setup(x => x.UserRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(GetTestUserEntities().First());
            
            mockUnitOfWork.Setup(x => x.UserRepository.UpdateAsync(It.IsAny<User>())).ReturnsAsync(user);
            
            
            var userService = new UserService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfileForBll(), mockUserManager.Object);
            //Act
            await userService.UserBan(1, 1);
            
            //Assert
            mockUnitOfWork.Verify(x => x.UserRepository.UpdateAsync(It.Is<User>(u => u.Email==user.Email && u.IsBanned == user.IsBanned) ), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UserService_CheckBan()
        {
            //Arrange
            var user = new User()
            {
                Id = 1, Email = "a@gmail.com", IsBanned = false, EndOfBan = DateTime.Now.AddDays(1)
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserManager = new Mock<UserManager<UserProfile>>(Mock.Of<IUserStore<UserProfile>>(), null, null, null, null, null, null, null, null);
            mockUnitOfWork.Setup(x => x.UserRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(GetTestUserEntities().First());
            mockUnitOfWork.Setup(x => x.UserRepository.GetAllAsync()).ReturnsAsync(GetTestUserEntities());
            mockUnitOfWork.Setup(x => x.UserRepository.UpdateAsync(It.IsAny<User>())).ReturnsAsync(user);
            
            
            var userService = new UserService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfileForBll(), mockUserManager.Object);
            //Act
           var result =  await userService.CheckBan(user.Email);
            
            //Assert
            Assert.AreEqual(false, result);
            mockUnitOfWork.Verify(x => x.UserRepository.UpdateAsync(It.Is<User>(u => u.Email==user.Email && u.IsBanned == user.IsBanned) ), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        
        #region data for tests

            private  IEnumerable<User> GetTestUserEntities()
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
            
            private  IEnumerable<UserProfile> GetTestUserProfileEntities()
            {
                return new List<UserProfile>()
                {
                    new UserProfile()
                    {
                        Id = Guid.NewGuid().ToString(), Email = "a@gmail.com", FirstName = "First", LastName = "Last",
                        PasswordHash = "Hash", PhoneNumber ="235475896"
                    },
                    new UserProfile()
                    {
                        Id = Guid.NewGuid().ToString(), Email = "l@gmail.com", FirstName = "Oleh", LastName = "L", PasswordHash = "Hash", PhoneNumber =
                            "235475896"
                    },
                    new UserProfile()
                    {
                        Id = Guid.NewGuid().ToString(), Email = "n@gmail.com", FirstName = "Mariia", LastName = "M", PasswordHash = "Hash", PhoneNumber =
                            "235475896"
                    }
                };
            }
            
            private  IEnumerable<List<string>> GetTestUserRoles()
            {
                return new List<List<string>>()
                {
                    new List<string>() {"user"},
                    new List<string>() {"admin"},
                    new List<string>() {"user"},
                };
            }
            
            private IEnumerable<UserInfoDto> GetTestUserModels()
            {
                return new List<UserInfoDto>()
                {
                    new UserInfoDto()
                    {
                        Id = 1, Email = "a@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue,
                        FirstName = "First", LastName = "Last", Password = "Hash", PhoneNumber =
                            "235475896",
                        Roles = new List<string>() {"user"}
                    },
                    new UserInfoDto()
                    {
                        Id = 2, Email = "l@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue,
                        FirstName = "Oleh", LastName = "L", Password = "Hash", PhoneNumber =
                            "235475896",
                        Roles = new List<string>() {"admin"}
                    },
                    new UserInfoDto
                    {
                        Id = 3, Email = "n@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue,
                        FirstName = "Mariia", LastName = "M", Password = "Hash", PhoneNumber =
                            "235475896",
                        Roles = new List<string>() {"user"}
                    }
                };
            }
            #endregion
    }
}