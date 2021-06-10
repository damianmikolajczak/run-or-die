using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string gameScene="Level1";
    private string mainMenuScene="MainMenu";
    private string aboutMenuScene="AboutMenu";

    //Ponizsze metody obsługują wciśnięcia przycisków w menu głównym i w pozostałych pod-menu. Na podstawie wybranej metody jest uruchamiana odpowiednia scena. Dodatkowo mozna wyłączyć grę.
    public void PlayGame() {
        SceneManager.LoadScene(gameScene);
    }

    public void AboutTheGame() {
        SceneManager.LoadScene(aboutMenuScene);
    }

    public void QuitTheGame() {
        Application.Quit();
    }

    public void ReturnToMain() {
        SceneManager.LoadScene(mainMenuScene);
    }
}
