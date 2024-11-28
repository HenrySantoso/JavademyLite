using SQLite;

namespace JavademyLite.Models;

[Table("category")]
public class Category
{
    [PrimaryKey, AutoIncrement]
    public int CategoryId { get; set; }

    [MaxLength(250), Unique]
    public string? Name { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }
}
