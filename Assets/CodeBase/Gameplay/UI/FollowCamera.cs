using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    [SerializeField] private float m_InterpolationLinear;

    [SerializeField] private float m_InterpolationAngular;

    [SerializeField] private float m_CameraZOffset;

    [SerializeField] private float m_ForwardOffset;

    private void FixedUpdate()
    {
        if (m_Target == null) return;
        
        Vector2 camPos = transform.position;
        Vector2 targetPos = m_Target.position + m_Target.transform.up * m_ForwardOffset;
        Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, m_InterpolationLinear * Time.deltaTime);
        transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset);

        if (m_InterpolationAngular > 0)
        {
            // m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation, m_InterpolationAngular * Time.deltaTime);

            
            float angle = Mathf.Atan2(m_Target.up.y, m_Target.up.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, m_InterpolationAngular * Time.deltaTime);
            
        }
    }

    public void SetTarget(Transform newtarget)
    {
        m_Target = newtarget;
    }
}
