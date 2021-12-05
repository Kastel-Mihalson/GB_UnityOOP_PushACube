using System;
using UnityEngine;

class PlayerController
{
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        _playerModel = model;
        _playerView = view;
    }

    // Подписка на события
    public void EnableEvents()
    {
        _playerView.ChangePlayerColorEvent += ChangeColor;
        _playerView.ChangePlayerSpeedEvent += ChangeSpeed;
    }

    // Управление игрока
    public void PlayerMove(Rigidbody rigidbody, Transform transform)
    {
        float vertical = Input.GetAxis(_playerModel.VERTICAL);
        float horizontal = Input.GetAxis(_playerModel.HORIZONTAL);

        if (rigidbody)
        {
            Vector3 rotate = new Vector3(0, horizontal * _playerModel.turnSpeed, 0);

            rigidbody.MovePosition(transform.position + transform.TransformDirection(new Vector3(0, 0, vertical)) * _playerModel.MoveSpeed * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotate));
        }
        else
        {
            throw new Exception("Component rigidBody for Player is not defind or null. Check it out");
        }
    }

    // Изменения цвета игрока на цвет объекта, при соприкосновении с ним
    // А также увеличение интенсивности света
    public void ChangeColor(Color color)
    {
        var playerLight = _playerView.transform.GetComponent<Light>();
        var playerMaterial = _playerView.transform.GetComponent<Renderer>().material;

        playerLight.color = color;
        playerMaterial.color = color;

        if (playerLight.intensity < 30) playerLight.intensity += 1;
    }

    // Измение скорости игрока
    public void ChangeSpeed(float speed)
    {
        _playerModel.MoveSpeed += speed;
    }

    public void SetPlayerDefaultSpeedAfterBonusEffect()
    {
        if (_playerModel.moveSpeed != _playerModel.oldSpeed)
        {
            if (_playerModel.moveSpeedBonusTime > 0)
            {
                _playerModel.moveSpeedBonusTime -= Time.deltaTime;
            }
            else
            {
                _playerModel.moveSpeed = _playerModel.oldSpeed;
                _playerModel.moveSpeedBonusTime = _playerModel.oldMoveSpeedBonusTime;
            }
        }
    }

    // Отписка от событий
    public void DisposeEvents()
    {
        _playerView.ChangePlayerColorEvent -= ChangeColor;
        _playerView.ChangePlayerSpeedEvent -= ChangeSpeed;
    }
}
