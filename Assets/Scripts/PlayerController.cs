using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private BoxCollider2D m_playerCollider;
    [SerializeField] float initialYoffset, initialYsize, crouchYoffset, crouchYsize;
    [SerializeField] float initialXoffset, initialXsize, crouchXoffset, crouchXsize;
    [SerializeField] float speed;
    private Rigidbody2D m_rigbod2d;
    [SerializeField] float jump;
    [SerializeField] bool onGround;
    public Vector3 playerStartPosition;//public since using in PlayerDeathController script
    const int SCORE_ADD = 25;
    [SerializeField] ScoreController scoreController;

    public void KeyPicked()
    {
        //Player Pcked the key increase score
        Debug.Log("Reached KeyPicked function");
        scoreController.IncreaseScore(SCORE_ADD);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        m_playerCollider = GetComponent<BoxCollider2D>();
        m_rigbod2d = GetComponent<Rigidbody2D>();
        initialYoffset = m_playerCollider.offset.y;
        initialYsize = m_playerCollider.size.y;
        initialXoffset = m_playerCollider.offset.x;
        initialXsize = m_playerCollider.size.x;
        playerStartPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Player Speed is:"+horizontal);
        float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log("Player can jump : "+vertical);
        PlayerMovementAnimation(horizontal,vertical);
        MovePlayer(horizontal,vertical);
    }

    private void MovePlayer(float horizontal,float vertical)
    {
        //Move player horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        //Move player vertically
        if(vertical>0 && onGround)
        {
            onGround = false;
           // Debug.Log("Move vertically");
            m_rigbod2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
      //  Debug.Log("OnCollisionEnter2D" + collision.collider.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
        //  Debug.Log("OnCollisionExit2D");
    }

    private void PlayerMovementAnimation(float horizontal,float vertical)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
         if (horizontal < 0)
         {
             scale.x = -1f * Mathf.Abs(scale.x);
         }
         else if (horizontal > 0)
         {
             scale.x = Mathf.Abs(scale.x);
         }
        // scale.x = horizontal < 0 ? -1f * Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        //Not using since spriterenderer is used for display face direction of player  

        transform.localScale = scale;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouch", true);
            m_playerCollider.offset = new Vector2(crouchXoffset, crouchYoffset);
            m_playerCollider.size = new Vector2(crouchXsize, crouchYsize);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouch", false);
            m_playerCollider.offset = new Vector2(initialXoffset, initialYoffset);
            m_playerCollider.size = new Vector2(initialXsize, initialYsize);
        }

        /*if (vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }*/
        animator.SetBool("Jump",vertical>0);
    }
}
