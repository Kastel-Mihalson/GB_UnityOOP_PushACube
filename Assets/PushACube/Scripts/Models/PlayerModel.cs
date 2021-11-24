using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : PlayerSettings
{
    public event Action Death;
    public event Action<float> ChangeHealth;

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

    public void SetNewHealthValue(float damageValue)
    {
        _currentHealth -= damageValue;

        if (_currentHealth > 0)
        {
            ChangeHealth?.Invoke(_currentHealth);
        }
        else
        {
            Death?.Invoke();
        }
    }

}