using Microsoft.EntityFrameworkCore;


namespace BeverageMachine.EntityInterface
{
    public interface IBought<T> where T : class
    {
        void Buy(DbSet<T> db, string name, decimal amount, int qnt)
        {
        }
    }
}
