using UnityEngine;
public class SlimeController : MonoBehaviour
{
    private Rigidbody2D slimeRb;

    [SerializeField]
    private float speed;

    public Animator slimeAnim;

    private int health;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float radius;

    public Transform collisionDetector;

    public LayerMask layer;

    public float Speed { get => speed; set => speed = value; }

    public float Radius { get => radius; set => radius = value; }
    public int Health { get => health; set => health = value; }

    internal void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        Health = 3;
       
    }

    internal void Update()
    {
        CollisionDetector();
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        slimeRb.velocity = new Vector2(Speed, slimeRb.velocity.y);
    }

    public void CollisionDetector()
    {
        Collider2D hit = Physics2D.OverlapCircle(collisionDetector.position, Radius, layer);
        if (hit)
        {
            Debug.Log("bateu");
            speed = -speed;
            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (transform.eulerAngles.y == 180)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    
    public void OnHit()
    {
        slimeAnim.SetTrigger("slimeHit");
        LifeController();
    }

    public void LifeController()
    {
        Health--;
        if (Health <= 0)
        {
            slimeAnim.SetTrigger("slimeDeath");
            Speed = 0;
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(collisionDetector.position, radius);
    }
}
