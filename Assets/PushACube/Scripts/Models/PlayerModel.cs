using System;
using UnityEngine;

public class PlayerModel : PlayerSettings
{
    public event Action DeathEvent;
    public event Action<float> ChangeHealthEvent;
    public event Action<Color> ChangeColorEvent;

    private float _currentHealth;

    public PlayerModel()
    {
        _currentHealth = maxHealth;
    }

    public void PlayerMove(Rigidbody rigidbody, Transform transform)
    {
        float vertical = Input.GetAxis(VERTICAL);
        float horizontal = Input.GetAxis(HORIZONTAL);

        if (rigidbody)
        {
            Vector3 rotate = new Vector3(0, horizontal * turnSpeed, 0);

            rigidbody.MovePosition(transform.position + transform.TransformDirection(new Vector3(0, 0, vertical)) * moveSpeed * Time.fixedDeltaTime);
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotate));
        }
        else
        {
            throw new Exception("Component rigidBody for Player is not defind or null. Check it out");
        }
    }
    public void ChangeColor(Color color)
    {
        ChangeColorEvent?.Invoke(color);
    }

    public void SetNewHealthValue(float damageValue)
    {
        _currentHealth -= damageValue;

        if (_currentHealth > 0)
        {
            ChangeHealthEvent?.Invoke(_currentHealth);
        }
        else
        {
            DeathEvent?.Invoke();
        }
    }

}