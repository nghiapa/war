using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiScripts : FastSingleton<UiScripts>
{
    [SerializeField] Button atk;
    [SerializeField] Button def;
    [SerializeField] Button back;


    // Start is called before the first frame update
    void Start()
    {
        def.onClick.AddListener(DefState);

        back.onClick.AddListener(BackState);

        atk.onClick.AddListener(AtkState);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AtkState()
    {
        PlayingSceneController.instance.PlayerBlue.ChangeArmyState(UnitBehavior.atk);
    }
    void DefState()
    {
        PlayingSceneController.instance.PlayerBlue.ChangeArmyState(UnitBehavior.def);
    }
    void BackState()
    {
        PlayingSceneController.instance.PlayerBlue.ChangeArmyState(UnitBehavior.back);
    }
}
