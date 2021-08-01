using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Vector3 _offset = new Vector3(5f, -9.5f, 6.5f);
    [SerializeField] private float _pitch = 1f;

    private Transform _target;



    private void Start() {
        _target = GameManager.instance.Player.transform;
    }


    private void LateUpdate() {
        if (_target == null) { return; }

        transform.position = _target.position - _offset;
        transform.LookAt(_target.position + Vector3.up * _pitch);
    }

}
