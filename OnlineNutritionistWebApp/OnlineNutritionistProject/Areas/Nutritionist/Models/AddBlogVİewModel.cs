using CoreLayer.Models;

namespace OnlineNutritionistProject.Areas.Nutritionist.Models
{
    public class AddBlogVİewModel : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int AppUserId { get; set; }
        public IFormFile CoverImage { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public BlogFeature BlogFeature { get; set; }
    }
}
