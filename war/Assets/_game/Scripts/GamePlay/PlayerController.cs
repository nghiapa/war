using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float foodCount;
    [SerializeField] private float goldCount;


    public Team team;

    public UnitBehavior ArmyCurrentState;

    public List<Unit> AllUnit;

    public List<Transform> defPos;

    public Transform backPos;

    public List<GoldMine> goldMines;

    public List<Food> foods;

    public Base myBase;

    public PlayerController enemyTeam;
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }
    void SetUp()
    {
        if(this == PlayingSceneController.instance.PlayerBlue)
        {
            enemyTeam = PlayingSceneController.instance.PlayerRed;
        }
        else
        {
            enemyTeam = PlayingSceneController.instance.PlayerBlue;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyUnit(UnitTypes types, Unit unit,int cost)
    {
        Unit u = Instantiate(unit);
        u.transform.SetParent(this.transform);

        u.transform.position = backPos.position;
        u.SetTeam(this,enemyTeam);
    }

    public void ChangeArmyState(UnitBehavior state)
    {
        ArmyCurrentState = state;
    }

    
    public Food GetAvailableFood()
    {
        return foods[Random.Range(0, foods.Count)];
    }
    public GoldMine GetAvailableGoldMine()
    {
        return goldMines[Random.Range(0, goldMines.Count)];
    }
    public Transform GetAvailableDefPos()
    {
        return defPos[Random.Range(0, defPos.Count)];
    }
    public void AddResource(int amt, ResourceType type)
    {
        if (type == ResourceType.gold)
        {
            goldCount += amt;
        }
        if (type == ResourceType.food)
        {
            foodCount += amt;
        }
    }
}
