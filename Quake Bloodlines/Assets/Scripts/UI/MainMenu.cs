using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Launcher launcher;

    //when you press join match button it will join a match
    public void JoinMatch()
    {
        launcher.Join();
    }

    //when you press create match button it will create a match
    public void CreateMatch()
    {
        launcher.Create();
    }

    //when you press quit it will leave the game and g oback to windows
    public void Leave()
    {
        Application.Quit();
    }
}
