using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    public AudioSource BgmSource;
    public AudioClip menuClip, ingameClip;

    public static BGM Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (BgmSource == null) BgmSource = GetComponent<AudioSource>();
    }

    public void PlayMenuBGM()
    {
        BgmSource.DOFade(0, fadeDuration).OnComplete(() =>
        {
            BgmSource.Stop();
            BgmSource.clip = menuClip;
            BgmSource.Play();
            BgmSource.DOFade(1, fadeDuration);
        });
    }

    public void PlayIngameBGM()
    {
        BgmSource.DOFade(0, fadeDuration).OnComplete(() =>
        {
            BgmSource.Stop();
            BgmSource.clip = ingameClip;
            BgmSource.Play();
            BgmSource.DOFade(1, fadeDuration);
        });
    }
}
