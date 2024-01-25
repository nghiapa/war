using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUnitBtnSingle : MonoBehaviour
{
    [SerializeField] Button button;

    [SerializeField] UnitTypes unitTypes;

    [SerializeField] Unit unit;

    [SerializeField] int cost;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClicked);
    }

    // Update is called once per frame
    void OnBtnClicked()
    {
        PlayingSceneController.instance.PlayerBlue.BuyUnit(unitTypes,unit,cost);        
    }
}
