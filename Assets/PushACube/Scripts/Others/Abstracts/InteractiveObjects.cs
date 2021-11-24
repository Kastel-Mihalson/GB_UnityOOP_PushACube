using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObjects : MonoBehaviour
{
    private float _lengthFlay;

    private void Awake()
    {
        _lengthFlay = Random.Range(0.5f, 1.5f);
    }

    public void CubeBonusPingPongFlyAnim()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.PingPong(Time.time, _lengthFlay),
                transform.localPosition.z);
    }
}
