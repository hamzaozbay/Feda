using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryPanelAnim : MonoBehaviour {

    [SerializeField] private float _startPosition = -893f;
    [SerializeField] private float _endPosition = -223f;

    private RectTransform _rectTransform;



    private void Start() {
        _rectTransform = GetComponent<RectTransform>();
    }


    public void Open() {
        _rectTransform.DOLocalMoveY(_endPosition, 1f, false).SetEase(Ease.InOutBack);
    }

    public void Close() {
        _rectTransform.DOLocalMoveY(_startPosition, 1f, false).SetEase(Ease.InOutBack);
    }


}
