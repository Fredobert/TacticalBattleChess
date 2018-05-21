using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubes : MonoBehaviour {
    public float dashtime;
    public float dashspeed;
    public float cd = 100;

    private float dx;
    private float dy;
    private float lookx;
    private float looky;

    private bool dash = false;
    private bool shoot = false;
    private float dcd;
    private float dtime;
    private GameObject prefab;

    // Use this for initialization
    void Start () {
        prefab = Resources.Load("fire") as GameObject;
        dashtime = 0.04f;
        dashspeed = 60f;
        cd = 0.8f;
        dcd = -0.1f;
	}
	// Update is called once per frame
	void Update () {
        if (dcd > 0)
        {
            dcd -= Time.deltaTime;
        }

        if ( !dash)
        {
            dx = 0f;
            dy = 0f;
            Setinput();
            
            transform.Translate(5f * dx * Time.deltaTime, 0f, 5f * dy * Time.deltaTime);
        }
        else
        {
            transform.Translate(dashspeed * dx * Time.deltaTime, 0f, dashspeed * dy * Time.deltaTime);
            dtime -= Time.deltaTime;
            if (dtime < 0f)
            {
                dash = false;
                dcd = cd;
            }
        }
        if (shoot)
        {
            GameObject s = Instantiate(prefab) as GameObject;
            fires z = (fires)s.GetComponent("fires");
            z.dx = lookx;
            z.dy = looky;
            s.transform.position = transform.position;
            shoot = false;
        }
    }



    public void Setinput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            dy += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dx += -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dy += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dx += 1f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (dcd < 0)
            {
                dash = true;
                dtime = dashtime;
                dcd = cd;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            looky = 1f;
            lookx = 0f;
            shoot = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            looky = -1f;
            lookx = 0f;
            shoot = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookx = -1f;
            looky = 0f;
            shoot = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lookx = 1f;
            looky = 0f;
            shoot = true;
        }

    }

}
