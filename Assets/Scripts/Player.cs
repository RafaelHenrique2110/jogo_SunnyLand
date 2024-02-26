using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StrategyGun Gun;
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float hurtForce;
    [SerializeField] private GameObject gem;
    private enum State {IDLE, RUN, JUMP, FALL, HURT}
    private State state = State.IDLE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        anim.SetInteger("state", (int)state);
        VelocityChange();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Atirar();
        }
    }
           
    void Atirar()
    {
        if(GameController.Instance().Gem> 0)
        {
            Gun?.Fire(transform);
            GameController.Instance().UseGem();
        }
           
        
    }
    public void Move()
    {
        if(state != State.HURT)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(speed * -1, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                //anim.SetBool("running", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                //anim.SetBool("running", true);
            }

            Pular();
        }
        
    }

    public void Pular()
    {
        if (Input.GetKey(KeyCode.Space) && col.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.JUMP;
        }
        
    }

    private void VelocityChange()
    {
        if(state == State.JUMP)
        {
            if(rb.velocity.y < 0.3f)
            {
                state = State.FALL;
            }
        }
        else if(state == State.FALL)
        {
            if (col.IsTouchingLayers(ground))
            {
                state = State.IDLE;
            }
        }
        else if(state == State.HURT)
        {
            if(rb.velocity.x < 0.1f)
            {
                state = State.IDLE;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.RUN;
        }
        else
        {
            state = State.IDLE;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gen2"))
        {
            if (other.transform.TryGetComponent<StrategyGun>(out StrategyGun g))
            {
                Gun = g;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Cherry"))
        {
            
            GameController.Instance().AddVidas();
            other.gameObject.GetComponent<Animator>().SetBool("collected", true);
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.CompareTag("DeathCollider"))
        {
            Destroy(this.gameObject);
            GameController.Instance().GameOver();
        }
        if (other.gameObject.CompareTag("Victory"))
        {
            GameController.Instance().Victory();
        }
        if (other.gameObject.CompareTag("Gem"))
        {
            if (other.transform.TryGetComponent<StrategyGun>(out StrategyGun g))
            {
                Gun = g;
            }
            GameController.Instance().AddGem();
            other.gameObject.GetComponent<Animator>().SetBool("collected", true);
            Destroy(other.gameObject, 0.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (state == State.FALL)
            {
                other.gameObject.GetComponent<Animator>().SetBool("death", true);
                Destroy(other.gameObject, 0.2f);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                Instantiate(gem, transform.position, transform.rotation);

            }
            
            else
            {
                state = State.HURT;
                if(other.gameObject.transform.position.x > gameObject.transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
                GameController.Instance().Damage(1);
            }
        }
    }
}