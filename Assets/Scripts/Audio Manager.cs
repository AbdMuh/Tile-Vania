using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager Instance;
  public  AudioClip[] musicSound, Effectsounds;
  public AudioSource musicSource, Effectsource;
  
  public void Awake()
  {
    if(Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }
  public void Start()
  {
    PlayMusic(0);
  }
  
  public void PlayMusic(int musicIndex)
  {
    musicSource.clip = musicSound[musicIndex];
    musicSource.Play();
  }
  public void PlayEffect(int effectIndex)
  {
    Effectsource.clip = Effectsounds[effectIndex];
    Effectsource.Play();
  }
  
}
