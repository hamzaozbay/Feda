using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingToCamera : MonoBehaviour {

    private Transform _cam;
    private Renderer _renderer;


    private void Start() {
        _renderer = transform.GetChild(0).GetComponent<Renderer>();
        _cam = Camera.main.transform;
    }


    private void Update() {
        if (!_renderer.isVisible) return;

        Vector3 lookPos = _cam.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        float eulerY = lookRot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
        transform.rotation = rotation;
    }

}
