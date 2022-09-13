public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Forçar criação coluna NOT NULL
    public int ProductId { get; set; }
}
