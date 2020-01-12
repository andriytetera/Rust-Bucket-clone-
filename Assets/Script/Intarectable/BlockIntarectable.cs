using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIntarectable : IntarectableObject
{
    [SerializeField] private AudioClip unlock;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        nameObject = "block";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override int Attack()
    {
        SoundManager.instance.PlaySoundSingle(unlock);
        GetComponent<Animator>().Play("opendoor");
        return 0;
    }
    public override bool TakeHit(int damage)
    {
        return false;
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}