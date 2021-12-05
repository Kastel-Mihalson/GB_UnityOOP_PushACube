using UnityEngine;

public class CubeSpeedBonusView : InteractiveObjects
{
    private void OnTriggerEnter(Collider other)
    {
        if (_playerView != null && _playerHUDView != null)
        {
            if (other.name.Equals(_playerView.gameObject.name))
            {
                Debug.Log("������� ����� ��������");
                _playerView.ChangeColor(gameObject.GetComponent<Renderer>().material.color);
                _playerView.ChangeSpeed(3f);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
