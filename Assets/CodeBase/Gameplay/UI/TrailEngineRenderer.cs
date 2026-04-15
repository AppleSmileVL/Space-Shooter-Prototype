using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEngineRenderer : MonoBehaviour
{
    [SerializeField] private SpaceShip m_TargetShip;
    [SerializeField] private ParticleSystem m_TrailEffect;
    [SerializeField] private float m_MaxEmissionRate;

    private ParticleSystem.EmissionModule m_EmissionModule;

    private void Start() 
    {
        if (m_TrailEffect != null) 
        {
            m_EmissionModule = m_TrailEffect.emission;
        }
    }

    private void Update()
    {
        if(m_TargetShip == null || m_TrailEffect == null) return; // Проверяет, что и корабль, и эффект следа заданы.

        float emissionRate = Mathf.Abs(m_TargetShip.TrustControl) * m_MaxEmissionRate; // Вычисляет скорость эмиссии на основе TrustControl корабля.
        m_EmissionModule.rateOverTime = emissionRate; // Устанавливает скорость эмиссии частицы.

        if (m_TargetShip.TrustControl != 0 && !m_TrailEffect.isPlaying) // Если TrustControl не равен нулю и эффект не играет, запускает эффект.
        {
            m_TrailEffect.Play();
        }
        else if(m_TargetShip.TrustControl == 0 && m_TrailEffect.isPlaying) // Если TrustControl равен нулю и эффект играет, останавливает эффект.
        {
            m_TrailEffect.Stop();
        }
    }
}
