namespace Infrastructure.Model
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}
