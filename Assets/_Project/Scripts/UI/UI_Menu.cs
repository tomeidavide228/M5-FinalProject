using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Menu : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _choiceMenu;

    public void ContinueClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        MenuState.MenuClosed();
        Debug.Log("Continue Game");
    }

    public void RestartClicked()
    {

        Time.timeScale = 1f;
        MenuState.MenuClosed();
        Debug.Log("Restart Game");
        SceneManager.LoadScene("Level1");
    }

    public void ReturnClicked()
    {
        _choiceMenu.SetActive(true);
        Debug.Log("Open Choice");
    }

    public void OptionsClicked()
    {
        MenuState.OptionOpen();
        gameObject.SetActive(false);
        _optionsMenu.SetActive(true);
        Debug.Log("Open Options");
    }

}
