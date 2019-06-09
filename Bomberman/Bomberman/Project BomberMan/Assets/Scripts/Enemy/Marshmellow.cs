using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marshmellow : MonoBehaviour
{
    public Transform Player;
    float delay = 0.1f;
    float range= 2.5f;
    float firenext= 0.1f;
    public GameObject shot;
    int numshots;
    bool shotActive = false;
    float firerate = 25.0f;
    //float distance;
    // bool shoot = false;
    bool INrange = false;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        //distance = Vector2.Distance(Player.transform.position , this.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        INrange = Vector3.Distance(Player.position, transform.position) < range;
        if (INrange)
        {
            // transform.LookAt(Player);

            StartCoroutine(shooting());
        }
        //transform.LookAt(Player);
        
        

    }


    IEnumerator shooting()
    {

        if (INrange && shotActive == false)
        {
            AudioManager.instance.Play("Shoot");
            shotActive = true;
            GameObject go = Instantiate(shot, transform.position, transform.rotation);
            Vector3 playerPos = Player.transform.position;
            Rigidbody2D shotrigid = go.GetComponent<Rigidbody2D>();
            go.transform.SetParent(this.transform);
            go.transform.localPosition = new Vector3(0, 0, -2);
            while (go.transform.position != playerPos)
            {
                yield return new WaitForSeconds(0.05f);
                go.transform.position = Vector3.MoveTowards(go.transform.position, playerPos, firerate * Time.deltaTime);
                if (go.transform.position == playerPos)
                {
                    Destroy(go);
                }

            }

            yield return new WaitForSeconds(firenext);
            shotActive = false;

        }
        yield return null;



    }


}

