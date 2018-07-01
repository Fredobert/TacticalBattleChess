using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int team;
    public int maxhealth = 10;
    public int health = 10;
    public int movment;
    public List<Ability> abilitys = new List<Ability>();
    //Unity preset

    public GameObject standingOn;
    public Material material;


    // Use this for initialization
    void Start()
    {
        GameObject field = GameObject.Find("World");
        GetComponent<MeshRenderer>().material = material;
    }

    public void Init()
    {
        Start();
        AbInit();
    }

    protected abstract void AbInit();
 

    public void DealDamage(int dmg)
    {
        health -= dmg;
        DamageTaken(dmg);
        if (health < 0)
        {
            Kill();
        }
    }
    public void Heal(int heal)
    {
        health += heal;

        if (health > maxhealth)
        {
            heal =heal- health - maxhealth;
            health = maxhealth;
        }
        OnHeal(heal);
    }


    //buggy null exception
    public void Kill()
    {
        standingOn.GetComponent<Tile>().character = null;
        standingOn.GetComponent<PFelement>().walkable = true;
        Destroy(gameObject);
    }


    //EVENTS
    void OnMouseDown()
    {
        EventManager.SelectCharacter(gameObject);
    }

    public delegate void DamageTakenAction(int dmg);
    public event DamageTakenAction OnDamageTaken;

    public void DamageTaken(int dmg)
    {
        if (OnDamageTaken != null)
        {
            OnDamageTaken(dmg);
        }
    }

    public delegate void HealAction(int dmg);
    public event HealAction OnHeal;

    public void HealChar(int heal)
    {
        if (OnDamageTaken != null)
        {
            OnHeal(heal);
        }
    }

}
