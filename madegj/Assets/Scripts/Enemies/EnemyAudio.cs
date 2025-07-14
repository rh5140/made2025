using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip hitClip;

    public void PlayHitAudio()
    {
        audioSource.PlayOneShot(hitClip);
    }
    
}
