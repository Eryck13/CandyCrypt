///////////////////////////////////////////////////////////////////////////////////////////
// Checks to see if the player runs into a door, and moves the player and
// the camera accordingly. Has a fluent flow between rooms.
// Also uses playermovement speed, and finds what value we set there.
// **Attach To Player**
///////////////////////////////////////////////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCamera;

    private GameObject colObject;

    private const float sideCamera    = 5.99f;
    private const float upCamera      = 3.99f;

    private float sidePlayer    = 1.68f;
    private float upPlayer      = 1.47f;

    private Vector3 currPos;
    private Vector3 currObjPos;

    private Vector3 finalPos;
    private Vector3 finalObjPos;
    private Animator Animation;

    private float transitionSpeed;
    private float tempSpeed;

    private bool Up             = false;
    private bool Down           = false;
    private bool Left           = false;
    private bool Right          = false;

    public bool reset = false;
    private bool stairDown = false;

    public float speed;
    public bool isSlowed;

    public bool isAreaTransition = false;

    public LevelGeneration lg;

    public GameObject playerHitAnim;
    public GameObject FadeAway;
    public GameObject FadeIn;

    public BaseStats stats;
    bool CanBeHit = true;
    // Takes the speed from player movement script
    public void Awake()
    {
        playerHitAnim = GameObject.Find("HitRed");
        FadeAway = GameObject.Find("FadeAway");
        FadeIn = GameObject.Find("FadeIn");
        Animation = GetComponent<Animator>();
        playerHitAnim = GameObject.Find("HitRed");
        FadeAway = GameObject.Find("FadeAway");
        FadeIn = GameObject.Find("FadeIn");

        var player = GetComponent<PlayerMovement>();
        tempSpeed = player.speed;
        speed = tempSpeed;
        playerHitAnim.gameObject.SetActive(false);
        FadeAway.gameObject.SetActive(false);
        FadeIn.gameObject.SetActive(false);
        stats = this.GetComponent<BaseStats>();
        colObject = GameObject.Find("Collider_object");
    }

    public IEnumerator hurtTimer()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        playerHitAnim.gameObject.SetActive(false);
    }
    public IEnumerator Fade1()
    {
        yield return new WaitForSecondsRealtime(.95f);
        FadeAway.gameObject.SetActive(false);
        StartCoroutine(Fade2());
        FadeIn.gameObject.SetActive(true);
    }
    public IEnumerator Fade2()
    {
        yield return new WaitForSecondsRealtime(.95f);
        FadeIn.gameObject.SetActive(false);
    }

    // Checks with collision between different items.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Moves player down
        if (other.tag == "Door1")
        {
            this.gameObject.transform.position += new Vector3(0, -upPlayer, 0);
            Down = true;
            finalPos += new Vector3(0, -upCamera, 0);
        }
        // Moves player up
        else if (other.tag == "Door2")
        {
            this.gameObject.transform.position += new Vector3(0, upPlayer, 0);
            Up = true;
            finalPos += new Vector3(0, upCamera, 0);
        }
        // Moves player left
        else if (other.tag == "Door3")
        {
            this.gameObject.transform.position += new Vector3(-sidePlayer, 0, 0);
            Left = true;
            finalPos += new Vector3(-sideCamera, 0, 0);
        }
        // Moves player right
        else if (other.tag == "Door4")
        {
            this.gameObject.transform.position += new Vector3(sidePlayer, 0, 0);
            Right = true;
            finalPos += new Vector3(sideCamera, 0, 0);
        }
        // Resets level
        else if (other.tag == "StairDown")
        {
            stairDown = true;
            StartCoroutine(Fade1());
            FadeAway.gameObject.SetActive(true);
        }
        // Checks Collision with Enemy
        else if ((other.tag == "Enemy"||other.tag=="shot")&&CanBeHit)
        {
            Animation.SetTrigger("Hit");
            AudioManager.instance.Play("PlayerHurt");
            StartCoroutine(hurtTimer());
            playerHitAnim.gameObject.SetActive(true);

            //calls the coroutine function
            StartCoroutine(cooldown());
            
            //distance the player will travel
            var mag = 1250;
            //calculating the distance between both the player and enemy
            var force = transform.position - other.transform.position;
            //round up the force value
            force.Normalize();
            //pushes the player back
            gameObject.GetComponent<Rigidbody2D>().AddForce(force * mag);
            var healthUI = GetComponent<HealthUI>();
            //takes off a health value on the UI
            stats.TakeDamage(1);

            if (stats.GetTotalModdedHealth() <= 0)
            {
                //so we dont delete the player
                AudioManager.instance.Play("Die");
                this.transform.position = new Vector3(1000f, 1000f, 1000f);
                playerHitAnim.SetActive(false);
            }
        }
       
        else if(other.tag == "Mud")
        {
            if (!isSlowed)
            {
                isSlowed = true;          
            }
        }

    }


    //the coroutine to delay and allow the player to be invulnerable for x time to not take multiple damage on hit.
    IEnumerator cooldown()
    {
        CanBeHit = false;
        yield return new WaitForSeconds(0.25f);
        CanBeHit = true;
        yield return null;


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Mud")
        {
            if (isSlowed)
            {
                isSlowed = false;
            }
        }
    }
    
    void Update()
    {
        // If collision with stair
        if (stairDown)
        {
            // Shrinks player
            if (transform.localScale.x >= 0)
            {
                transform.localScale += (new Vector3(-1f, -1f, -1f) * Time.deltaTime);
                isAreaTransition = true;
            }
            // Resets everything
            if (transform.localScale.x <= 0)
            {
                this.gameObject.transform.position          = new Vector3(0, 0, -1.01f);
                MainCamera.gameObject.transform.position    = new Vector3(0, 0, -10);
                transform.localScale    = (new Vector3(1, 1, 1));
                finalPos                = new Vector3(0, 0, -1);
                reset                   = true;
                stairDown               = false;
                isAreaTransition        = false;
            }
            colObject.gameObject.transform.position = MainCamera.gameObject.transform.position;

        }

        currPos = MainCamera.gameObject.transform.localPosition;

        // Pass reset value from levelgeneration script
        if(lg.done)
        {
            reset   = lg.reset;
            lg.done = false;
        }

        // Camera movements (down)
        if (Down)
        {
            if (currPos.y > finalPos.y)
            {
                Time.timeScale = 0;
                currPos += new Vector3(0, -0.05f, 0);
                MainCamera.gameObject.transform.position    = currPos;
                isAreaTransition = true;
            }
            else
            {
                Time.timeScale = 1;
                Down = false;
                isAreaTransition    = false;
                currPos.y           = ((int)(currPos.y / upCamera) * -upCamera);
            }
            colObject.gameObject.transform.position = MainCamera.gameObject.transform.position;
        }
        // Camera movements (up)
        else if (Up)
        {
            if (currPos.y < finalPos.y)
            {
                Time.timeScale = 0;
                currPos += new Vector3(0, 0.05f, 0);
                MainCamera.gameObject.transform.position = currPos;
                colObject.gameObject.transform.position = currObjPos;
                isAreaTransition = true;
            }
            else
            {
                Time.timeScale = 1;
                Up = false;
                isAreaTransition = false;
                currPos.y = ((int)(currPos.y / upCamera) * upCamera);
            }
            colObject.gameObject.transform.position = MainCamera.gameObject.transform.position;
        }
        // Camera movements (left)
        else if (Left)
        {
            if (currPos.x > finalPos.x)
            {
                Time.timeScale = 0;
                currPos += new Vector3(-0.05f, 0, 0);
                MainCamera.gameObject.transform.position = currPos;
                isAreaTransition = true;
            }
            else
            {
                Time.timeScale = 1;
                Left = false;
                isAreaTransition = false;
                currPos.y = ((int)(currPos.y / sideCamera) * -sideCamera);
            }
            colObject.gameObject.transform.position = MainCamera.gameObject.transform.position;
        }
        // Camera movements (right)
        else if (Right)
        {
            if (currPos.x < finalPos.x)
            {
                Time.timeScale = 0;
                currPos += new Vector3(0.05f, 0, 0);
                MainCamera.gameObject.transform.position = currPos;
                isAreaTransition = true;
            }
            else
            {
                Time.timeScale = 1;
                Left = false;
                isAreaTransition = false;
                currPos.y = ((int)(currPos.y / sideCamera) * sideCamera);
            }
            colObject.gameObject.transform.position = MainCamera.gameObject.transform.position;
        }
    }
}
