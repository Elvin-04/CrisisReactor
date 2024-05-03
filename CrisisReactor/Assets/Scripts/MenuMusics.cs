using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MenuMusics : MonoBehaviour
{

    AudioSource mainMenuAudioSource;

    GameObject multiSceneManager;

    [Header("Audio")]
    [SerializeField] private List<AudioClip> musics; 

    void Start()
    {

        mainMenuAudioSource = GetComponent<AudioSource>();

        multiSceneManager = GameObject.Find("MultiSceneManager");

        if(multiSceneManager != null )
        {
            AudioSource[] sources = multiSceneManager.GetComponents<AudioSource>();
            foreach( AudioSource source in sources )
            {
                source.Stop();
            }
        }

        SetClip();
        
    }

    void Update()
    {
        if (!mainMenuAudioSource.isPlaying)
        {
            SetClip();
        }
    }

    public void SetClip()
    {
        int randomInt = Random.Range(0, musics.Count);

        if (mainMenuAudioSource.clip == null || mainMenuAudioSource.clip != musics[randomInt])
        {
            mainMenuAudioSource.clip = musics[randomInt];
            mainMenuAudioSource.Play();
        }
        else
        {
            SetClip();
        }
            
    }
}
