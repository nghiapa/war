using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSceneController : FastSingleton<PlayingSceneController>
{
    // Start is called before the first frame update

    public PlayerController PlayerBlue;
    public PlayerController PlayerRed;


    protected override void Awake()
    {
        base.Awake();

        PlayerController[] allPlayer = FindObjectsOfType<PlayerController>();

        Debug.Log(allPlayer.Length);

        allPlayer[0].team = Team.blue;
        allPlayer[1].team = Team.red;

        PlayerBlue = allPlayer[0];
        PlayerRed = allPlayer[1];
    }

}
