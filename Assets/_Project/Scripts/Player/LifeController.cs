using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] private int _currentHP = 3;
    [SerializeField] private int _maxHP = 3;
    [SerializeField] private Transform _spawnPoint;

    [Header("Events Settings")]
    [SerializeField] private UnityEvent OnDefeated;
    public event Action<int, int> OnHPChanged;

    private NavMeshAgent _agent;

    private void Start()
    {
        SetHP(_maxHP);
        _agent = GetComponent<NavMeshAgent>();
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
        _agent.isStopped = true;

        _agent.ResetPath();

        _agent.Warp(_spawnPoint.position);
        transform.rotation = _spawnPoint.rotation;

        _agent.isStopped = false;
    }
}
