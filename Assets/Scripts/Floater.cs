using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float amplitude = 0.15f;
    public float frequency = 1f;
    // Start is called before the first frame update

    private Vector3 offset = new Vector3 ();
    private Vector3 tempPos = new Vector3 ();
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = offset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }
}
