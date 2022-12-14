using System;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic;
using UnityEngine;

public sealed class EnemyLaserView : MonoBehaviour
{
    [SerializeField] private Transform _laserStartPoint;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private EnemyToCharacterChaser _chaser;
    [SerializeField] private Color _attackColor = Color.red;
    private readonly Vector3 _fromCharacterOffset = new(0, 3.1f, 0);

    private void Update()
    {
        _laser.positionCount = 2;
        _laser.SetPosition(0, _laserStartPoint.position);
        _laser.SetPosition(1, _chaser.Character.Position + _fromCharacterOffset);
    }

    public async void ShowIsAttacking()
    {
        var startColor = _laser.endColor;
        _laser.endColor = _attackColor;
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        _laser.endColor = startColor;
    }
}