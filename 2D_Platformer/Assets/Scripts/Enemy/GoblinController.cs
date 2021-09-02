using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{

    Rigidbody2D _goblinRb;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _visionRange;
    bool _inFront;
    [SerializeField]
    bool _isRight;
    Vector2 _direction;

    
    public Transform raycastShooter;


    public float Speed { get => _speed; set => _speed = value; }
    public float VisionRange { get => _visionRange; set => _visionRange = value; }
    public bool InFront { get => _inFront; set => _inFront = value; }
    public bool IsRight { get => _isRight; set => _isRight = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }

    // Start is called before the first frame update
    void Start()
    {
        _isRight = true;
        _goblinRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        FindPlayer();
        Mover(Speed);
        ChooseDirection();
        
    }

    void ChooseDirection()
    {
        if (FindPlayer())
        {
            Speed = 2f;
            if (IsRight) //Se estiver indo pra direita, vira pra esquerda
            {
                transform.eulerAngles = new Vector2(0, 0);
                Direction = Vector2.right;
                Mover(Speed);

            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                Direction = Vector2.left;
                Mover(Speed * -1);
            }
        }
        else
        {
            Speed = 0f;
        }

    }

    void Mover(float speed)
    {
        _goblinRb.velocity = new Vector2(speed, _goblinRb.velocity.y);
    }
    bool FindPlayer()
    {
        bool aux = false;
        //O raycast requer origen, direção e tamanho do raio.
        RaycastHit2D hitRaycast = Physics2D.Raycast(raycastShooter.position, Direction, VisionRange);
        

        //Se o raio está encontrando algo com colisor
        if (hitRaycast.collider)
        {
            if (hitRaycast.transform.CompareTag("Player")) 
            {
                aux = true;
                Debug.Log($"O raio encontrou o {hitRaycast.collider.name}");
            }
            else
            {
                aux = false;
            }
            
        }
        return aux;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(raycastShooter.position, Direction*VisionRange);
    }
}
