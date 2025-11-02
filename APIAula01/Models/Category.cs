namespace APIAula01.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        // Propriedade de navegação: uma categoria pode ter vários TodoItems
        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
