using UnityEngine;

public class CubeBonusView : InteractiveObjects
{
    private PlayerView _playerView;

    private void Start()
    {
        _playerView = GameObject.FindObjectOfType<PlayerView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(_playerView.gameObject.name))
        {
            Debug.Log("Собрали бонус");
            _playerView.ChangeColor(gameObject.GetComponent<Renderer>().material.color);
            Destroy(gameObject, 0.1f);
        }
    }
}
