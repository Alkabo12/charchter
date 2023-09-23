using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Action : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(0);
         
    }
    public void StartGame2()
    {
        SceneManager.LoadScene(1);
         
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
