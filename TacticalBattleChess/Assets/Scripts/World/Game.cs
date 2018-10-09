using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public HumanPlayer player1;
    public HumanPlayer player2;

    //prefabs
    public GameObject player1prefab;
    public GameObject player2prefab;
    public static World world;

    public int currentPlayer = 0;
    // Use this for initialization
    void Start () {
        //Quick and Dirty solution  Problem Gameobject/Script variables get cleared on Play
        world = GetComponent<World>();
        player1 = GameObject.Find("player1").GetComponent<HumanPlayer>();
        player2 = GameObject.Find("player2").GetComponent<HumanPlayer>();
        print("started");
    }

    public void GenerateGame()
    {
        if (player1 != null || player2 != null)
        {
            DestroyImmediate(player1.gameObject);
            DestroyImmediate(player2.gameObject);
        }
        //init player
        GameObject p1 = Instantiate(player1prefab);
        GameObject p2 = Instantiate(player2prefab);
        p1.transform.SetParent(transform);
        p2.transform.SetParent(transform);
        p1.name = "player1";
        p2.name = "player2";
        player1 = p1.GetComponent<HumanPlayer>();
        player2 = p2.GetComponent<HumanPlayer>();

        player1.teamid = 0;
        player2.teamid = 1;

        GetComponent<Field>().GenerateMap();
    }

    public HumanPlayer GetCurrentPlayer()
    {
        if (currentPlayer == 0)
        {
            return player1;
        }
        return player2;
    }


    public void FinishTurn()
    {
        EventManager.TurnEnd(currentPlayer);
        currentPlayer = (currentPlayer + 1) % 2;
        GetCurrentPlayer().TurnStart();
        EventManager.TurnStart(currentPlayer);

    }

}
