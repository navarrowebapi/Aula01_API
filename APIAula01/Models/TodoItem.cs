namespace APIAula01.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        
        // Chave estrangeira: relaciona TodoItem com Category
        public int? CategoryId { get; set; }
        
        // Propriedade de navegação: permite acessar a categoria do TodoItem
        public virtual Category? Category { get; set; }
    }
}
