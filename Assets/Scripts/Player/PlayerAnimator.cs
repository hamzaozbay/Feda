using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator {


    protected override void Start() {
        base.Start();

        EquipmentManager.instance.onEquipmentChanged += UpdateAnimations;
        SetAnimations();
    }


    private void UpdateAnimations(Equipment newItem, Equipment oldItem) {
        if (oldItem != null && oldItem is Weapon && newItem == null) {
            currentAnimationSet = DefaultAnimationSet;
            anim.runtimeAnimatorController = DefaultController;
        }
        else if (newItem != null && newItem is Weapon) {
            Weapon newWeapon = (Weapon)newItem;
            overrideController = newWeapon.animController;
            anim.runtimeAnimatorController = overrideController;

            currentAnimationSet = newWeapon.attackClips;
        }
    }

    public void SetAnimations() {
        Equipment[] equipments = EquipmentManager.instance.GetCurrentEquipment();
        if (equipments == null) return;

        Weapon weapon = (Weapon)equipments[3];

        if (weapon == null) {
            currentAnimationSet = DefaultAnimationSet;
            anim.runtimeAnimatorController = DefaultController;
        }
        else {
            overrideController = weapon.animController;
            anim.runtimeAnimatorController = overrideController;

            currentAnimationSet = weapon.attackClips;
        }
    }


    private void OnDestroy() {
        EquipmentManager.instance.onEquipmentChanged -= UpdateAnimations;
    }


}
