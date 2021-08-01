using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

    public Image healthFill;

    private CharacterStats _myStats;



    private void Start() {
        _myStats = GetComponent<CharacterStats>();
        _myStats.OnHealthChanged += HealthFillChanged;
    }


    private void HealthFillChanged(int maxHealth, int currentHealth, int changedValue) {
        UpdateUI(maxHealth, currentHealth);
    }


    public void UpdateUI(int maxHealth, int currentHealth) {
        float healthPercent = currentHealth / (float)maxHealth;
        healthFill.fillAmount = healthPercent;
    }


    private void OnDestroy() {
        GetComponent<CharacterStats>().OnHealthChanged -= HealthFillChanged;
    }

}
