using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MoveIndicator : MonoBehaviour {

    private Image _img;
    private ObjectPool _pool;
    private bool _initialized = false;



    private void Awake() {
        _img = GetComponent<Image>();
        _pool = transform.parent.GetComponent<ObjectPool>();
    }


    private void Start() {
        _initialized = true;
    }


    private void OnEnable() {
        _img.DOFade(1f, .15f).OnComplete(() => {
            _img.DOFade(0f, .5f).OnComplete(() => {
                this.transform.SetParent(_pool.transform);
                this.gameObject.SetActive(false);
            });
        });
    }


    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }

}

