using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMoveOneStep : IntarectableObject
{
    private UnitMotor motor;
    private HealthSystem healthSystem;
    // Start is called before the first frame update
    public override Vector3 NewPosition
    {
        get { return motor.NewPosition; }
    }
    protected override void Start()
    {

        nameObject = "enemy";
        base.Start();
        damage = 1;

        healthSystem.CreateHealthBar(health, maxHealth);
    }
    protected override void Awake()
    {
        motor = GetComponent<UnitMotor>();
        healthSystem = GetComponent<HealthSystem>();
        OnChangeHealth += healthSystem.UpdateHealthPointRander;
        base.Awake();
    }
    public override bool Move(Vector2 direction)
    {
        if (direction.x == 0 && direction.y == 0)
            return false;

        motor.CheckCanMove(direction);
        return true;
        
    }
    public override int Attack()
    {

        return base.Attack();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
