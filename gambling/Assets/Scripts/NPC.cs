using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NPCPlanner))]
public class NPC : MonoBehaviour {


    public float walkSpeed = 10f;
    float turnspeed = 15f;

    bool acting = false;
    int act_index = 0;

    NPCPlanner planner;
    bool taskcomplete = false;
    float time_waited = 0f;
    Vector3 original_pos;


	void Start () {
        planner = GetComponent<NPCPlanner>();
	    Act();
	}
	
    void Update (){
        // if we're acting and we're not at the end of the itinerary
        if (acting && act_index < planner.itinerary.Count){
            // do whatever the current item on the itinerary is

            switch (planner.itinerary[act_index].type){

                // player says something
                case NPCPlanner.actionType.respond:

                    print("player says \""+planner.itinerary[act_index].text+"\"");
                    taskcomplete = true;

                    break;

                // this NPC says something
                case NPCPlanner.actionType.talk:

                    print("says \""+planner.itinerary[act_index].text+"\"");
                    taskcomplete = true;

                    break;

                // this NPC is waiting
                case NPCPlanner.actionType.wait:

                    time_waited += Time.deltaTime;
                    if (time_waited >= planner.itinerary[act_index].duration){
                        taskcomplete = true;
                    }

                    break;

                // this NPC is walking to a location
                case NPCPlanner.actionType.walk:

                    // move in the appropriate direction the appropriate amount
                    Vector3 moveVector = (planner.itinerary[act_index].destination - transform.position).normalized;
                    transform.position += moveVector * walkSpeed * Time.deltaTime;
                    // turn to face the right direction
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), Time.deltaTime*turnspeed);

                    // if we've gone far enough, we're done
                    if ((transform.position - original_pos).magnitude >= (planner.itinerary[act_index].destination - original_pos).magnitude){
                        // snap us to exactly the right place
                        transform.position = planner.itinerary[act_index].destination;
                        taskcomplete = true;
                    }


                    break;
                default:
                    break;
            }

            if (taskcomplete){
                act_index ++;
                BeginTask();
            }

        }
    }

    void BeginTask() {

        // if we're not at the end of the itinerary
        if (act_index < planner.itinerary.Count){

            switch (planner.itinerary[act_index].type){
                case NPCPlanner.actionType.respond:
                    break;
                case NPCPlanner.actionType.talk:
                    break;
                case NPCPlanner.actionType.wait:
                    // start the idle animation
                    time_waited = 0f;
                    break;
                case NPCPlanner.actionType.walk:
                    // start the walking animation
                    original_pos = transform.position;
                    break;
                default:
                    break;
            }

            taskcomplete = false;
        }
        else {
            print("ALL DONE");
        }
    }

	void Act() {
        acting = true;
        act_index = 0;
        BeginTask();
    }
}
