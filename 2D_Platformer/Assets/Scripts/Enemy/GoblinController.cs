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
    float _lookDir;
    [SerializeField]
    float _stopDistance;
    bool _inFront;
    bool _isRight;
    Vector2 _direction;
    //bool aux;
    GameObject player;


    public Transform raycastShooter;


    public float Speed { get => _speed; set => _speed = value; }
    public float VisionRange { get => _visionRange; set => _visionRange = value; }
    public bool InFront { get => _inFront; set => _inFront = value; }
    public bool IsRight { get => _isRight; set => _isRight = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float LookDir { get => _lookDir; set => _lookDir = value; }
    public float StopDistance { get => _stopDistance; set => _stopDistance = value; }

    // Start is called before the first frame update
    void Start()
    {
        _isRight = true;
        _goblinRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //FindCollision();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(raycastShooter.position, Direction * 0.2f, Color.red);

    }

    void FixedUpdate()
    {
        LookingDirection();
        FindCollision();
    }

    void LookingDirection()
    {
        if (IsRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            Direction = Vector2.right;
            LookDir = 1;
            
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            Direction = Vector2.left;
            LookDir = -1f;
        }
        Run(Speed * LookDir);
    }

  

    bool FindCollision()
    {
        bool aux = false;
        
        RaycastHit2D lfPlayer = Physics2D.Raycast(raycastShooter.position, Direction, VisionRange);
        RaycastHit2D lfWalls = Physics2D.Raycast(raycastShooter.position, Direction, 0.2f);
        RaycastHit2D lfFloor = Physics2D.Raycast(raycastShooter.position, Vector2.down, 0.5f);


        if (lfPlayer.collider)
        {
            if (lfPlayer.collider.CompareTag("Player")) //Looking for the player
            {
                aux = true;
                Speed = 2.5f; //Increase the speed is the player is visible
                if (DistanceCheck(lfPlayer))
                {
                    _goblinRb.velocity =  Vector2.zero;
                    Atack();
                }
                else
                {
                    //_goblinRb.velocity = Vector2
                }
            }
            else
            {
                aux = false;
                Speed = 2f;
            }
        }

        void Atack()
        {
            player.GetComponent<PlayerCombat>().OnHit();
        }
        //Looking for walls but not changing speed unless the player is visible
        if (lfWalls.collider && lfWalls.collider.CompareTag("Ground"))
        {
            LookingDirection();
            IsRight = !IsRight;
        }  
        //Looking for the edge of floaing platforms
        if (!lfFloor.collider)
        {
            LookingDirection();
            IsRight = !IsRight;
        }
        
        return aux;        
    }

    bool DistanceCheck(RaycastHit2D playerRaycast)
    {
        var check = false;
        float distance = Vector2.Distance(transform.position, playerRaycast.transform.position);
        if(distance <= StopDistance)
        {
            check = true;
            
        }
        else
        {
            check = false;
        }
        return check;
    }

    void Run(float speed)
    {
        _goblinRb.velocity = new Vector2(speed, _goblinRb.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(raycastShooter.position, Direction * VisionRange);
        Gizmos.DrawRay(raycastShooter.position, Vector2.down * 0.5f);
    }
}
