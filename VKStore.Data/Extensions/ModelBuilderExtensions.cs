using VKStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.Data.Enums;

namespace VKStore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasData(
            //    new Product() { Id = 1, Name = "IPhone Xs Max", Price = 100 },
            //    new Product() { Id = 2, Name = "IPhone 14 Pro Max", Price = 999 }
            //    );
            modelBuilder.Entity<Slide>().HasData(
                new Slide() { Id = 1, Name = "Slide số 1", Description = "Slide số 1", SortOrder = 1, Url ="#", Image= "/img/carousel-1.jpg", Status = Status.Active},
                new Slide() { Id = 2, Name = "Slide số 2", Description = "Slide số 2", SortOrder = 2, Url = "#", Image = "/img/carousel-2.jpg", Status = Status.InActive },
                new Slide() { Id = 3, Name = "Slide số 3", Description = "Slide số 3", SortOrder = 3, Url = "#", Image = "/img/carousel-3.jpg", Status = Status.InActive }
                );
        }

    }
}
