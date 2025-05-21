using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] private UnityEvent<Boss> onDie;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem hitParticle;

    [SerializeField] private Collider bossCollider;
    [SerializeField] private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (!hitParticle)
        {
            hitParticle = GetComponentInChildren<ParticleSystem>();
        }
        if(!audioSource) audioSource = GetComponent<AudioSource>();
        GameManager.Instance.bosses.Add(this);
        onDie.AddListener(_ => GameManager.Instance.OnBossDie(this));
        bossCollider = GetComponent<Collider>();
    }

    private int dieAniamtion = Animator.StringToHash("Die");

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Boss");

        transform.rotation = other.transform.rotation;

        // Destroy(other.gameObject);
        bossCollider.enabled = false;
        
        audioSource.Play();
        hitParticle.Play();
        animator.SetTrigger(dieAniamtion);
        onDie?.Invoke(this);
    }

}
