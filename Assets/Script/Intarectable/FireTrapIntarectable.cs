using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapIntarectable : IntarectableObject
{
    private Animator anim;
    [SerializeField] private IntarectableObject targetIntarectable;
    private enum FireTrapStat
    {
        sleep,
        prepare,
        danger,
    }
    [SerializeField]private FireTrapStat fireTrapStat;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        nameObject = "trap";
        anim = GetComponent<Animator>();
        //anim.Play("idil");
        fireTrapStat = FireTrapStat.sleep;
        damage = 0;

    }
    public override bool TakeHit(int damage)
    {
        //return base.TakeHit(damage);
        return false;
    }
    public override bool Move(Vector2 direction)
    {
        //Debug.Log(fireTrapStat);
        if(fireTrapStat == FireTrapStat.sleep)
        {
            //Debug.Log("anim.Play(idil);");
            anim.Play("idil");
            damage = 0;
            fireTrapStat = FireTrapStat.prepare;
        }else if(fireTrapStat == FireTrapStat.prepare)
        {
            //Debug.Log("anim.Play(beforDanger);");
            anim.Play("beforDanger");
            damage = 0;
            fireTrapStat = FireTrapStat.danger;
        }else if(fireTrapStat == FireTrapStat.danger)
        {
            //Debug.Log("FireTrapStat.danger");
            damage = 1;
            //Debug.Log("anim.Play(danger);");
            anim.Play("danger");
            
            fireTrapStat = FireTrapStat.sleep;
            

            
        }
        //return base.Move(direction);
        return true;
    }
    public void DefoultStat()
    {
        //if (targetIntarectable != null)
        //    targetIntarectable.TakeHit(damage);
        Move(new Vector2(0, 0));
    }
    public override int Attack()
    {
        Move(new Vector2(0, 0));
        return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetIntarectable = collision.GetComponent<IntarectableObject>();
        targetIntarectable.TakeHit(damage);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        targetIntarectable = null;
    }
}
