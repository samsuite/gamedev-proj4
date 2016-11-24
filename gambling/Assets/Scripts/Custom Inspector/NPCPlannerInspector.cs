using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCPlanner))]
public class NPCPlannerInspector : Editor {


    NPCPlanner planner;
    int editIndex = 0;
    bool editing = false;

    Vector3 finalDestination;

    public override void OnInspectorGUI() {

        planner = target as NPCPlanner;
        DrawDefaultInspector();

        finalDestination = planner.transform.position;

        for (int i = 0; i < planner.itinerary.Count; i++){

            if (editing && i == editIndex){

                // start a horizonal layout
                GUILayout.BeginHorizontal();

                // add the title of the entry
                EditorGUILayout.LabelField(planner.itinerary[i].type.ToString().ToUpper(), EditorStyles.boldLabel);

                // and a button for when we're done editing
                if (GUILayout.Button("DONE", GUILayout.MaxWidth(50f))){

                    if (planner.itinerary[editIndex].type == NPCPlanner.actionType.walk){
                        Tools.current = Tool.Move;
                    }

                    editing = false;
                    SceneView.RepaintAll();
                }

                GUILayout.EndHorizontal();
                // ok, horizontal layout is done


                EditorGUI.indentLevel++;
                switch (planner.itinerary[i].type){

                    case NPCPlanner.actionType.respond:
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("player says", EditorStyles.label, GUILayout.MaxWidth(90f));
                        NPCPlanner.NPCAction tempAction0 = planner.itinerary[i];
                        tempAction0.text = EditorGUILayout.TextField(planner.itinerary[i].text);
                        planner.itinerary[i] = tempAction0;

                        GUILayout.EndHorizontal();
                        break;
                    case NPCPlanner.actionType.talk:
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("says", EditorStyles.label, GUILayout.MaxWidth(90f));
                        NPCPlanner.NPCAction tempAction1 = planner.itinerary[i];
                        tempAction1.text = EditorGUILayout.TextField(planner.itinerary[i].text);
                        planner.itinerary[i] = tempAction1;

                        GUILayout.EndHorizontal();
                        break;
                    case NPCPlanner.actionType.wait:
                        GUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("for", EditorStyles.label, GUILayout.MaxWidth(50f));
                        NPCPlanner.NPCAction tempAction2 = planner.itinerary[i];
                        tempAction2.duration = EditorGUILayout.FloatField(planner.itinerary[i].duration);
                        planner.itinerary[i] = tempAction2;
                        EditorGUILayout.LabelField("seconds", EditorStyles.label, GUILayout.MaxWidth(80f));

                        GUILayout.EndHorizontal();
                        break;
                    case NPCPlanner.actionType.walk:
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("to", EditorStyles.label, GUILayout.MaxWidth(50f));

                        NPCPlanner.NPCAction tempAction3 = planner.itinerary[i];
                        tempAction3.destination = EditorGUILayout.Vector3Field("", planner.itinerary[i].destination);
                        planner.itinerary[i] = tempAction3;

                        SceneView.RepaintAll();
                        finalDestination = planner.itinerary[i].destination;
                        GUILayout.EndHorizontal();
                        break;
                    default:
                        break;

                }
                EditorGUI.indentLevel--;
                EditorUtility.SetDirty(planner);

            }
            else {

                // start a horizonal layout
                GUILayout.BeginHorizontal();

                // add the title of the entry
                EditorGUILayout.LabelField(planner.itinerary[i].type.ToString().ToUpper(), EditorStyles.boldLabel);

                // and a button for editing
                if (GUILayout.Button("EDIT", GUILayout.MaxWidth(50f))){

                    if (planner.itinerary[editIndex].type == NPCPlanner.actionType.walk && editing){
                        Tools.current = Tool.Move;
                    }

                    editing = true;
                    editIndex = i;
                    SceneView.RepaintAll();
                }

                // and a button for removing
                if (GUILayout.Button("REMOVE", GUILayout.MaxWidth(70f))){

                    editing = false;
                    planner.RemoveAction(i);
                    SceneView.RepaintAll();
                    break;
                }

                GUILayout.EndHorizontal();
                // ok, horizontal layout is done

                EditorGUI.indentLevel++;
                switch (planner.itinerary[i].type){

                    case NPCPlanner.actionType.respond:
                        EditorGUILayout.LabelField("player says \""+planner.itinerary[i].text+"\"", EditorStyles.label);
                        break;
                    case NPCPlanner.actionType.talk:
                        EditorGUILayout.LabelField("says \""+planner.itinerary[i].text+"\"", EditorStyles.label);
                        break;
                    case NPCPlanner.actionType.wait:
                        EditorGUILayout.LabelField("for "+planner.itinerary[i].duration+" seconds", EditorStyles.label);
                        break;
                    case NPCPlanner.actionType.walk:
                        EditorGUILayout.LabelField("to "+planner.itinerary[i].destination.x +", "+planner.itinerary[i].destination.y+", "+planner.itinerary[i].destination.z, EditorStyles.label);
                        finalDestination = planner.itinerary[i].destination;
                        break;
                    default:
                        break;

                }
                EditorGUI.indentLevel--;

            }

            GUILayout.Space(10);
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("WAIT", GUILayout.MaxWidth(150f))){

            if (editing && planner.itinerary.Count > 0){
                if (planner.itinerary[editIndex].type == NPCPlanner.actionType.walk){
                    Tools.current = Tool.Move;
                }
            }

            planner.AddAction(NPCPlanner.actionType.wait);
            editing = true;
            editIndex = planner.itinerary.Count-1;

            EditorUtility.SetDirty(planner);
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("WALK", GUILayout.MaxWidth(150f))){
            planner.AddAction(NPCPlanner.actionType.walk);

            NPCPlanner.NPCAction newAction = planner.itinerary[planner.itinerary.Count-1];
            newAction.destination = finalDestination + new Vector3(1f,0f,0f);
            planner.itinerary[planner.itinerary.Count-1] = newAction;

            editing = true;
            editIndex = planner.itinerary.Count-1;

            EditorUtility.SetDirty(planner);
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("TALK", GUILayout.MaxWidth(150f))){

            if (editing && planner.itinerary.Count > 0){
                if (planner.itinerary[editIndex].type == NPCPlanner.actionType.walk){
                    Tools.current = Tool.Move;
                }
            }

            planner.AddAction(NPCPlanner.actionType.talk);
            editing = true;
            editIndex = planner.itinerary.Count-1;

            EditorUtility.SetDirty(planner);
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("RESPOND", GUILayout.MaxWidth(150f))){

            if (editing && planner.itinerary.Count > 0){
                if (planner.itinerary[editIndex].type == NPCPlanner.actionType.walk){
                    Tools.current = Tool.Move;
                }
            }

            planner.AddAction(NPCPlanner.actionType.respond);
            editing = true;
            editIndex = planner.itinerary.Count-1;

            EditorUtility.SetDirty(planner);
            SceneView.RepaintAll();
        }

        GUILayout.EndHorizontal();

    }

