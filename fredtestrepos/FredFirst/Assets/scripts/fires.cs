using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fires : MonoBehaviour {
    public float dtime = 2f;
    public float speed = 10f;
    public float dx = 0f;
    public float dy = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed * dx * Time.deltaTime, 0f, speed * dy * Time.deltaTime);
        dtime -= Time.deltaTime;
        if (dtime < 0)
        {
            Destroy(gameObject);
        }
    }
}
