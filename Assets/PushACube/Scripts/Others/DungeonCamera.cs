using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    private GameObject _target;
    private Vector3 _offset;
    public float _damping = 1f;

    public GameObject Target { set => _target = value; }

    void Start()
    {
        InitTargetAndOffset();
    }

    void LateUpdate()
    {
        if (_target == null)
        {
            InitTargetAndOffset();
        }

        Vector3 desiredPosition = _target.transform.position + _offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * _damping);
        transform.position = position;

        transform.LookAt(_target.transform.position);
    }

    private void InitTargetAndOffset()
    {
        transform.rotation = Quaternion.AngleAxis(80f, Vector3.right);
        _target = GameObject.FindObjectOfType<PlayerView>().gameObject;

        if (_target != null) _offset = transform.position - _target.transform.position;
    }
}
