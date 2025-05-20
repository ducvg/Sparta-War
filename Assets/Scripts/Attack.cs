using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

[RequireComponent(typeof(LineRenderer))]
public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask reflectLayer;
    [SerializeField] private List<Transform> pathSequence = new();

    void Awake()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }


    }

    private void Update()
    {
        Vector2 origin = transform.position;
        DrawLaser(origin, Vector3.forward);
    }


    void DrawLaser(Vector2 origin, Vector2 direction)
    {
        Vector2 currentOrigin = origin;
        Vector2 currentDirection = direction;


        if (Physics.Raycast(currentOrigin, currentDirection, out RaycastHit hitInfo, 200f))
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitInfo.point);
            if (hitInfo.collider.CompareTag("Reflector"))
            {
                var reflection = Vector3.Reflect(currentDirection, hitInfo.normal);
                DrawLaser(currentOrigin, reflection);
            }
        }
        else
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentOrigin + currentDirection * 200f);
        }
        
    }

    private void SpawnAttack()
    {
        var spear = Instantiate(attackPrefab, transform.position, quaternion.identity);
    }


}
