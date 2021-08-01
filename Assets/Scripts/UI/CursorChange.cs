using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private CursorType cursorType;

    private bool _mouseOver = false;



    //// FOR GAMEOBJECTS
    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (cursorType == CursorType.SELECT) {
            Cursor.SetCursor(GameManager.instance.CursorSprite.CursorSelect, Vector2.zero, CursorMode.Auto);
        }
        else {
            Cursor.SetCursor(GameManager.instance.CursorSprite.CursorAttack, Vector2.zero, CursorMode.Auto);
        }

        _mouseOver = true;
    }

    private void OnMouseExit() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Cursor.SetCursor(GameManager.instance.CursorSprite.CursorNormal, Vector2.zero, CursorMode.Auto);
        _mouseOver = false;
    }


    //// FOR UI OBJECTS
    public void OnPointerEnter(PointerEventData eventData) {
        Cursor.SetCursor(GameManager.instance.CursorSprite.CursorSelect, Vector2.zero, CursorMode.Auto);
        _mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Cursor.SetCursor(GameManager.instance.CursorSprite.CursorNormal, Vector2.zero, CursorMode.Auto);
        _mouseOver = false;
    }



    private void OnDestroy() {
        if (_mouseOver) {
            Cursor.SetCursor(GameManager.instance.CursorSprite.CursorNormal, Vector2.zero, CursorMode.Auto);
            _mouseOver = false;
        }

    }
    private void OnDisable() {
        if (_mouseOver) {
            Cursor.SetCursor(GameManager.instance.CursorSprite.CursorNormal, Vector2.zero, CursorMode.Auto);
            _mouseOver = false;
        }
    }


    private enum CursorType {
        SELECT,
        ATTACK
    }

}

