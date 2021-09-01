using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform atackPoint;
    [SerializeField]
    private float atackRadius;
    private bool isAtacking;
    private bool isHitted;
    private int life;
    private PlayerMovement playerMovement;
    public AlertObserver animAlert;

    public LayerMask enemyLayer;

    /*__________GETTERS AND SETTERS__________*/
    public float AtackRadius { get => atackRadius; set => atackRadius = value; }
    public bool IsAtacking { get => isAtacking; set => isAtacking = value; }
    public bool IsHitted { get => isHitted; set => isHitted = value; }
    public int Life { get => life; set => life = value; }

    /*_______________________________________*/

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Life = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Atack();
    }

    void Atack()
    {
        if (Input.GetButtonDown("Fire1") && !IsAtacking && !IsHitted)
        {
            IsAtacking = true;
            playerMovement.playerSprite.GetComponent<Animator>().SetInteger("transition", 3);
            DamagedEnemy();
        }
        
        
    }


    void DamagedEnemy()
    {
        Collider2D espadaHit = Physics2D.OverlapCircle(atackPoint.position, AtackRadius);

        if (espadaHit)
        {
            var objeto = espadaHit.name;
            if (espadaHit.name == "SlimeEnemy") 
                espadaHit.GetComponent<SlimeController>().OnHit();
           
        }
    }

    public void OnHit()
    {
        isHitted = true;
        playerMovement.playerSprite.GetComponent<Animator>().SetTrigger("playerHit");
        LifeController();
    }

    void LifeController()
    {
        Life--;
        Debug.Log($"A vida do player: {Life}");
        if (Life <= 0)
        {
            playerMovement.playerSprite.GetComponent<Animator>().SetTrigger("playerDeath");
            Destroy(gameObject, 0.6f);
            //Tela de game over 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnHit();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atackPoint.position, AtackRadius);
    }
}
