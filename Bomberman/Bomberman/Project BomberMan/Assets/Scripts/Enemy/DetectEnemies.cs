using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    public static DetectEnemies instance;

    public List<GameObject> currentEnemies = new List<GameObject>();


    // Singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // checks for the enemies with multiple collider or same containers
            if (!currentEnemies.Contains(collision.gameObject))
            {
                currentEnemies.Add(collision.gameObject);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (currentEnemies.Contains(collision.gameObject))
            {
                currentEnemies.Remove(collision.gameObject);
            }
        }
        
    }



}
