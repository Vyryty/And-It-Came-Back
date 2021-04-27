using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Functions to load the selected map from the Map Selection Menu
    public void PlayGameMap1 ()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayGameMap2 ()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayGameMap3()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayGameMap4()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayGameMap5()
    {
        SceneManager.LoadScene(5);
    }

    public void PlayGameMap6()
    {
        SceneManager.LoadScene(6);
    }


    // To quit the game from the main menu.
    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}