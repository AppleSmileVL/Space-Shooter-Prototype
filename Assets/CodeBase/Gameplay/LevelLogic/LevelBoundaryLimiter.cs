using UnityEngine;

/// <summary>
/// Ограничитель позиции, работающий в связке с LevelBoundary, данный скрипт кидается на объект который нужно ограничить в пределах уровня.
/// </summary>
public class LevelBoundaryLimiter : MonoBehaviour, IDependency<LevelBoundary>
{
    private LevelBoundary levelBoundary;
    public void Construct(LevelBoundary boundary) => levelBoundary = boundary;

    private void Update()
    {
        if (levelBoundary == null) return;

        float radius = levelBoundary.Radius;

        if (transform.position.magnitude > radius)
        {
            if (levelBoundary.LimitMode == LevelBoundary.Mode.Limit)
            {
                transform.position = transform.position.normalized * radius;
            }
            else if (levelBoundary.LimitMode == LevelBoundary.Mode.Teleport)
            {
                transform.position = -transform.position.normalized * radius;
            }
        }
    }
}
