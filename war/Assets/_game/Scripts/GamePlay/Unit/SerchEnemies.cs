using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerchEnemies : MonoBehaviour
{
    public Unit owner;

    public List<Unit> enemiesInRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Constant.UnitTag)
        {
            Unit unit = collision.GetComponent<Unit>();

            if (unit.team!=owner.team && enemiesInRange.Contains(unit)==false)
            {
                enemiesInRange.Add(unit);
            }
        }
    }

    public Unit GetNearlestUnitInRange(Vector3 pos, float range)
    {
        RemoveMissing();

        Unit nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Unit enemy in enemiesInRange)
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
    void RemoveMissing()
    {
        Unit valueToRemove = null;

        // Create a copy of the list to iterate through
        int index = 0;
        while (index < enemiesInRange.Count)
        {
            if (enemiesInRange[index] == valueToRemove)
            {
                // Remove the item from the original list
                enemiesInRange.RemoveAt(index);
            }
            else
            {
                // Move to the next item in the list
                index++;
            }
        }
    }
}
