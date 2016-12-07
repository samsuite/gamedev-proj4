using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // a class to store globally accessible variables (yeah, it's a singleton. don't judge me)

    public static GameObject player;
    public static GameObject camera;
    public static int money = 0;
    public static int day = 0;
    public static bool can_walk = true;

    static GameObject current_card;
    static int prize_money;

    bool made = false;

    public GameObject card_inp;
    public static GameObject card;

	void Awake () {
	    player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main.gameObject;
        card = card_inp;
	}

    void LateUpdate () {
        if (!made){
            scratch_card(10, 6);
            made = true;
        }
    }

    public void scratch_card (int price, int value) {
        prize_money = value;
        money -= price;
        
        current_card = Instantiate(card);
        current_card.transform.position = camera.transform.position + camera.transform.forward*5f;
        current_card.transform.LookAt(camera.transform.position);
        current_card.transform.forward *= -1f; 

        ScratchCard card_class = current_card.GetComponent<ScratchCard>();
        card_class.GenerateCard(price, value);

        current_card.transform.localScale = new Vector3(0.7f,0.7f,0.7f);

        can_walk = false;
    }

    public void card_done () {
        money += prize_money;
        
        Destroy(current_card);
        can_walk = true;
    }
}
