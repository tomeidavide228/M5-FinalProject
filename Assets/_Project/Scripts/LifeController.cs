using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] private int _currentHP = 3;
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private Transform _spawnPoint;

    public event Action<int, int> OnHPChanged;
    public event Action OnDefeated;


    private void Start()
    {
        SetHP(_maxHP);
    }

    public int GetMaxHP() => _maxHP;
    public int GetHP() => _currentHP;

    public void SetHP(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHP);

        if (hp != _currentHP)
        {
            _currentHP = hp;
            OnHPChanged?.Invoke(_currentHP, _maxHP);

            if (_currentHP == 0)
            {
                OnDefeated?.Invoke();
            }
        }
    }

    public void AddHP(int amount) => SetHP(_currentHP + amount);

    public void TakeDamage(int damage)
    {
        AddHP(-damage);
        Respawn();
    }

    public void Respawn()
    {
        transform.position = _spawnPoint.position;
    }
}
