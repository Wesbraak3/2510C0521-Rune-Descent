using System.Collections;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, IDamageable, IHealable {

    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    [SerializeField] private bool _isDead = false;
    void Awake() {
        _currentHealth = _maxHealth;
    }

    public void OnHit<T>(int amount, T attacker) {
        Damage(amount);
        TextPopup.CreateTextPopup(transform.position, amount.ToString(), Color.red, new(0, 2, 0));
    }

    public void OnHit(int amount) {
    }

    public void OnHeal(int amount) {

        TextPopup.CreateTextPopup(transform.position, amount.ToString(), Color.green, new(0, 2, 0));
        Heal(amount);
    }

    protected virtual void Damage(int damage) {
        if (_isDead) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0) {
            _currentHealth = 0;
            Die();
        }
    }

    

    protected virtual void Heal(int amount) {
        if (_isDead) {
            return;
        }

        _currentHealth += amount;

        if (_currentHealth >= _maxHealth) {
            _currentHealth = _maxHealth;
        }
    }

    protected virtual void Die() {
        if (_currentHealth <= 0) {
            _isDead = true;
        }
    }
}
