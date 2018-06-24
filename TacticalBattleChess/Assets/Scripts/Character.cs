using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {


    public int x;
    public int y;
    public int team;
    public int movment;
    public int health = 10;
    public GameObject standingOn;
    public Material material;
    public Ability ability1;
    public Ability ability2;


    // Use this for initialization
    void Start() {
        GameObject field = GameObject.Find("World");
        ability1 = GetComponent<Fireball>();
        ability2 = GetComponent<Thunder>();
        GetComponent<MeshRenderer>().material = material;
        movment = 3;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseDown()
    {
        print("selected a char");
        EventManager.SelectCharacter(gameObject);
    }

    public void DealDamage(int dmg)
    {
        health -= dmg;
        if (health < 0)
        {
            Kill();
        }
    }
    //buggy null exception
    public void Kill()
    {
        standingOn.GetComponent<Tile>().character = null;
        standingOn.GetComponent<PFelement>().walkable = true;
        Destroy(gameObject);
    }
    public void Init(){
        Start();
    }

}
