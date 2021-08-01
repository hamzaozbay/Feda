using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public event System.Action<int, int, int> OnHealthChanged;
    public event System.Action OnDied;

    public Stat damage;
    public Stat armor;
    [SerializeField] protected int currentHealth;
    protected float baseRange = 3f;
    protected float currentRange = 2f;
    protected bool isDead = false;

    [SerializeField] private int _maxHealth = 100;



    protected virtual void Awake() {
        currentHealth = _maxHealth;
        currentRange = baseRange;

        OnDied += Die;
    }


    public void TakeDamage(int damage) {
        damage -= (int)((damage * armor.GetValue()) / 100f);
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        //Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null) {
            OnHealthChanged(_maxHealth, currentHealth, damage);
        }

        if (currentHealth <= 0) {
            OnDied();
        }
    }


    public virtual void Die() {
        Debug.Log(transform.name + " died.");
        isDead = true;
    }


    public bool IsDead() {
        return isDead;
    }


    public float GetCurrentRange() { return currentRange; }
    public int MaxHealth { get { return _maxHealth; } }
    public int CurrentHealth { get { return currentHealth; } }

}
