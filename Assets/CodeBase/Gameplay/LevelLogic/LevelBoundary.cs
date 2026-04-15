using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public enum Mode
    {
        Limit,
        Teleport
    }

    [SerializeField] private float m_Radius;
    [SerializeField] private Mode m_LimitMode;

    public float Radius => m_Radius;
    public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(Vector3.zero, m_Radius);
    }
#endif
}
