using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour
{
    public bool playEffect;
    public AudioSource asource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playEffect)
        {
            playEffect = false;
            PlayEff();
        }
    }

    public void PlayEff()
    {
        asource.PlayOneShot(clip);
    }
}
