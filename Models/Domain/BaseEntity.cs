namespace CrudApi.Models.Domain
{
    public abstract class BaseEntity<Tkey>
    {
        public Tkey Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

    }
}
