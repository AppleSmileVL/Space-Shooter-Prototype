using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class PowerUp : MonoBehaviour, IDependency<Player>
{
    private Player player;

    public void Construct(Player playerInstance) => player = playerInstance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceShip ship = collision.GetComponent<SpaceShip>();

        if (ship != null && player != null && player.ActiveShip != null)
        {
            OnPickedUp(ship);
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickedUp(SpaceShip ship);
}