    private void OnSceneGUI () {
        planner = target as NPCPlanner;

        if (!Application.isPlaying){
            planner.startLocation = planner.transform.position;
            EditorUtility.SetDirty(planner);
        }

        Vector3 currentpos = planner.startLocation;


        Handles.color = Color.green;
        Handles.DrawWireDisc(currentpos,Vector3.up,0.6f);
        Handles.DrawSolidDisc(currentpos,Vector3.up,0.1f);

        for (int i = 0; i < planner.itinerary.Count; i++){
            if (planner.itinerary[i].type == NPCPlanner.actionType.walk){
                Handles.DrawLine(currentpos, planner.itinerary[i].destination);
                currentpos = planner.itinerary[i].destination;

                Handles.DrawWireDisc(currentpos,Vector3.up,0.6f);
                Handles.DrawSolidDisc(currentpos,Vector3.up,0.1f);
            }
        }

        // if we're currently editing a walking element
        if (editing && planner.itinerary[editIndex].type == NPCPlanner.actionType.walk){
            Tools.current = Tool.None;

            NPCPlanner.NPCAction newAction = planner.itinerary[editIndex];
            newAction.destination = Handles.DoPositionHandle(planner.itinerary[editIndex].destination, Quaternion.identity);
            planner.itinerary[editIndex] = newAction;

            Repaint();
        }

    }
}
