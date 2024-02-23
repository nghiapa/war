using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ActionUnit : Unit
{
    

    public UnitState currentState;
    public UnitBehavior currentBehavior;
    [SerializeField] bool isMoveBack;

    [SerializeField] Transform target;
    [SerializeField] protected Unit currentEnemy;
    [SerializeField] private int level;

    [SerializeField] float atkTimer;
    [SerializeField] float atkRange;
    [SerializeField] int cost;

    [SerializeField] Transform startPos;
    [SerializeField] SerchEnemies serchArea;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        startPos = owner.GetAvailableDefPos();
    }

    
    // Update is called once per frame
    protected virtual void Update()
    {
        SetBehavior(owner.ArmyCurrentState);
        HandleUnitAction();
    }
    void SetBehavior(UnitBehavior unitBehavior)
    {
        if(this.currentBehavior != unitBehavior)
        {
            currentBehavior = unitBehavior;
        }
        if (unitBehavior == UnitBehavior.def)
        {
            isMoveBack = true;
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
            if (serchArea.enemiesInRange.Count > 0)
            {
                target = serchArea.GetNearlestUnitInRange(transform.position, atkRange).transform;

            }
            else
            {
                target = enemyTeam.myBase.transform;

            }

            Moving();
            SetEnenmy(serchArea.GetNearlestUnitInRange(transform.position, atkRange));

        }
        if (currentEnemy!=null)
        {
            Attacking();
        }


    }
    void HandleDefState()
    {

        if (isMoveBack)
        {
            target = startPos;
            Moving();
            return;
        }

        if (currentEnemy == null && target==null)
        {
            Idle();
        }
        if(target != null && currentEnemy == null)
        {
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
        if(atkTimer > 1)
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
        if (collision.tag == Constant.UnitTag && currentEnemy==null)
        {
            if (collision.TryGetComponent<ActionUnit>(out ActionUnit unit))
            {
                if (unit.team != this.team)
                {
                    currentEnemy = unit;
                }
            }
            if (collision.TryGetComponent<NeturalCreep>(out NeturalCreep neturalCreep))
            {
                if (neturalCreep.team != this.team)
                {
                    currentEnemy = neturalCreep;
                }
            }
        }

        if (collision.tag == Constant.BaseArea)
        {
            isMoveBack = false;
        }

    }
    public virtual void SetEnenmy(Unit enemy)
    {
        this.currentEnemy = enemy;
    }
}
