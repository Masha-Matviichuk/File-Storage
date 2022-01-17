using System;
using AutoMapper;
using BLL.Configuration;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    internal static class UnitTestsHelper
    {
        public static DbContextOptions<FileStorageDBContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<FileStorageDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new FileStorageDBContext(options))
            {
                SeedData(context);
            }
            return options;
        }

        public static void SeedData(FileStorageDBContext context)
        {
            context.Accesses.Add(new Access { Id = 1, Modifier = "Private"});
            context.Accesses.Add(new Access { Id = 2, Modifier = "Public"});
            context.Users.Add(new User {Id = 1, Email = "a@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue});
            context.Users.Add(new User {Id = 2, Email = "l@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue});
            context.Users.Add(new User {Id = 3, Email = "n@gmail.com", IsBanned = false, EndOfBan = DateTime.MinValue});
            context.Files.Add(new File { Id = 1, Title = "First", Description = "Simple file", AccessId = 1, Url = "", Upload = new DateTime(2022, 1, 14), Size = 1032, UserId = 1});
            context.Files.Add(new File { Id = 2, Title = "Second", Description = "Simple file", AccessId = 2, Url = "", Upload = new DateTime(2022, 2, 14), Size = 1032, UserId = 2});
            context.Files.Add(new File { Id = 3, Title = "Third", Description = "Simple file", AccessId = 1, Url = "", Upload = new DateTime(2022, 1, 15), Size = 1032, UserId = 2});
            context.SaveChanges();
        }

        public static Mapper CreateMapperProfileForBll()
        {
            var myProfileForBll = new AutoMapperProfileForBll();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfileForBll));

            return new Mapper(configuration);
        }
        
        public static Mapper CreateMapperProfileForPl()
        {
            var myProfileForPl = new AutoMapperProfileForBll();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfileForPl));

            return new Mapper(configuration);
        }
    }
}