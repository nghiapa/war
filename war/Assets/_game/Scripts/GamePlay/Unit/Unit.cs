using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public int moveSpeed;
    [SerializeField] public int hp;
    [SerializeField] public int atkDame;
    [SerializeField] public int amor;

    [SerializeField] public Team team;
    [SerializeField] public PlayerController owner;
    [SerializeField] public PlayerController enemyTeam;


    public bool isDead;
    public AnimationController animControl;

    public void SetTeam(PlayerController owner, PlayerController enemyTeam)
    {
        this.enemyTeam = enemyTeam;
        if (owner == null)
        {
            team = Team.netural;
            return;
        }
        this.owner = owner;
        if (owner.team == Team.blue)
        {
            this.team = Team.blue;
        }
        else
        {
            this.team = Team.red;
        }
    }
    public virtual void TakeDamage(int atkDame)
    {
        hp -= atkDame - amor;
        if (hp < 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        isDead = true;
        if (animControl != null)
        {
            animControl.SetAnimDie();
        }
        Destroy(this.gameObject);
    }
}
