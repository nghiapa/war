using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit
{
    [SerializeField] ResourceType resourceType;

    [SerializeField] int speed;

    public UnitState currentState;
    public UnitBehavior currentBehavior;

    [SerializeField] Transform target;
    [SerializeField] protected Unit currentEnemy;
    [SerializeField] private int level;

    [SerializeField] float atkTimer;
    [SerializeField] float atkRange;
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        SetBehavior(owner.ArmyCurrentState);
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
        if (currentBehavior == UnitBehavior.back)
        {
            HandleBackState();
        }
    }
    protected virtual void HandleAtkState()
    {

        if (currentEnemy == null)
        {
            if (resourceType == ResourceType.food)
            {
                target = owner.GetAvailableFood().transform;

            }
            if (resourceType == ResourceType.gold)
            {
                target = owner.GetAvailableGoldMine().transform;
            }
            Moving();
        }
        if (currentEnemy != null)
        {
            Attacking();
        }


    }
    void HandleDefState()
    {

        if (currentEnemy == null)
        {
            if (resourceType == ResourceType.food)
            {
                target = owner.GetAvailableFood().transform;

            }
            if (resourceType == ResourceType.gold)
            {
                target = owner.GetAvailableGoldMine().transform;
            }
            Moving();
        }
        if (currentEnemy != null)
        {
            Attacking();
        }
    }
    void HandleBackState()
    {
        target = owner.backPos;
        currentEnemy = null;
        Moving();
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
            owner.AddResource(atkDame, resourceType);
        }
    }
    protected virtual void Moving()
    {
        currentState = UnitState.run;
        if (target != null && Vector3.Distance(transform.position,target.position)>.2f)
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
        if(collision.tag == Constant.Gold)
        {
            if (resourceType == ResourceType.gold)
            {
                currentEnemy = collision.GetComponent<GoldMine>();
            }
        }
        if (collision.tag == Constant.Food)
        {
            if (resourceType == ResourceType.food)
            {
                currentEnemy = collision.GetComponent<Food>();
            }
        }

    }
    public virtual void SetEnenmy(Unit enemy)
    {
        this.currentEnemy = enemy;
    }
}
