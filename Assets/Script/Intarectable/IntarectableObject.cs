using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class IntarectableObject : MonoBehaviour
{
    //[SerializeField] private Tilemap map;
    [SerializeField] protected int damage;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    public event Action<int> OnChangeHealth;
    public int Health
    {
        get { return health; }
        //set {
        //    if(OnChangeHealth != null)
        //        OnChangeHealth(value);
        //    health = value;
        //}
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int MaxHeath
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public void SetHealth(int addHealth)
    {
        health += addHealth;
        if (health > maxHealth)
            health = maxHealth;

        if (OnChangeHealth != null)
        {
            OnChangeHealth(health);
        }
    }
    [SerializeField] protected Vector3 newPosition;
    public string nameObject;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        Vector3 temp = NewPosition;
        LevelManager.instance.AddIntarectable(this);
    }
    public virtual Vector3 NewPosition
    {
        //get { return null; }
        get
        {
            newPosition = Position;
            return Position;
        }
    }
    public Vector3 Position
    {
        get
        {
            return gameObject.transform.position;
        }
    }
    public virtual bool TakeHit(int damage)
    {
        //OnChangeHealth(damage);

        SetHealth(-damage);
        if(health <= 0)
        {
            LevelManager.instance.DeleteIntarectale(this);
            return true;
        }
        else
        {
            return false;
        }
        //SoundManager.instance.PlaySoundSingle(hitAudioClip);
        //Destroy(gameObject);

    }
    public virtual bool Move(Vector2 direction)
    {
        return false;
    }
    public virtual int Attack()
    {
        //Debug.Log(" public virtual int Attack()" + damage);
        return damage;
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
