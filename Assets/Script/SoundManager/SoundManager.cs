using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource efxSound;
    [SerializeField] private AudioSource efxSound1;
    [SerializeField] private AudioSource efxSound2;

    [SerializeField] private AudioSource musicSoud;

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        efxSound = gameObject.AddComponent<AudioSource>();
        efxSound1 = gameObject.AddComponent<AudioSource>();
        efxSound2 = gameObject.AddComponent<AudioSource>();

        musicSoud = gameObject.AddComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void PlaySoundSingle(AudioClip clip)
    {
        
        if(efxSound.isPlaying)
        {
            OneSoundSingle(clip);
            return;
        }
        efxSound.clip = clip;
        efxSound.Play();
    }
    private void OneSoundSingle(AudioClip clip)
    {
        if (efxSound1.isPlaying)
        {
            TwoSoundSingle(clip);
            return;
        }
        efxSound1.clip = clip;
        efxSound1.Play();
    }
    private void TwoSoundSingle(AudioClip clip)
    {
        //if (efxSound2.isPlaying)
        //{
        //    PlaySoundSingle(clip);
        //    return;
        //}
        efxSound2.clip = clip;
        efxSound2.Play();
    }
}
