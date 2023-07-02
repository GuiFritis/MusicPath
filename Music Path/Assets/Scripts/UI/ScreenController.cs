using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

public enum GameplayScreenType
{
    PLAYER_HUD,
    MENU,
    GAME_OVER
}

public class ScreenController : Singleton<ScreenController>
{    
    public List<GameScreen> screens = new List<GameScreen>();

    public void ShowScreen(GameplayScreenType screenType, bool active = true)
    {
        screens.Find(i => i.screenType.Equals(screenType))?.screen.SetActive(active);
    }

    public void HideAllScreens()
    {
        foreach (var item in screens)
        {
            item.screen.SetActive(false);
        }
    }
}

[System.Serializable]
public class GameScreen
{
    public GameplayScreenType screenType;
    public GameObject screen;
}
