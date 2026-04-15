using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public static string ignoreTag = "WorldBoundary"; 
    [SerializeField] private float m_VelocityDamageModifier;
    [SerializeField] private float m_DamageConstant;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == ignoreTag) return; 

        var destructible = transform.root.GetComponent<Destructible>();

        if (destructible != null)  
        {
            destructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));   
        }

    }
}
