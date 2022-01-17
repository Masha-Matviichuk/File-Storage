using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.DataTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private DbContextOptions<FileStorageDBContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = UnitTestsHelper.GetUnitTestDbOptions();
        }

        [Test]
        public async Task UserRepository_GetAllAsync()
        {
            using (var context = new FileStorageDBContext(_options))
            {
                //arrange
                var userRepository = new UserRepository(context);

                //act
                var users = (await userRepository.GetAllAsync()).ToList();

                //assert
                Assert.AreEqual(3, users.Count);
            }
        }

        [Test]
        public async Task UserRepository_GetByIdWithDetails_ReturnValueById()
        {
            using (var context = new FileStorageDBContext(_options))
            {
                //arrange
                int id = 1;
                var userRepository = new UserRepository(context);

                //act
                var user = await userRepository.GetByIdAsync(id);

                //assert
                Assert.IsNotNull(user);
                Assert.AreEqual("a@gmail.com", user.Email);
                Assert.AreEqual(false, user.IsBanned);
                Assert.AreEqual(DateTime.MinValue, user.EndOfBan);
            }
        }
        
        [Test]
        public async Task UserRepository_CreateAsync()
        {
            using (var context = new FileStorageDBContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var userRepository = new UserRepository(context);
                var user = new User(){Id = 4};

                await userRepository.CreateAsync(user);
                await context.SaveChangesAsync();
                
                Assert.AreEqual(4, context.Users.Count());
            }
        }

        [Test]
        public async Task UserRepository_DeleteByIdAsync()
        {
            using (var context = new FileStorageDBContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var userRepository = new UserRepository(context);
                
                await userRepository.DeleteByIdAsync(1);
                await context.SaveChangesAsync();
                
                Assert.AreEqual(2, context.Users.Count());
            }
        }

        [Test]
        public async Task UserRepository_UpdateAsync()
        {
            using (var context = new FileStorageDBContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var userRepository = new UserRepository(context);

                var user = new User(){ Id = 1, Email = "u@gmail.com", IsBanned = true, EndOfBan = DateTime.MinValue.AddDays(4)};

                await userRepository.UpdateAsync(user);
                await context.SaveChangesAsync();

                Assert.AreEqual(1, user.Id);
                Assert.AreEqual("u@gmail.com", user.Email);
                Assert.AreEqual(true, user.IsBanned);
                Assert.AreEqual(DateTime.MinValue.AddDays(4), user.EndOfBan);
            }
        }
    }
}