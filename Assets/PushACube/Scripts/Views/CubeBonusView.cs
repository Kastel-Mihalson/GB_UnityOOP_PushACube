using UnityEngine;

public class CubeBonusView : InteractiveObjects
{
    private void OnTriggerEnter(Collider other)
    {
        if (_playerView != null && _playerHUDView != null)
        {
            if (other.name.Equals(_playerView.gameObject.name))
            {
                Debug.Log("Собрали бонус");
                _playerView.ChangeColor(gameObject.GetComponent<Renderer>().material.color);
                _playerHUDView.CurrentCubeBonusCount(1);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
