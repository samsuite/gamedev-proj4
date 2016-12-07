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
    GameManager gm;

	void Start () {
        gm = GameObject.FindGameObjectWithTag("CORE").GetComponent<GameManager>();

        add_event(5, 1);
        add_event(10, 2);
        add_event(25, 4);
        add_event(50, 8);
        add_event(100, 10);
        add_event(200, 12);
        add_event(500, 15);

        play_event();
	}

    void Update () {
        if (GameManager.cards_left <= 0){
            play_event();
        }
    }


    void add_event (int spent, int num_cards) {
        DayData new_event = new DayData();
        new_event.money_spent = spent;
        new_event.cards_bought = num_cards;

        events.Add(new_event);
    }



    void play_event () {

        GameManager.player.transform.position = gm.spawn_loc;

        if (events.Count > GameManager.day_index) {
            gm.day_text.text = "DAY "+(GameManager.day_index+1);
            gm.buy_cards(events[GameManager.day_index].money_spent);
            GameManager.cards_left += events[GameManager.day_index].cards_bought;
            GameManager.day_index ++;
        }
        else {
            gm.day_text.text = "DAY "+(GameManager.day_index+1);
            gm.buy_cards(1000);
            GameManager.cards_left += 20;
        }
    }

}
