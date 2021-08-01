using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineLogic : MonoBehaviour {

    [SerializeField] private Outline _outline;

    private bool _interactableFocused = false;



    private void Start() {
        _outline.enabled = false;
    }


    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _outline.enabled = true;
    }


    private void OnMouseExit() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!_interactableFocused)
            _outline.enabled = false;
    }


    public void OutlineEnable() {
        _interactableFocused = true;
        _outline.enabled = true;
    }

    public void OutlineDisable() {
        _interactableFocused = false;
        _outline.enabled = false;
    }

}
