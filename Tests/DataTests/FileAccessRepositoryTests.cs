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
    public class FileAccessRepositoryTests
    {
        private DbContextOptions<FileStorageDBContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = UnitTestsHelper.GetUnitTestDbOptions();
        }

        [Test]
        public async Task FileAccessRepository_GetAllAsync()
        {
            using (var context = new FileStorageDBContext(_options))
            {
                //arrange
                var fileAccessRepository = new FileAccessRepository(context);

                //act
                var accesses = (await fileAccessRepository.GetAll()).ToList();

                //assert
                Assert.AreEqual(2, accesses.Count);
            }
        }
    }
}