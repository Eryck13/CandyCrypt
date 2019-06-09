using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private Sprite[] color;

    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = color[Random.RandomRange(0,color.Length)];
    }

}
