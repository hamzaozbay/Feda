using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Skill {

    [SerializeField] private float _radius = 3.25f;
    [SerializeField] private float _duration = 5f;



    public override void Execute() {
        base.Execute();

        TornadoAttack();
    }


    private void TornadoAttack() {
        skillManager.anySkillPlaying = true;
        canExecute = false;

        skillManager.PlayerCombat.CanAttackFalse();
        skillManager.PlayerAnimator.GetAnimator().SetBool("tornadoAttack", true);
        skillManager.PlayerMotor.GetAgent().angularSpeed = 0f;

        InvokeRepeating("TornadoDamage", 0f, 1f);
        skillManager.Player.trail.SetActive(true);
        StartCoroutine(waitForTornadoAnimation());
        StartCoroutine(waitForTornadoCoolDown());
    }
    private IEnumerator waitForTornadoAnimation() {
        yield return new WaitForSeconds(_duration);
        skillManager.PlayerAnimator.GetAnimator().SetBool("tornadoAttack", false);
        skillManager.Player.trail.SetActive(false);
        CancelInvoke("TornadoDamage");
        DonePlayingSkill();
    }
    private IEnumerator waitForTornadoCoolDown() {
        yield return new WaitForSeconds(cooldDown);
        canExecute = true;
    }
    private void TornadoDamage() {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _radius);

        foreach (var hitCollider in hitColliders) {
            CharacterStats enemyStats = hitCollider.GetComponent<CharacterStats>();
            if (hitCollider.CompareTag("Enemy") && enemyStats != null) {
                enemyStats.TakeDamage(damage + skillManager.PlayerStats.damage.GetValue());
            }
        }
    }



    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }

}
