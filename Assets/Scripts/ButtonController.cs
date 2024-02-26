using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public void Yes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void No()
    {
        Application.Quit();
    }

    public void ClosePainel(GameObject painel)
    {
        painel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Handler()
    {
        Configuration.Instance().Volume();
    }
}
