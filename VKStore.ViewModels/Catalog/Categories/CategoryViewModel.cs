using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.Data.Enums;

namespace VKStore.ViewModels.Catalog.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Status Status { get; set; }
        public int TotalProduct { get; set; }
    }
}
