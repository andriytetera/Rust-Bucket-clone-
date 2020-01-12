using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMoveTwoStepEnemy : IntarectableObject
{
    [SerializeField] private UnitMotor motor;
    private UnitAnimationController anim;
    [SerializeField] private AudioClip hit;

    private bool canAttack;
    public bool CanAttack
    {
        get { return  canAttack; }
    }
    public override Vector3 NewPosition
    {
        get { return motor.NewPosition; }
    }
    //public OneMoveTwoStepEnemy()
    //{
    //    nameObject = "Bable";
    //}
    protected override void Start()
    {
        base.Start();
    }
    protected override void Awake()
    {
        base.Awake();
        nameObject = "enemy";
        motor = GetComponent<UnitMotor>();
        anim = GetComponent<UnitAnimationController>();
        canAttack = false;
    }
    public override bool TakeHit(int damage)
    {
        SoundManager.instance.PlaySoundSingle(hit);
        return base.TakeHit(damage);
    }
    public override bool Move(Vector2 direction)
    {
        if (direction.x == 0 && direction.y == 0)
            return false;
        if (canAttack)
            TurnMoveStat(direction);
        else
            TurnAttackStat();
        return true;
        //return base.Move(newPosiotion);
    }
    public bool TurnAttackStat()
    {
        anim.PlayAttackAnimation();
        canAttack = true;
        return true;
    }
    public bool TurnMoveStat(Vector2 direction)
    {
        anim.PlayWalkAnimation();
        motor.CheckCanMove(direction);
        canAttack = false;
        return true;
    }
    public override int Attack()
    {
        if(canAttack)
        {
            return 1;
        }else
        {
            return 0;
        }
    }
}
