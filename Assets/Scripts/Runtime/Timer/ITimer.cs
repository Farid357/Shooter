using System.Threading.Tasks;

namespace Shooter.Model
{
    public interface ITimer
    {
        Task End();

        ITimer Restart();
    }
}