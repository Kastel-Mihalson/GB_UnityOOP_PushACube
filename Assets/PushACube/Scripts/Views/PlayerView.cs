using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerModel _playerModel;
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

    public PlayerModel PlayerModel
    {
        get => _playerModel;
        set => _playerModel = value;
    }

    public void ChangeHealth(float health)
    {

    }

    public void Death()
    {

    }

    public void ChangeColor(Color color)
    {
        var playerLight = transform.GetComponent<Light>();
        var playerMaterial = transform.GetComponent<Renderer>().material;

        playerLight.color = color;
        playerLight.intensity += 1;
        playerMaterial.color = color;
    }

    public void ChangeSpeed(float speed)
    {
        Debug.Log($"Установили новую скорость {speed}");
        _playerModel.MoveSpeed += speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(_gameOverDeathLine.transform.name))
        {
            IsGameOver = true;
        }
    }
}