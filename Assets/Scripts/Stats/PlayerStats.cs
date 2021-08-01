using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

    private CharacterCombat _combat;
    private PlayerHealthUI _playerHealthUI;



    private void Start() {
        _combat = GetComponent<CharacterCombat>();
        _playerHealthUI = GetComponent<PlayerHealthUI>();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        UpdateModifiers();

        currentHealth = GameManager.instance.playerHealth;
        _playerHealthUI.UpdateUI(MaxHealth, currentHealth);
    }


    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
        if (newItem != null) {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);

            if (newItem is Weapon) {
                Weapon newWeapon = (Weapon)newItem;
                currentRange = newWeapon.range;
                _combat.attackSpeed = newWeapon.attackSpeed;
            }
        }

        if (oldItem != null) {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);

            if (oldItem is Weapon && newItem == null) {
                currentRange = baseRange;
                _combat.attackSpeed = 1f;
            }
        }

    }



    private void UpdateModifiers() {
        foreach (Equipment e in EquipmentManager.instance.GetCurrentEquipment()) {
            if (e == null) continue;

            armor.AddModifier(e.armorModifier);
            damage.AddModifier(e.damageModifier);
        }

        if (EquipmentManager.instance.GetCurrentEquipment()[(int)EquipmentSlot.Weapon] == null) {
            currentRange = baseRange;
        }
        else {
            currentRange = ((Weapon)EquipmentManager.instance.GetCurrentEquipment()[(int)EquipmentSlot.Weapon]).range;
        }
    }



    public override void Die() {
        base.Die();

        GameManager.instance.PlayerDied();
    }

    public void AddHealth(int value) {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, -5, MaxHealth);

        _playerHealthUI.UpdateUI(MaxHealth, currentHealth);
    }


    private void OnDestroy() {
        EquipmentManager.instance.onEquipmentChanged -= OnEquipmentChanged;
    }



}
