using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedAi : MonoBehaviour
{

    [SerializeField] PlayerController playerRed;

    public List<ActionUnit> allUnit;

    [SerializeField] float behaviorTimer;
    [SerializeField] float actionTimer;
    [SerializeField] float timeEachAction;
    [SerializeField] float timeEachChangeBehavior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerRedAtion();
    }

    void HandlePlayerRedAtion()
    {
        HandleArmyBehavior();
        HandleBuyAction();
    }

    void HandleArmyBehavior()
    {
        behaviorTimer -= Time.deltaTime;
        if (behaviorTimer < 0)
        {
            behaviorTimer = timeEachChangeBehavior;
            ChangeState();
        }
    }
    void HandleBuyAction()
    {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0)
        {
            actionTimer = timeEachAction;
            BuyRandomUnit();
            timeEachAction = 0.9f * timeEachAction;
        }
    }
    void BuyRandomUnit()
    {
        ActionUnit unit = allUnit[Random.Range(0, allUnit.Count)];

        playerRed.BuyUnit(unit, 0);
    }
    void ChangeState()
    {
        UnitBehavior behavior = GetRandomEnumValue<UnitBehavior>();
        playerRed.ChangeArmyState(behavior);
        if(behavior==UnitBehavior.atk && playerRed.AllUnit.Count > 2)
        {
            behaviorTimer = 999;
        }
    }
    T GetRandomEnumValue<T>()
    {
        System.Array enumValues = System.Enum.GetValues(typeof(T));
        T randomValue = (T)enumValues.GetValue(Random.Range(0, enumValues.Length));
        return randomValue;
    }
}
