using UnityEngine;

public abstract class InteractiveObjects : MonoBehaviour
{
    private float _lengthFlay;

    protected PlayerView _playerView;
    protected PlayerHUDView _playerHUDView;

    private void Start()
    {
        _playerView = GameObject.FindObjectOfType<PlayerView>();
        _playerHUDView = GameObject.FindObjectOfType<PlayerHUDView>();
    }
    private void Awake()
    {
        _lengthFlay = Random.Range(0.5f, 1.5f);
    }

    public virtual void CubeBonusPingPongFlyAnim()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.PingPong(Time.time, _lengthFlay),
                transform.localPosition.z);
    }
}
