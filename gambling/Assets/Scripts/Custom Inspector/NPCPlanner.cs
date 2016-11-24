using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCPlanner : MonoBehaviour {

    public enum actionType {
        wait,
        walk,
        talk,
        respond
    };

    [System.Serializable]
    public struct NPCAction {
        public actionType type;
        public float duration;
        public string text;
        public Vector3 destination;
    }


    [HideInInspector]
    public List<NPCAction> itinerary = new List<NPCAction>();
    
    [HideInInspector]
    public int actionIndex = 0;

    [HideInInspector]
    public Vector3 startLocation;


    // add a new action to the list
    public NPCAction AddAction (actionType type) {
        NPCAction newAction = new NPCAction();


        newAction.type = type;
        newAction.duration = 0f;
        newAction.text = "";
        newAction.destination = new Vector3(0f,0f,0f);


        itinerary.Add(newAction);
        return newAction;
    }

    // remove an action from the list
    public void RemoveAction (int index) {
        itinerary.RemoveAt(index);
    }
}
