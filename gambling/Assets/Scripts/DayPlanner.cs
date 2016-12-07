using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayPlanner : MonoBehaviour {

    [System.Serializable]
    public struct DayData {
        public int money_spent;
        public int cards_bought;
    }

    public List<DayData> events = new List<DayData>();

	void Start () {
	    

	}


    void add_event (int spent, int num_cards) {
        DayData new_event = new DayData();
        new_event.money_spent = spent;
        new_event.cards_bought = num_cards;
    }
}
