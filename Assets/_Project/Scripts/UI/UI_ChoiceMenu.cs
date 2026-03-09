using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ChoiceMenu : MonoBehaviour
{
    public void YesClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        MenuState.MenuClosed();
        Debug.Log("Yes: Return to Main Menu");
    }

    public void NoClicked()
    {
        gameObject.SetActive(false);
        Debug.Log("No: Return to Menu");
    }
}
