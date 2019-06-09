

using UnityEngine;

public class Scaling : MonoBehaviour
{
    public bool isDead = false;
    private float scaleSpeed = -3;
    public Transform SpawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            isDead = true;
        }
    }
    void Update()
    {
        if (isDead)
        {
            if (transform.localScale.x >= 0)
            {
            transform.localScale += (new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime);

            }

        }
        if (transform.localScale.x <= 0)
        {
            this.transform.position = SpawnPoint.transform.position;
            transform.localScale = (new Vector3(4, 4, 4));
            isDead = false;
        }
    }

}