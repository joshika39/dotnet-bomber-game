namespace Bomber.BL.Entities
{
    public interface IEnemy : INpc
    {
        Task ExecuteAsync();
    }
}
