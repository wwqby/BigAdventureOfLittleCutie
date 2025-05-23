using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rig;
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (!ReleaseBullet())
            {
                return;
            }
            LeanTween.cancel(gameObject);
            player.TakeDamage(damage);
        }
    }

    public void Shoot(Vector2 from, Vector2 direction, int damage)
    {
        this.damage = damage;
        transform.position = from;
        transform.up = direction;
        rig.velocity = direction * moveSpeed;
        LeanTween.delayedCall(gameObject, 2, () =>
        {
            ReleaseBullet();
        });

    }

    private bool ReleaseBullet()
    {
        if (!gameObject.activeSelf)
        {
            return false;//说明已经被其他碰撞销毁
        }
        EnemyBulletManager.pool.Release(this);
        return true;
    }

}
