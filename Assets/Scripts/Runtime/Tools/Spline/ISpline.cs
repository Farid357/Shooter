using UnityEngine;

namespace Shooter.Tools
{
    public interface ISpline
    {
        Vector3 GetNextPoint();

        bool CanGetNextPoint();

        void Reset();
    }
}