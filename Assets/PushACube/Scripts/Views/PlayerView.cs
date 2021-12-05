using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public event Action<Color> ChangePlayerColorEvent;
    public event Action<float> ChangePlayerSpeedEvent;

    private GameObject _gameOverDeathLine;
    private bool _isGameOver;

    public GameObject GameOverDeathLine
    {
        get => _gameOverDeathLine;
        set => _gameOverDeathLine = value;
    }

    public bool IsGameOver
    {
        get => _isGameOver;
        set => _isGameOver = value;
    }

    public void ChangeColor(Color color)
    {
        ChangePlayerColorEvent?.Invoke(color);
    }

    public void ChangeSpeed(float speed)
    {
        ChangePlayerSpeedEvent?.Invoke(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(_gameOverDeathLine.transform.name))
        {
            IsGameOver = true;
        }
    }
}