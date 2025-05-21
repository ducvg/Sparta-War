using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    void Awake()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        if (!animator) animator = GetComponent<Animator>();
    }

    private int blockAnimation = Animator.StringToHash("block");
    public void OggerEnter(Collider other)
    {
        audioSource.Play();
        animator.SetTrigger(blockAnimation);
    }

}