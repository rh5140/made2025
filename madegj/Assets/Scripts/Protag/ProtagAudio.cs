using UnityEngine;

public class ProtagAudio : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip shootClip;
    [SerializeField]
    private AudioClip aimClip;
    [SerializeField]
    private AudioClip rollClip;
    [SerializeField]
    private AudioClip dieClip;
    public void PlayShootAudio()
    {
        audioSource.PlayOneShot(shootClip);
    }
    public void PlayAimAudio()
    {
        audioSource.PlayOneShot(aimClip);
    }
    public void PlayRollAudio()
    {
        audioSource.PlayOneShot(rollClip);
    }
    public void PlayDieAudio()
    {
        audioSource.PlayOneShot(dieClip);
    }
}
