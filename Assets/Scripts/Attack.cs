using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class Attack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackSpeed = 1f;
    public int maxReflections = 20;
    public float maxDistance = 100f;
    public LayerMask hitLayerMask;
    public UnityEvent<bool> attackEvent;
    [SerializeField] private PhysicsRaycaster physicsRaycaster;
    [SerializeField] private List<Vector3> targetList;

    private Queue<Vector3> targetQueue;
    private LineRenderer lineRenderer;
    private AudioSource audioSource;
    private bool isAttacking = false;

    void Awake()
    {
        if (!physicsRaycaster) physicsRaycaster = Camera.main.GetComponent<PhysicsRaycaster>();
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        CastLaser();
    }


    void CastLaser()
    {
        Vector3 origin = transform.position + new Vector3(0, 0.3f, 0); // avoid ground
        Vector3 direction = transform.forward;
        List<Vector3> points = new List<Vector3> { origin };
        targetList.Clear();
        bool bossHit = false;

        for (int i = 0; i < maxReflections; i++)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, hitLayerMask))
            {
                origin = hit.point;
                points.Add(hit.point);

                if (hit.collider.CompareTag("Boss"))
                {
                    targetList.Add(hit.point);
                    bossHit = true;
                    attackEvent?.Invoke(!isAttacking);
                    // UIManager.instance.ToggleAttackBtn(!isAttacking);
                    targetQueue = new Queue<Vector3>(targetList);

                    break;
                }
                else if (hit.collider.CompareTag("Shield"))
                {
                    targetList.Add(hit.point);
                    Debug.DrawRay(hit.point, hit.normal, Color.blue, 1f);
                    direction = Vector3.Reflect(direction, hit.normal).normalized;

                }
                else
                {
                    break;
                }
            }
            else
            {
                points.Add(origin + direction.normalized * maxDistance);
                break;
            }
        }


        if (!bossHit)
        {
            attackEvent?.Invoke(false);
            // UIManager.instance.ToggleAttackBtn(false);

        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void AttackBoss()
    {
        if (isAttacking) return;
        GameObject attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);

        Vector3[] targetArray = targetList.ToArray();

        attack.transform.DOPath(targetArray, attackSpeed, PathType.Linear, PathMode.Full3D)
        // .SetEase(Ease.Linear)
        .OnStart(() =>
        {
            audioSource.Play();
            physicsRaycaster.enabled = false;

            isAttacking = true;
            lineRenderer.enabled = false;
            attackEvent?.Invoke(false);
            // UIManager.instance.ToggleAttackBtn(false);
        })
        .OnWaypointChange((waypointIndex) =>
        {
            if (waypointIndex < targetList.Count)
            {
                attack.transform.LookAt(targetList[waypointIndex]);
            }
        })
        .OnComplete(() =>
        {
            physicsRaycaster.enabled = true;
            Destroy(attack);
            isAttacking = false;
            lineRenderer.enabled = true;
        });
    }
}
