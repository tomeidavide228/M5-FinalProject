using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    [Header("HP Bar Settings")]
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Image _fillableLifebar;

    private LifeController _lifeController;

    private void Start()
    {
        _lifeController = FindAnyObjectByType<LifeController>();
        _lifeController.OnHPChanged += UpdateGraphics;
    }
    private void OnDestroy()
    {
        _lifeController.OnHPChanged -= UpdateGraphics;
    }
    public void UpdateGraphics(int currentHP, int maxHP)
    {
        _hpText.SetText($"{currentHP}/{maxHP}");
        _fillableLifebar.fillAmount = (float)currentHP / maxHP;
    }
}