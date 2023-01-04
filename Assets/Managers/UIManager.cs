using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]GameObject startP,ınGameP, gameOverP;
    public Image[] Skills = new Image[5];
    public void PanelController(GameManager.GAMESTATE currentPanel)
    {
        startP.SetActive(false);
        gameOverP.SetActive(false);
        ınGameP.SetActive(false);
        switch (currentPanel)
        {
            case GameManager.GAMESTATE.Start:
                startP.SetActive(true);
                break;
            case GameManager.GAMESTATE.Ingame:
                ınGameP.SetActive(true);
                break;
            case GameManager.GAMESTATE.GameOver:
                gameOverP.SetActive(true);
                break;
        }
    }
}
