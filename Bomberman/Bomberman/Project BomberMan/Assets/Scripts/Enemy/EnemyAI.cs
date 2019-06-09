using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float Speed = 1;
    public float TurnSpeed = 2.0f;
    public float StoppingDIstance = 0.25f;

    private Transform target;       // player

    [SerializeField]
    private Transform source;       // enemy

    public bool followPlayer = false;
    public bool isIdleWalk = true;

    public float    walkTime = 2;
    public float    waitTIme = 0.25f;

    private float   walkCounter;
    private float   waitCounter;

    private int WalkDirection;
    private Rigidbody2D enemyRigid;
    Vector2 lastpos = new Vector2(0, 0);


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        waitCounter = waitTIme;
        walkCounter = walkTime;

        //WalkDirection = 1;
        ChooseDirection();
    }

    private void Update()
    {
        if (DetectEnemies.instance != null)
        {
            foreach (GameObject enemy in DetectEnemies.instance.currentEnemies)
            {
                if (enemy == this.transform.parent.gameObject)
                {
                    enemyRigid = enemy.GetComponent<Rigidbody2D>();
                    source = enemy.transform;

                    if (!followPlayer)
                    {
                        IdleWalking(enemyRigid);
                    }
                    else
                    {
                        Moving(source);
                    }
                }
            }
        }
    }


    private void Moving(Transform currentEnem)
    {

        float disBw = Vector3.Distance(currentEnem.position, target.position);

        if (disBw > StoppingDIstance && followPlayer)
        {

            MoveTowards(currentEnem, target.position);
        }

    }

    void MoveTowards(Transform newEnemPos, Vector3 newTarget)
    {
        newEnemPos.position = Vector3.MoveTowards(newEnemPos.position, newTarget, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            followPlayer = true;
        }
        else if (collision.GetComponent<Collider2D>().GetType() == typeof(BoxCollider2D))
        {
            
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            followPlayer = false;
        }
    }




    void IdleWalking(Rigidbody2D currEnemyRigid)
    {
        if (isIdleWalk)
        {
            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    currEnemyRigid.velocity = new Vector2(0, Speed);
                    break;

                case 1:
                    currEnemyRigid.velocity = new Vector2(Speed, 0);
                    break;

                case 2:
                    currEnemyRigid.velocity = new Vector2(0, -Speed);
                    break;

                case 3:
                    currEnemyRigid.velocity = new Vector2(-Speed, 0);
                    break;
            }

            if (walkCounter < 0)
            {
                isIdleWalk = false;
                waitCounter = waitTIme;
            }

        }
        else
        {
            waitCounter -= Time.deltaTime;

            currEnemyRigid.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {

        //WalkDirection = ((int)currentEnemy.position.x * (int)currentEnemy.position.y) % 4; 
        WalkDirection = Random.Range(0, 4);
        isIdleWalk = true;
        walkCounter = walkTime;
    }

}