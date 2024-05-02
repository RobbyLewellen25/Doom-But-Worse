using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public bool isBullet;
    public float lifeTime;
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    private Vector3 start;
    private Vector3 end;


    // Start is called before the first frame update
    void Start()
    {


        if (isBullet)
        {
            startTime = Time.time;
            start = transform.position;
            end = transform.position + new Vector3(0, Random.Range(1.0f, 3.0f), 0);
            journeyLength = Vector3.Distance(start, end);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isBullet)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            Vector3 nextPosition = Vector3.Lerp(start, end, fractionOfJourney);
        }

        Destroy(gameObject, lifeTime);
    }
}