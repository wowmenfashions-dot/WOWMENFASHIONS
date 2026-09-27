using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wowmenfashions.Data.Entities;

public class HomepageImage
{
    [Key]
    public int Id { get; set; }

    [Required]
    public byte[] ImageData { get; set; } = Array.Empty<byte>();

    [Required]
    [MaxLength(50)]
    public string ContentType { get; set; } = "image/avif";

    [Required]
    public int DisplayOrder { get; set; }
}
