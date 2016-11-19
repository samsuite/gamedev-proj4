using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // a class to store globally accessible variables (yeah, it's a singleton. don't judge me)

    public static GameObject player;
    public static GameObject camera;
    public static int money = 0;
    public static int day = 0;


	void Awake () {
	    player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main.gameObject;
	}
}
