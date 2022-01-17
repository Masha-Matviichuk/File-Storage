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
    public class FileRepositoryTests
    {
        private DbContextOptions<FileStorageDBContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = UnitTestsHelper.GetUnitTestDbOptions();
        }

        [Test]
        public async Task FileRepository_GetAllAsync()
        {
            using (var context = new FileStorageDBContext(_options))
            {
                //arrange
                var fileRepository = new FileRepository(context);

                //act
                var files = (await fileRepository.GetAllAsync()).ToList();

                //assert
                Assert.AreEqual(3, files.Count);
            }
        }

        [Test]
        public async Task FileRepository_GetByIdWithDetails()
        {
            using (var context = new FileStorageDBContext(_options))
            {
                //arrange
                int id = 1;
                var fileRepository = new FileRepository(context);

                //act
                var file = await fileRepository.GetByIdAsync(id);

                //assert
                Assert.IsNotNull(file);
                Assert.AreEqual("First", file.Title);
                Assert.AreEqual("Simple file", file.Description);
                Assert.AreEqual(1032, file.Size);
                Assert.AreEqual(new DateTime(2022, 1, 14), file.Upload);
                Assert.AreEqual("", file.Url);
                Assert.AreEqual(1, file.UserId);
                Assert.AreEqual(1, file.AccessId);
            }
        }
        
        [Test]
        public async Task FileRepository_CreateAsync()
        {
            using (var context = new FileStorageDBContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var fileRepository = new FileRepository(context);
                var file = new File(){Id = 4};

                await fileRepository.CreateAsync(file);
                await context.SaveChangesAsync();
                
                Assert.AreEqual(4, context.Files.Count());
            }
        }

        [Test]
        public async Task FileRepository_DeleteByIdAsync()
        {
            using (var context = new FileStorageDBContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var fileRepository = new FileRepository(context);
                
                await fileRepository.DeleteByIdAsync(1);
                await context.SaveChangesAsync();
                
                Assert.AreEqual(2, context.Files.Count());
            }
        }

        [Test]
        public async Task FileRepository_UpdateAsync()
        {
            using (var context = new FileStorageDBContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var fileRepository = new FileRepository(context);

                var file = new File(){ Id = 2, Title = "New Second", Description = "Simple file description for second file", AccessId = 1, Url = "url", Upload = new DateTime(2022, 3, 14), Size = 2022, UserId = 2};

                await fileRepository.UpdateAsync(file);
                await context.SaveChangesAsync();
 
                Assert.AreEqual("New Second", file.Title);
                Assert.AreEqual("Simple file description for second file", file.Description);
                Assert.AreEqual(2022, file.Size);
                Assert.AreEqual(new DateTime(2022, 3, 14), file.Upload);
                Assert.AreEqual("url", file.Url);
                Assert.AreEqual(2, file.UserId);
                Assert.AreEqual(1, file.AccessId);
            }
        }
    }
}