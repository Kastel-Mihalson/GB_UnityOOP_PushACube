using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBonusView : InteractiveObjects
{
    private GameObject _playerGO;

    private void Start()
    {
        _playerGO = GameObject.FindObjectOfType<PlayerView>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(_playerGO.name))
        {
            Debug.Log("Собрали бонус");
            Destroy(gameObject, 0.2f);
        }
    }
}
