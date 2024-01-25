using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeturalCreep : Unit
{
    public NeuturalCamp myCamp;

    public UnitState currentState;
    public UnitBehavior currentBehavior;

    [SerializeField] Transform target;
    [SerializeField] protected Unit currentEnemy;
    [SerializeField] private int level;

    [SerializeField] float atkTimer;
    [SerializeField] float atkRange;

    // Start is called before the first frame update


    // Update is called once per frame
    protected virtual void Update()
    {
        SetBehavior(myCamp.campCurrentBehavior);
        HandleUnitAction();
    }
    void SetBehavior(UnitBehavior unitBehavior)
    {
        if (this.currentBehavior != unitBehavior)
        {
            currentBehavior = unitBehavior;
        }
    }
    void HandleUnitAction()
    {
        if (currentBehavior == UnitBehavior.atk)
        {
            HandleAtkState();
        }
        if (currentBehavior == UnitBehavior.def)
        {
            HandleDefState();
        }
    }
    void HandleDefState()
    {

        if (target==null || myCamp.defPos.Contains(target)==false)
        {
            target = myCamp.GetAvailableDefPos();
        }
        Moving();

    }
    protected virtual void HandleAtkState() 
    {



        if (currentEnemy == null)
        {
            target = myCamp.GetNearlestUnitInRange(transform.position,99).transform;
            Moving();
            SetEnenmy(myCamp.GetNearlestUnitInRange(transform.position,atkRange));

        }
        if (currentEnemy != null)
        {
            Attacking();
        }


    }
    
    protected virtual void Idle()
    {
        currentState = UnitState.idle;
        if (animControl != null)
        {
            animControl.SetAnimIdle();
        }
    }
    protected virtual void Attacking()
    {
        currentState = UnitState.atk;

        if (animControl != null)
        {
            animControl.SetAnimAtk();
        }

        atkTimer += Time.deltaTime;
        if (atkTimer > 1)
        {
            DealDamage();
            atkTimer = 0;
        }
    }
    protected virtual void DealDamage()
    {
        if (currentEnemy != null)
        {
            currentEnemy.TakeDamage(atkDame);
            if (currentEnemy.isDead == true)
            {
                SetEnenmy(null);
            }
        }
    }
    protected virtual void Moving()
    {
        currentState = UnitState.run;
        if (target != null && Vector3.Distance(transform.position, target.position) > .2f)
        {
            // Calculate the direction from the current position to the target position
            Vector3 direction = target.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move towards the target with constant velocity
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("No target assigned!");
        }
        if (animControl != null)
        {
            animControl.SetAnimMove();
        }
    }


    public virtual void UpLevel()
    {

    }
    public void SetLevel(int level)
    {
        this.level = level;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constant.UnitTag)
        {
            if (collision.TryGetComponent<ActionUnit>(out ActionUnit unit))
            {
                if (unit.team != this.team)
                {
                    currentEnemy = unit;
                }
            }
        }


    }
    public virtual void SetEnenmy(Unit enemy)
    {
        this.currentEnemy = enemy;
    }
}
