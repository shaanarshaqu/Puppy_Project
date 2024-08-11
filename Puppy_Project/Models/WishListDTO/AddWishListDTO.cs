using System.ComponentModel.DataAnnotations;

namespace Puppy_Project.Models.WishListDTO
{
    public class AddWishListDTO
    {
        [Required]
        public int User_Id { get; set; }
        public int Product_Id { get; set;}
    }
}
