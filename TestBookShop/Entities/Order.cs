namespace TestBookShop.Entities;

public class Order
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public List<Book> Books { get; set; } = default!;
    public DateTime TimeOfOrder { get; set; }
}