using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuturalCamp : MonoBehaviour
{
    public List<Unit> enemiesOfCamp;
    public List<NeturalCreep> campCreep;

    public List<Transform> defPos;


    public UnitBehavior campCurrentBehavior;

    private void Update()
    {
        if (enemiesOfCamp.Count > 0)
        {
            campCurrentBehavior = UnitBehavior.atk;
        }
        else
        {
            campCurrentBehavior = UnitBehavior.def;

        }

        CheckCurrentUnitInCamp();
    }

    void CheckCurrentUnitInCamp()
    {
        int i = 0;
        foreach(NeturalCreep neturalCreep in campCreep)
        {
            if(neturalCreep!=null && neturalCreep.isDead == false)
            {
                i++;
            }
        }
        if (i == 0)
        {
            DeleteCamp();
        }
    }
    void DeleteCamp()
    {
        Destroy(this.gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constant.UnitTag)
        {
            if (collision.TryGetComponent<ActionUnit>(out ActionUnit unit))
            {
                if (unit.team != Team.netural)
                {
                    enemiesOfCamp.Add(unit);
                }
            }
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constant.UnitTag)
        {
            if (collision.TryGetComponent<ActionUnit>(out ActionUnit unit))
            {
                if (unit.team != Team.netural && enemiesOfCamp.Contains(unit))
                {
                    enemiesOfCamp.Remove(unit);
                }
            }
        }
    }
    public Unit GetNearlestUnitInRange(Vector3 pos, float range)
    {
        Unit nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;



        foreach (Unit enemy in enemiesOfCamp)
        {
            float distanceToEnemy = Vector3.Distance(pos, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (shortestDistance < range)
        {
            return nearestEnemy;
        }
        else
        {
            return null;
        }
    }
    public Transform GetAvailableDefPos()
    {
        return defPos[Random.Range(0, defPos.Count)];
    }
}
