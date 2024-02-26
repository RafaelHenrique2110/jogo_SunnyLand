using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{
    private static Configuration instance;
    [SerializeField] private GameObject configPanel;
    [SerializeField] private AudioSource music;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        OpenPainel();
    }

    public static Configuration Instance()
    {
        return instance;
    }
    public void OpenPainel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            configPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Volume()
    {
        music.volume = configPanel.GetComponentInChildren<Scrollbar>().value;
    }
}
