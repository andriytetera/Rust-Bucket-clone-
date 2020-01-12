using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IntarectableObject
{
    private UnitMotor motor;
    public AudioClip footStep;
    [SerializeField] private UnitAnimationController anim;
    [SerializeField] private AudioClip hit;
    //[SerializeField] private int maxHealth;
    public static PlayerManager instance;

    public override Vector3 NewPosition
    {
        get { return motor.NewPosition; }
    }
  

    protected override void Awake()
    {
        Debug.Log("PlayerManager");
        //base.Awake();
        instance = this;
        nameObject = "player";
        //LoadSaveSystem.instance.Defolt();
        health = LoadSaveSystem.instance.LoadHealth();
        maxHealth = LoadSaveSystem.instance.LoadMaxHealth();
        damage = LoadSaveSystem.instance.LoadDamage();

        motor = GetComponent<UnitMotor>();
        anim = GetComponent<UnitAnimationController>();
        //OnChangeHealth += healthSystem.UpdateHealthPointRander;
    }
    protected override void Start()
    {
        //OnChangeHealth += healthSystem.UpdateHealthPointRander;
    }
    public override bool Move(Vector2 direction)
    {
        if (motor.CheckCanMove(direction))
        {
            SoundManager.instance.PlaySoundSingle(footStep);
            anim.PlayWalkAnimation();
            return true;
        }
        return false;
    }
    public override bool TakeHit(int damage)
    { 
        SetHealth(-damage);
        SoundManager.instance.PlaySoundSingle(hit);
        if(Health <= 0)
        {
            LevelManager.instance.RestarLevel();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsHealthFull()
    {
        if (health == maxHealth)
            return true;
        return false;
    }
    
    // Use this for initialization
    //public bool moveUp()
    //{
    //    Debug.Log("c");
    //    if (motor.CheckCanMove(new Vector2(0, 0.5f)))
    //    {
    //        SoundManager.instance.PlaySoundSingle(footStep);
    //        anim.PlayWalkAnimation();
    //        return true;
    //    }
    //    return false;
    //}
    //public bool moveDown()
    //{
    //    if (motor.CheckCanMove(new Vector2(0, -0.5f)))
    //    {
    //        SoundManager.instance.PlaySoundSingle(footStep);
    //        anim.PlayWalkAnimation();
    //        return true;
    //    }
    //    return false;
    //}
    //public bool moveRight()
    //{
    //    if (motor.CheckCanMove(new Vector2(0.5f, 0)))
    //    {
    //        SoundManager.instance.PlaySoundSingle(footStep);
    //        anim.PlayWalkAnimation();
    //        return true;
    //    }
    //    return false;
    //}
    //public bool moveLeft()
    //{
    //    if (motor.CheckCanMove(new Vector2(-0.5f, 0)))
    //    {
    //        SoundManager.instance.PlaySoundSingle(footStep);
    //        anim.PlayWalkAnimation();
    //        return true;
    //    }
    //    return false;
    //}
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    moveUp();
        //}
        //else
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    moveRight();
        //}
        //else
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    moveDown();
        //}
        //else
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    moveLeft();
        //}
    }
    private void OnDestroy()
    {
        LoadSaveSystem.instance.SaveMaxHealth(maxHealth);
        LoadSaveSystem.instance.SaveHealth(health);
        LoadSaveSystem.instance.SaveDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    //    if(collision.tag == "vasa")
    //    {
    //        SoundManager.instance.PlaySoundSingle(collision.GetComponent<Vasa>().breakAudioClip);
    //        anim.PlayAttackAnimation();
    //        Destroy(collision.gameObject);
    //    }if (collision.tag == "enemy")
    //    {

    //    }
        //if(collision.tag == "item")
        //{
        //    collision.GetComponent<Item>().AddItem();
        //}

    }
}
