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
    [SerializeField]
    private AudioClip reviveClip;
    [SerializeField]
    private AudioClip pickupClip;
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
    public void PlayReviveAudio()
    {
        audioSource.PlayOneShot(reviveClip);
    }
    public void PlayPickupAudio()
    {
        audioSource.PlayOneShot(pickupClip);
    }
}
