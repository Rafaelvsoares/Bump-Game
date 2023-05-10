using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float spinSpeed = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinObstacle();
    }

    void spinObstacle()
    {
        transform.Rotate(0,spinSpeed,0);
    }
}
