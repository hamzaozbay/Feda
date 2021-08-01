using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    public bool canAttack = true;
    public bool InCombat { get; set; }
    public bool isAttacking { get; set; }
    public event System.Action onAttack;


    private float _attackCooldown = 0f;
    private const float _combatCooldown = 3f;
    private float _lastAttackTime;

    private CharacterStats _myStats;
    private CharacterStats _opponentStats;



    private void Start() {
        _myStats = GetComponent<CharacterStats>();
    }



    private void Update() {
        _attackCooldown -= Time.deltaTime;

        if (Time.time - _lastAttackTime > _combatCooldown) {
            InCombat = false;
        }

        if (Time.time - _lastAttackTime > attackSpeed) {
            canAttack = true;
        }
    }


    public void Attack(CharacterStats targetStats) {
        if (canAttack && _myStats.CurrentHealth > 0f) {
            isAttacking = true;
            _opponentStats = targetStats;

            if (onAttack != null) {
                onAttack();
            }

            InCombat = true;
            _lastAttackTime = Time.time;
        }
    }


    public void AttackHit_AnimationEvent() {
        _opponentStats.TakeDamage(_myStats.damage.GetValue());

        if (_opponentStats.CurrentHealth <= 0) {
            InCombat = false;
        }
    }

    public bool GetCanAttack() {
        return canAttack;
    }

    public void SetLastAttackTime() {
        _lastAttackTime = Time.time;
    }

    public void CanAttackTrue() {
        canAttack = true;
    }
    public void CanAttackFalse() {
        canAttack = false;
    }

}
