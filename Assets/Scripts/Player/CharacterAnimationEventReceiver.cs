using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour {

    [SerializeField] private CharacterCombat _combat;
    [SerializeField] private SkillManager _skillManager;
    [SerializeField] private Player _player;



    public void AttackHitEvent() {
        _combat.AttackHit_AnimationEvent();
    }

    public void Attacked() {
        _combat.isAttacking = false;
        _combat.InCombat = true;
        _combat.CanAttackFalse();
        _combat.SetLastAttackTime();
    }

    public void CanAttackTrue() {
        _combat.CanAttackTrue();
    }
    public void CanAttackFalse() {
        _combat.CanAttackFalse();
    }


    public void JumpDownAttack() {
        _skillManager.MeteorAttackSkill.JumpDownDamage();
    }


    public void EnableTrail() {
        _player.trail.SetActive(true);
        StartCoroutine(disableWait());
    }
    private IEnumerator disableWait() {
        yield return new WaitForSeconds(1f);
        _player.trail.SetActive(false);
    }


    public void DestroyObject() {
        StartCoroutine(WaitForDestroy());
    }
    private IEnumerator WaitForDestroy() {
        yield return new WaitForSeconds(3f);
        Destroy(transform.parent.gameObject);
    }

}
