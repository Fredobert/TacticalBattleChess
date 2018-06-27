using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour {


    List<CharUIElement> charUIs = new List<CharUIElement>();



    public void AddUI(CharUIElement cue)
    {
        charUIs.Add(cue);
    }



    // Use this for initialization
    void Start () {

        
	}
	


}
