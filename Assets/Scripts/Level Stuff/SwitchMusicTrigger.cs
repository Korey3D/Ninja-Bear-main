using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{
    public GameManagerScript GM;
    public AudioClip newTrack;

    private AudioManager theAM;

    // Start is called before the first frame update
    void Start()
    {
        theAM = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.gameOverUI.activeSelf)
        {
            if (newTrack is null)
             theAM.ChangeBGM(newTrack);
        }
    }

    
}
