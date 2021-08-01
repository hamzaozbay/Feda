using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : Skill {

    [SerializeField] private float _radius;
    [SerializeField] private GameObject _particlePrefab;



    public override void Execute() {
        base.Execute();

        MeteorSkill();
    }

    private void MeteorSkill() {
        canExecute = false;

        skillManager.PlayerController.canMove = false;
        skillManager.PlayerCombat.CanAttackFalse();
        skillManager.PlayerMotor.StopPlayer();
        skillManager.PlayerAnimator.GetAnimator().SetTrigger("jumpDownAttack");
        StartCoroutine(waitForJumpDownAnimation());
        StartCoroutine(waitForJumpDownCoolDown());
    }
    private IEnumerator waitForJumpDownAnimation() {
        yield return new WaitForSeconds(AnimationLength.ClipLength(skillManager.PlayerAnimator.GetAnimator(), "Armature|JumpUpDownAttack"));
        DonePlayingSkill();
    }
    private IEnumerator waitForJumpDownCoolDown() {
        yield return new WaitForSeconds(cooldDown);
        canExecute = true;
    }
    public void JumpDownDamage() {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _radius);

        foreach (var hitCollider in hitColliders) {
            CharacterStats enemyStats = hitCollider.GetComponent<CharacterStats>();
            if (hitCollider.CompareTag("Enemy") && enemyStats != null) {
                enemyStats.TakeDamage(damage + skillManager.PlayerStats.damage.GetValue());
            }
        }

        Instantiate(_particlePrefab, this.transform.position, Quaternion.identity);
    }



    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }

}
