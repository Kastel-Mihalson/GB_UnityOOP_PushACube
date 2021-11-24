using System;
using System.Collections;
using System.Collections.Generic;
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
        _playerModel.ChangeHealth += ChangeHealth;
        _playerModel.Death += Death;
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

    public void Dispose()
    {
        _playerModel.ChangeHealth -= ChangeHealth;
        _playerModel.Death -= Death;
    }
}
