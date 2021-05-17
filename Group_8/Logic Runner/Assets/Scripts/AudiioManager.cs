using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudiioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        PlaySound("MainTheme");
        
    }

    public void PlaySound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.Play();
            }
        }
    }
    public void PauseSound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.Pause();
            }
        }
    }
    public void StopSound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.Stop();
            }
        }
    }
    public void UnPauseSound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.UnPause();
            }
        }
    }

}
