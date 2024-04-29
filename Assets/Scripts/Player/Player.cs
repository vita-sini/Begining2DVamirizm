using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _damage;

    private float _healthMax = 10;

    private float _currentHealth;

    public float HealthMax => _healthMax;
    public float CurrentHealth => _currentHealth;

    public event UnityAction<float, float> HealthChanged;

    private void Awake()
    {
        _currentHealth = _healthMax;
    }

    public void Healing(float health)
    {
        HealthChanged?.Invoke(_currentHealth, _healthMax);

        if (_currentHealth < _healthMax)
            _currentHealth += health;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _healthMax);
    }
}
