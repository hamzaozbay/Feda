using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool canMove = true;

    [SerializeField] private LayerMask movementMask, interactableMask;

    private Interactable _focus;
    private Camera _cam;
    private PlayerMotor _motor;
    private PlayerStats _stats;
    private CharacterCombat _combat;
    private PlayerAnimator _playerAnimator;
    private SkillManager _skills;




    private void Start() {
        _cam = Camera.main;
        _motor = GetComponent<PlayerMotor>();
        _stats = GetComponent<PlayerStats>();
        _combat = GetComponent<CharacterCombat>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _skills = GetComponent<SkillManager>();
    }


    private void Update() {
        if (!Input.GetMouseButton(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!canMove) return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200, interactableMask)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            Enemy enemy = interactable.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.radius = _stats.GetCurrentRange();
                SetFocus(enemy);
            }
            else if (interactable != null) {
                SetFocus(interactable);
                _combat.InCombat = false;
            }
        }
        else if (Physics.Raycast(ray, out hit, 200, movementMask)) {
            _combat.isAttacking = false;
            _combat.InCombat = false;
            RemoveFocus();
            _motor.MoveToPoint(hit.point);

            MoveIndicator(hit.point);
        }
    }



    private void SetFocus(Interactable newFocus) {
        if (newFocus != _focus) {
            if (_focus != null)
                _focus.OnDefocused();

            _focus = newFocus;
            _motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(this.transform);
    }

    private void RemoveFocus() {
        if (_focus != null)
            _focus.OnDefocused();

        _focus = null;
        _motor.StopFollowingTarget();

    }


    private void MoveIndicator(Vector3 pos) {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.MoveIndicatorPool != null) {
            GameObject indicator = GameManager.instance.MoveIndicatorPool.GetObject();
            indicator.transform.SetParent(GameManager.instance.WorldSpaceCanvas.transform);
            indicator.transform.position = pos + new Vector3(0f, .05f, 0f);
            indicator.SetActive(true);
        }
    }



}
