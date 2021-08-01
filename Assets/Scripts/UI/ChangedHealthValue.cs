using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ChangedHealthValue : MonoBehaviour {

    public Transform target;

    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    private Transform _cam;



    private void Start() {
        _cam = Camera.main.transform;
        _rect = GetComponent<RectTransform>();

        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.DOFade(0f, .8f).SetUpdate(true);

        _rect.DOLocalMoveY(_rect.localPosition.y + 2.5f, 1f).OnComplete(() => {
            Destroy(this.gameObject);
        }).SetUpdate(true);
    }


    private void LateUpdate() {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.forward = _cam.transform.forward;
    }

}
