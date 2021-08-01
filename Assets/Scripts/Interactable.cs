using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;

    [SerializeField] private Transform _interactionTransform;

    private bool _isFocused = false;
    private Transform _player;
    private bool _hasInteracted = false;



    private void Update() {
        if (_isFocused && !_hasInteracted) {
            float distance = Vector3.Distance(_player.position, _interactionTransform.position);

            if (distance <= radius) {
                _hasInteracted = true;
                Interact();
            }
        }
    }


    public virtual void Interact() {
        //Debug.Log("Interact with " + transform.name);
    }


    public void OnFocused(Transform playerTransform) {
        _isFocused = true;
        _player = playerTransform;
        _hasInteracted = false;

        GetComponent<OutlineLogic>().OutlineEnable();
    }

    public void OnDefocused() {
        _isFocused = false;
        _player = null;
        _hasInteracted = false;

        GetComponent<OutlineLogic>().OutlineDisable();
    }


    private void OnDrawGizmosSelected() {
        if (_interactionTransform == null) {
            _interactionTransform = this.transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionTransform.position, radius);
    }


    public Transform InteractionTransform { get { return _interactionTransform; } }

}
