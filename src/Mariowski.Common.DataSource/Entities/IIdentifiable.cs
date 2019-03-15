namespace Mariowski.Common.DataSource.Entities
{
    public interface IIdentifiable<out TId>
    {
        TId Id { get; }
    }
}