using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : Skill {

    [SerializeField] private float _duration;



    public override void Execute() {
        AttackSpeedSkill();
    }


    private void AttackSpeedSkill() {
        canExecute = false;

        skillManager.PlayerCombat.CanAttackFalse();
        TrailRenderer tr = skillManager.Player.trail.GetComponent<TrailRenderer>();
        tr.startColor = Color.red;
        tr.endColor = Color.red;

        Weapon weapon = (Weapon)EquipmentManager.instance.GetCurrentEquipment()[(int)EquipmentSlot.Weapon];
        skillManager.PlayerCombat.attackSpeed = weapon.attackSpeed / 2f;

        skillManager.PlayerStats.damage.AddModifier(damage);

        StartCoroutine(waitForAttackSpeedBuff());
        StartCoroutine(waitForAttackSpeedCoolDown());
    }
    private IEnumerator waitForAttackSpeedBuff() {
        yield return new WaitForSeconds(_duration);
        Weapon weapon = (Weapon)EquipmentManager.instance.GetCurrentEquipment()[(int)EquipmentSlot.Weapon];
        skillManager.PlayerCombat.attackSpeed = weapon.attackSpeed;

        skillManager.PlayerStats.damage.RemoveModifier(damage);

        TrailRenderer tr = skillManager.Player.trail.GetComponent<TrailRenderer>();
        tr.startColor = Color.white;
        tr.endColor = Color.white;
    }
    private IEnumerator waitForAttackSpeedCoolDown() {
        yield return new WaitForSeconds(cooldDown);
        canExecute = true;
    }
}
