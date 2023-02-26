using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Catalog.Slide;
using VKStore.ViewModels.Common;

namespace VKStore.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> Slides { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
 