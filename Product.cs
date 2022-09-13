public class Product
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Mark { get; set; }
    public string Amount { get; set; }

    // Relacionamento com a class category
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    // Relacionamento com a class tag em forma de Lista
    public List<Tag> Tags { get; set; }

}
