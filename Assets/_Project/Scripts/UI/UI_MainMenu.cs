using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [Header("UI Main Menu Settings")]
    [SerializeField] private GameObject _optionsMainMenu;

    public void StartClicked()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("Start Game");
    }

    public void OptionsClicked()
    {
        gameObject.SetActive(false);
        _optionsMainMenu.SetActive(true);
        Debug.Log("Open Options");
    }

    public void QuitClicked()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
