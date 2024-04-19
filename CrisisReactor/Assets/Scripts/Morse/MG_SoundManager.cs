using UnityEngine;


//just used to play morse sound
public class MG_SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundIndex)
    {
        audioSource.PlayOneShot(sounds[soundIndex]);
    }
}