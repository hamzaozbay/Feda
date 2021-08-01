using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    private GameObject _playerObject;
    private CharacterStats _myStats;
    private CharacterCombat _playerCombat;
    private SkillManager _playerSkillManager;



    private void Start() {
        _playerObject = GameManager.instance.Player;
        _myStats = GetComponent<CharacterStats>();
        _playerCombat = _playerObject.GetComponent<CharacterCombat>();
        _playerSkillManager = _playerObject.transform.GetChild(0).GetComponent<SkillManager>();
    }


    public override void Interact() {
        base.Interact();

        if (_playerCombat != null && _playerCombat.GetCanAttack() && !_playerCombat.isAttacking && !_playerSkillManager.anySkillPlaying) {
            _playerCombat.Attack(_myStats);
        }
    }


}
