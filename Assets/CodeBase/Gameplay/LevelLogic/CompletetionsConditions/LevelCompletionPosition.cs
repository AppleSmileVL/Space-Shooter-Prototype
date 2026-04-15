using UnityEditor;
using UnityEngine;

public class LevelCompletionPosition : LevelCondition, IDependency<Player>
{
    [SerializeField] private float m_Radius;
    private Player player;

    public void Construct(Player playerInstance) => player = playerInstance;

    public override bool IsCompleted
    {
        get
        {
            if (player == null || player.ActiveShip == null) 
                return false;

            if (Vector3.Distance(player.ActiveShip.transform.position, transform.position) <= m_Radius)
            {
                return true;
            }

            return false;
        }
    }

#if UNITY_EDITOR
    private static Color GizmoColor = new Color(0f, 1f, 0f, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
    }
#endif
}
