using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] private UnityEvent<Boss> onDie;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem hitParticle;

    [SerializeField] private Collider bossCollider;

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

        GameManager.Instance.bosses.Add(this);
        bossCollider = GetComponent<Collider>();
    }

    private int dieAniamtion = Animator.StringToHash("Die");

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Boss");

        transform.rotation = other.transform.rotation;

        Destroy(other.gameObject);
        bossCollider.enabled = false;

        hitParticle.Play();
        animator.SetTrigger(dieAniamtion);
        onDie?.Invoke(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Boss");
        Destroy(collision.gameObject);
        bossCollider.enabled = false;

        hitParticle.Play();
        animator.SetTrigger(dieAniamtion);
        onDie?.Invoke(this);
    }
}
