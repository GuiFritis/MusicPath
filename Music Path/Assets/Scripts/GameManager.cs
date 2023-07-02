using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Padrao.Utils;

[DefaultExecutionOrder(-2)]
public class GameManager : Singleton<GameManager>
{
    public List<SO_Tone> tones;
    public string keyPrefTone = "Tone";
    private GameOverScreen _gameOverScript;
    private Ship _playerShip;
    
    private int _tone = 0;

    protected override void Awake() 
    {
        base.Awake();
        _tone = PlayerPrefs.GetInt(keyPrefTone, 0);
    }

    public void SetGameOverScreen(GameOverScreen gameOverScreen)
    {
        _gameOverScript = gameOverScreen;
    }

    public void SetPlayerShip(Ship ship)
    {
        _playerShip = ship;
        _playerShip.OnDie += GameOver;
    }

    public string GetToneName()
    {
        if(_tone == -1)
        {
            return "Rand";
        }
        return tones[_tone].name;
    }

    public SO_Tone GetTone()
    {
        if(_tone == -1)
        {
            return tones.GetRandom();
        }
        return tones[_tone];
    }

    public void IncreaseTone()
    {
        _tone++;
        if(_tone == tones.Count)
        {
            _tone = -1;
        }
        PlayerPrefs.SetInt(keyPrefTone, _tone);
    }

    public void GameOver()
    {
        ScreenController.Instance.HideAllScreens();
        ScreenController.Instance.ShowScreen(GameplayScreenType.GAME_OVER);
        TouchManager.Instance.enabled = false;
        MelodyManager.Instance.enabled = false;
        _gameOverScript?.GameOver(true);
    }
}
