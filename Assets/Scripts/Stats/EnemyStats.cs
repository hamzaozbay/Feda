using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

    public override void Die() {
        base.Die();

        GetComponent<CharacterAnimator>().GetAnimator().Play("Death", 0);
        GameManager.instance.Player.GetComponent<PlayerStats>().AddHealth(15);
    }

}
