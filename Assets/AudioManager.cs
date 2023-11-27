using UnityEngine;

public class AudioManager : MonoBehaviour
{
 
 [SerializeField] AudioSource sfxSource;


 public AudioClip buttonClick;  


 private void click(AudioClip clip)
 {
        sfxSource.clip = clip;
        sfxSource.Play();
 }
}
