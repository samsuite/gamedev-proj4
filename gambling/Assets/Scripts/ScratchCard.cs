using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScratchCard : MonoBehaviour {

    public GameObject win_1;
	public GameObject win_5;
    public GameObject win_10;
    public GameObject lose;

    [HideInInspector]
    public int reward;
    [HideInInspector]
    public int cost;

    public void GenerateCardRandom(){
        var rand = Random.value;
        if (rand < 0.5f){
            GenerateCard(0, 0);
        }
        else {
            GenerateCard(0, Random.Range(1, 15));
        }
    }

	public void GenerateCard(int card_cost, int card_reward){

        cost = card_cost;
        reward = card_reward;

        int rows = 3;
		int columns = 2;

        int[,] positions = new int[columns, rows];

        for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
                positions[j,i] = 0;
            }
        }



        if (reward > 60){
            //uh oh -- there's not enough room on the card for that many wins
            print ("ERROR - card reward can't be over $60");
            return;
        }
        else {
            while (card_reward >= 10){

                int x = Random.Range(0,columns);
                int y = Random.Range(0,rows);

                while (positions[x,y] != 0) {
                    x = Random.Range(0,columns);
                    y = Random.Range(0,rows);
                }

                positions[x,y] = 10;
                card_reward -= 10;
            }

            while (card_reward >= 5){

                int x = Random.Range(0,columns);
                int y = Random.Range(0,rows);

                while (positions[x,y] != 0) {
                    x = Random.Range(0,columns);
                    y = Random.Range(0,rows);
                }

                positions[x,y] = 5;
                card_reward -= 5;
            }

            while (card_reward >= 1){

                int x = Random.Range(0,columns);
                int y = Random.Range(0,rows);

                while (positions[x,y] != 0) {
                    x = Random.Range(0,columns);
                    y = Random.Range(0,rows);
                }

                positions[x,y] = 1;
                card_reward -= 1;
            }
        }


        for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {

                GameObject go;

                switch (positions[j,i]){
                    case 10:
                        go = (GameObject)Instantiate(win_10);
                        break;
                    case 5:
                        go = (GameObject)Instantiate(win_5);
                        break;
                    case 1:
                        go = (GameObject)Instantiate(win_1);
                        break;
                    default:
                        go = (GameObject)Instantiate(lose);
                        break;
                }

                Vector3 pos = new Vector3(-4.15f+j*8.1f, -7f+i*5f, 0f);

				go.name = i+" "+j;
                go.transform.parent = this.transform;
				go.transform.localPosition = pos;
                go.transform.localRotation = Quaternion.identity;
            }
        }

	}
}
