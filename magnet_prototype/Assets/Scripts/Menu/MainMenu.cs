using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame ()
    {
        AudioManager.instance.Play("botao_entra");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame () 
    {
        AudioManager.instance.Play("botao_sair");
        Application.Quit();
    }

    public void ToMainMenu()
	{
        AudioManager.instance.Play("botao_sair");
        SceneManager.LoadScene("MainMenu");
	}
}
