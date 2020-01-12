using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vasa : IntarectableObject
{
    public AudioClip breakAudioClip;
    [SerializeField] private ChanceDropSystem chance;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //audioSource = GetComponent<AudioSource>();
        chance = GetComponent<ChanceDropSystem>();
        nameObject = "vasa";
    }

    public override bool TakeHit(int damage)
    {
        SoundManager.instance.PlaySoundSingle(breakAudioClip);
        //Debug.Log(breakAudioClip);
        //audioSource.clip = breakAudioClip;
        //audioSource.Play();
        if (base.TakeHit(damage))
        {
            chance.CreateRandonObject();
            return true;
        }
        return false;
    }
}
