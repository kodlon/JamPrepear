namespace Entities.Interfaces
{
    public interface IDamageable
    {
        int Health { get; set; }
        void TakeDamage(int amount);
    }
}