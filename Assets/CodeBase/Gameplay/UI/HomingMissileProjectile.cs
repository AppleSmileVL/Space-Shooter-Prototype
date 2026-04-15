using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissileProjectile : Projectile
{
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private string[] targetTags = new string[] { "CosmicBody" };

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 lastPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        target = FindNearestTargetByTags(targetTags);
        lastPosition = rb.position;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = - rotateAmount * rotateSpeed;

        rb.velocity = transform.up * Velocity;

        RaycastHit2D hit = Physics2D.Linecast(lastPosition, rb.position);
        if (hit.collider != null)
        {
            Debug.Log("Missile hit: " + hit.collider.name + " at " + hit.point);
            if (IsTargetTag(hit.collider.tag))
            {
                HandleHit(hit); 
                return;
            }
        }
        lastPosition = rb.position;
    }

    private bool IsTargetTag(string tag) 
    {
        foreach (var t in targetTags)
            if (t == tag) return true;
        return false;
    }

    private Transform FindNearestTargetByTags(string[] tags)
    {
        Transform nearest = null;
        float minDist = float.MaxValue;
        Vector2 currentPos = transform.position;

        foreach (string tag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                float dist = Vector2.Distance(currentPos, obj.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = obj.transform;
                }
            }
        }
        return nearest;
    }
}
