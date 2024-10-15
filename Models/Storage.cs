namespace WebApp_vSem1.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int Сapacity { get; set; }
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
