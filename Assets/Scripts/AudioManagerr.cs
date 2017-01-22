using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerr : MonoBehaviour
{

    [SerializeField]
    public AudioClip[] audioClipss;

    [SerializeField]
    private int audioSourceNumberr = 2;

    private AudioSource[] audioSourcess;

    private static AudioManagerr audioManager;

    void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        //initialize audio sources
        audioSourcess = new AudioSource[audioSourceNumberr];
        for (int i = 0; i < audioSourceNumberr; i++)
        {
            audioSourcess[i] = gameObject.AddComponent<AudioSource>();
        }
        //source 0 is music loop
        audioSourcess[0].loop = true;
        audioSourcess[0].volume = 0.2f;
        
        playAudioClip(1);

    }

    public static void playAudioClip(int clipNumber)
    {
        if (clipNumber == 1 || clipNumber == 0)
        {
            float playTime = audioManager.audioSourcess[0].time;
            audioManager.audioSourcess[0].Stop();

            audioManager.audioSourcess[0].clip = audioManager.audioClipss[clipNumber];
            audioManager.audioSourcess[0].time = playTime;
            audioManager.audioSourcess[0].Play();
        }
        else if (audioManager.audioClipss.Length > 0 && //audioClips isnt empty
            clipNumber < audioManager.audioClipss.Length && clipNumber >= 0)//clipNumber is within bounds
        {
            for (int i = 1; i < audioManager.audioSourceNumberr; i++)
            {
                if (!audioManager.audioSourcess[i].isPlaying)
                {
                    audioManager.audioSourcess[i].clip = audioManager.audioClipss[clipNumber];
                    if (clipNumber == 3)
                    {
                        audioManager.audioSourcess[i].volume = 0.3f;
                    }
                    else
                    {
                        audioManager.audioSourcess[i].volume = 1;
                    }
                    audioManager.audioSourcess[i].Play();
                    
                    break;
                }
            }
        }
    }

    string getAudioClipName(int clipNumber)
    {
        return audioClipss[clipNumber].name;
    }
}
