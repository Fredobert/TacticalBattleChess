using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour {


    public List<CharUIElement> charUIs = new List<CharUIElement>();
    public GameObject t1anchor;
    public GameObject t0anchor;
    public GameObject marker;
    public Text turnCounter;
    public Text turnIndicator;
    public Text freeMoves;
    public Text apCounter;
    public Field field;
    public float offset = -5000f;
    public float markerOffset = 60f;
    public bool markActive = false;
    private int _turnCounter = 0;

    private void Start()
    {
        EventManager.OnTurnStart += TurnEnd;
        EventManager.OnAbilityAction += Action;
        EventManager.OnMoveAction += Action;
        EventManager.OnTurnStart += TurnStart;
        //EventManager.OnTurnStart += Ability;
        turnIndicator.text = "Player " + ((_turnCounter % 2) + 1);
        freeMoves.text = field.getCurrentPlayer().freeMove+ "";
        apCounter.text = field.getCurrentPlayer().ap+ "";
        print(field.getCurrentPlayer().ap);
    }

    public void Action()
    {
        freeMoves.text = field.getCurrentPlayer().freeMove + "";
        apCounter.text = field.getCurrentPlayer().ap + "";
     
    }
    public void TurnStart(int id)
    {
        Action();
    }

    public void TurnEnd(int id)
    {
        turnCounter.text = ++_turnCounter+ "";
        turnIndicator.text = "Player " + ((_turnCounter % 2) + 1);
    }

    public void AddUI(CharUIElement cue)
    {
        charUIs.Add(cue);
     
        RectTransform rect;
        int count;
        if (cue.character.team == 0)
        {
            rect = t0anchor.GetComponent<RectTransform>();
            count = t0anchor.transform.childCount;
            cue.transform.SetParent(t0anchor.transform);
        } else  {
            rect = t1anchor.GetComponent<RectTransform>();
            count = t1anchor.transform.childCount;
            cue.transform.SetParent(t1anchor.transform);
        }
        cue.GetComponent<RectTransform>().anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y -count * -offset);

    }

    public void Mark(Character character)
    {
        marker.SetActive(true);
        markActive = true;
        for (int i = 0; i < charUIs.Count; i++)
        {
            if (charUIs[i].character == character)
            {
                if (character.team == 0)
                {
                    marker.GetComponent<RectTransform>().position = new Vector2(charUIs[i].GetComponent<RectTransform>().position.x - markerOffset , charUIs[i].GetComponent<RectTransform>().position.y);
                    marker.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
                  
                }
                else
                {
                    marker.GetComponent<RectTransform>().position = new Vector2(charUIs[i].GetComponent<RectTransform>().position.x + markerOffset, charUIs[i].GetComponent<RectTransform>().position.y);
                    marker.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90);

                }
            }
        }
    }

    public void HideMarker()
    {
        marker.SetActive(false);
        markActive = false;
    }

    public void RemoveUI()
    {
        charUIs = new List<CharUIElement>();
        foreach (var item in charUIs)
        {
            DestroyImmediate(item);
        }
    }


}
