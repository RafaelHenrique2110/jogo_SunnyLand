using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    [SerializeField] private int Vidas { get; set; }
    [SerializeField] public int Gem { get; set; }
    [SerializeField] private List<Text> texts;
    [SerializeField] private GameObject gameoverCanvas;
    [SerializeField] private GameObject victoryCanvas;
    void Awake()
    {
        instance = this;
    }

    public static GameController Instance()
    {
        return instance;
    }

    private void Start()
    {
        Vidas = 1;
        texts[0].text = Vidas + "X";
    }    

    public void Damage(int value)
    {
        Vidas -= value;
        texts[0].text = Vidas + "X";
        if(Vidas <= 0)
        {
            GameOver();
        }
    }

    public void AddVidas()
    {
        Vidas++;
        texts[0].text = Vidas + "X";
    }

    public void AddGem()
    {
        Gem= Gem +5;
        texts[1].text = Gem + "X";
    }

    public void UseGem()
    {
        Gem--;
        texts[1].text = Gem + "X";
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameoverCanvas.SetActive(true);
        
    }

    public void Victory()
    {
        Time.timeScale = 0;
        victoryCanvas.SetActive(true);

    }

}
