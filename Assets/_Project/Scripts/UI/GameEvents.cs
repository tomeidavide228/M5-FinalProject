using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [Header("Events Settings")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _youWin;

    public void GameOver()
    {
        SetEvent(_gameOver);
    }

    public void YouWin()
    {
        SetEvent(_youWin);
    }

    public void SetEvent(GameObject state)
    {
        state.SetActive(true);
    }
}