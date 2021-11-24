using System;
using UnityEngine;

class PlayerController : IDisposable
{
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        _playerModel = model;
        _playerView = view;
    }

    public void Enable()
    {
        _playerModel.ChangeHealthEvent += ChangeHealth;
        _playerModel.DeathEvent += Death;
        _playerModel.ChangeColorEvent += ChangeColor;
    }

    public void PlayerMove(Rigidbody rigidbody, Transform transform)
    {
        _playerModel.PlayerMove(rigidbody, transform);
    }

    public void ChangeHealth(float health)
    {
        _playerView.ChangeHealth(health);
    }

    public void Death()
    {
        _playerView.Death();
        Dispose();
    }

    public void ChangeColor(Color color)
    {
        _playerView.ChangeColor(color);
    }

    public void Dispose()
    {
        _playerModel.ChangeHealthEvent -= ChangeHealth;
        _playerModel.DeathEvent -= Death;
        _playerModel.ChangeColorEvent -= ChangeColor;
    }
}
