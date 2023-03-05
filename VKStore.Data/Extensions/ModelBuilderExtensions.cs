using VKStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace VKStore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser() {
                    Id = Guid.NewGuid(),
                    FullName = "Văn Kiếm",
                    UserName = "vankiemd3v",
                    PasswordHash = hasher.HashPassword(null, "Admin123@"),
                    Email = "vankiemd3v@gmail.com",
                    PhoneNumber = "0336154196",
                }
                ); ;
            modelBuilder.Entity<Slide>().HasData(
                new Slide() { Id = 1, Name = "Slide số 1", Description = "Slide số 1", SortOrder = 1, Url ="#", Image= "/img/carousel-1.jpg", Status = Status.Active},
                new Slide() { Id = 2, Name = "Slide số 2", Description = "Slide số 2", SortOrder = 2, Url = "#", Image = "/img/carousel-2.jpg", Status = Status.InActive },
                new Slide() { Id = 3, Name = "Slide số 3", Description = "Slide số 3", SortOrder = 3, Url = "#", Image = "/img/carousel-3.jpg", Status = Status.InActive }
                );
        }

    }
}
