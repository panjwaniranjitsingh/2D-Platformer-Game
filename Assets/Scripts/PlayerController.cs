using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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
    [SerializeField] Vector3 playerStartPosition;
    [SerializeField] ScoreController scoreController;
    [SerializeField] GameOverController gameOverController;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private int noOfLives;
    [SerializeField] private GameObject PlayerHeart1, PlayerHeart2, PlayerHeart3;
    const int SCORE_ADD = 25;
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    const string GROUND = "Ground";
    const string SPEED = "Speed";
    const string ISCROUCH = "isCrouch";
    const string JUMP = "Jump";
    private bool m_playerAlive = true;

    public void KeyPicked()
    {
        //Player Pcked the key increase score
        Debug.Log("Reached KeyPicked function");
        scoreController.IncreaseScore(SCORE_ADD);
    }

    private void Awake()
    {
        m_playerCollider = GetComponent<BoxCollider2D>();
        m_rigbod2d = GetComponent<Rigidbody2D>();
        initialYoffset = m_playerCollider.offset.y;
        initialYsize = m_playerCollider.size.y;
        initialXoffset = m_playerCollider.offset.x;
        initialXsize = m_playerCollider.size.x;
        playerStartPosition = transform.position;
        LevelText.text = SceneManager.GetActiveScene().name + " Level";
        noOfLives = 3;
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw(HORIZONTAL);
        //Debug.Log("Player Speed is:"+horizontal);
        float vertical = Input.GetAxisRaw(VERTICAL);
        //Debug.Log("Player can jump : "+vertical);
        if (m_playerAlive)
        {
            PlayerHorizontalMovement(horizontal);
            PlayerJump(vertical);
            PlayerCrouch();
        }
        
    }

    private void PlayerHorizontalMovement(float horizontal)
    {
        animator.SetFloat(SPEED, Mathf.Abs(horizontal));
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
        //Move player horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
    }

    private void PlayerCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool(ISCROUCH, true);
            m_playerCollider.offset = new Vector2(crouchXoffset, crouchYoffset);
            m_playerCollider.size = new Vector2(crouchXsize, crouchYsize);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool(ISCROUCH, false);
            m_playerCollider.offset = new Vector2(initialXoffset, initialYoffset);
            m_playerCollider.size = new Vector2(initialXsize, initialYsize);
        }
    }

    private void PlayerJump(float vertical)
    {
        //Move player vertically
        if (vertical > 0 && onGround)
        {
            onGround = false;
            // Debug.Log("Move vertically");
            m_rigbod2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
        }
        /*if (vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }*/
        animator.SetBool(JUMP, vertical > 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND))
        {
            onGround = true;
        }
        //  Debug.Log("OnCollisionEnter2D" + collision.collider.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND))
        {
            onGround = false;
        }
        //  Debug.Log("OnCollisionExit2D");
    }

    public void LevelComplete()
    {
        // Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(2);
        if (SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(1);
    }

    public void PlayerDie()
    {
        noOfLives--;
        if (noOfLives == 2)
            Destroy(PlayerHeart1);
        if (noOfLives == 1)
            Destroy(PlayerHeart2);
        if (noOfLives <= 0)
        {
            Destroy(PlayerHeart3);
            m_playerAlive = false;
            animator.SetBool("PlayerDies", true);
            // LevelText.text = "Level "+SceneManager.GetActiveScene().name + " Restarted";

            //Restarting Level using LoadScene
            Invoke("CallPlayerDied", 3);
            //USING INVOKE FUNTION FOR 3 SEC DELAY   
            //Not using LoadScene as Restart Level cannot be shown on text
            //Restarting Level using transform
            // transform.position = playerStartPosition;
        }
    }
    private void CallPlayerDied()
    {
        gameOverController.PlayerDied();
        this.enabled = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NoPlatform()
    {
        noOfLives = 1;
        if(PlayerHeart1!=null)
            Destroy(PlayerHeart1);
        if (PlayerHeart2!=null)
            Destroy(PlayerHeart2);
    }

}

