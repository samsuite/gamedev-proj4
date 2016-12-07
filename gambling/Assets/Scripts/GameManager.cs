using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour {

    // a class to store globally accessible variables (yeah, it's a singleton. don't judge me)

    public static GameObject player;
    public static GameObject camera;
    public static int money = 150;
    public static int cards_left = 0;
    public static int day = 0;
    public static bool can_walk = true;

    bool showing_spent = false;
    float spent_timer = 0f;

    public Text money_text;
    
    public GameObject spent_image;
    public Text spent_text;
    static GameObject current_card;
    static int prize_money;

    bool made = false;

    public GameObject card_inp;
    public static GameObject card;
    public Button done_button_inp;
    public static Button done_button;

	void Awake () {
	    player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main.gameObject;
        card = card_inp;
        done_button = done_button_inp;

        done_button.gameObject.SetActive(false);
        spent_image.SetActive(false);
	}


    void Update () {
        money_text.text = "$"+money.ToString();

        if (showing_spent){
            spent_timer -= Time.deltaTime;
            if (spent_timer <= 0f){
                showing_spent = false;
                spent_image.SetActive(false);
            }
        }
    }

    /*
    //// delete this later
    void LateUpdate () {
        if (!made){
            scratch_card(10, 6);
            buy_cards (50);
            made = true;
        }
    }
    */


    public void buy_cards (int amount_spent) {
        spent_timer = 2f;
        showing_spent = true;
        spent_image.SetActive(true);
        spent_text.text = "YOU SPENT $"+amount_spent+" ON SCRATCH CARDS";
    }

    public static void scratch_card (int price, int value) {
        prize_money = value;
        money -= price;
        
        current_card = Instantiate(card);
        current_card.transform.position = camera.transform.position + camera.transform.forward*5f;
        current_card.transform.LookAt(camera.transform.position);
        current_card.transform.forward *= -1f; 

        ScratchCard card_class = current_card.GetComponent<ScratchCard>();
        card_class.GenerateCard(price, value);

        current_card.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        done_button.gameObject.SetActive(true);

        can_walk = false;
    }

    public void card_done () {
        money += prize_money;
        done_button.gameObject.SetActive(false);
        
        Destroy(current_card);
        can_walk = true;
    }
}
