using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;


public class PathCreator : MonoBehaviour{

	public ERRoadNetwork roadNetwork;
//__________________________________________	
    public ERRoad road1;
    public ERRoad road2;
    public ERRoad road3;
    public ERRoad road4;
    public ERRoad road5;
    public ERRoad road6;
    public ERRoad road7;
    public ERRoad road8;
    public ERRoad road9;
    public ERRoad road10;
    public ERRoad road11;
    public ERRoad road12;

//__________________________________________
    //public ERRoad[] roads;
//__________________________________________
	public GameObject go;

    void Start()
    {

        roadNetwork = new ERRoadNetwork();
        //_____________________________________________________________________________________________
        ERRoadType roadType = new ERRoadType();
		roadType.roadWidth = 35;
		roadType.roadMaterial = Resources.Load("Materials/roads/road material") as Material;
        //____________________________________________________________________________________________

/*
        roads[0] = roadNetwork.CreateRoad("road 1", roadType, markers1);
        roads[1] = roadNetwork.CreateRoad("road 2", roadType, markers2);
       
        roads[0] = roadNetwork.ConnectRoads(roads[0], roads[1]);
*/
       
        //_____________________________________________________________________________________________
        Vector3[] markers1 = new Vector3[2];
		markers1[0]  = new Vector3(70,     0,    0);
        markers1[1]  = new Vector3(430,    0,    0);
        //_____________________________________________________________________________________________
        Vector3[] markers2 = new Vector3[2];
        markers2[0]  = new Vector3(500,    0,   70);
        markers2[1]  = new Vector3(500,    0,  430);		
        //_____________________________________________________________________________________________
        Vector3[] markers3 = new Vector3[2];
        markers3[0]  = new Vector3(570,   0,  500);
        markers3[1]  = new Vector3(930,  0,  500);		
        //_____________________________________________________________________________________________
        Vector3[] markers4 = new Vector3[2];
        markers4[0]  = new Vector3(1000,   0,  430);
        markers4[1]  = new Vector3(1000,   0,  70);		
        //_____________________________________________________________________________________________
        Vector3[] markers5 = new Vector3[2];
        markers5[0]  = new Vector3(1070,   0,   0);
        markers5[1]  = new Vector3(1430,   0,  0);		
        //_____________________________________________________________________________________________
        Vector3[] markers6 = new Vector3[2];
        markers6[0]  = new Vector3(1500,   0,   -70);
        markers6[1]  = new Vector3(1500,   0,  -430);
        //_____________________________________________________________________________________________
        Vector3[] markers7 = new Vector3[2];
        markers7[0]  = new Vector3(1430,   0,  -500);
        markers7[1]  = new Vector3(1070,   0,  -500);
        //_____________________________________________________________________________________________
        Vector3[] markers8 = new Vector3[2];
        markers8[0]  = new Vector3(1000,   0,  -570);
        markers8[1]  = new Vector3(1000,   0,  -930);
        //_____________________________________________________________________________________________
        Vector3[] markers9 = new Vector3[2];
        markers9[0]  = new Vector3(930,    0, -1000);
        markers9[1]  = new Vector3(570,    0, -1000);
        //_____________________________________________________________________________________________
        Vector3[] markers10 = new Vector3[2];
        markers10[0]  = new Vector3(500,   0,  -930);
        markers10[1]  = new Vector3(500,   0,  -570);
        //_____________________________________________________________________________________________
        Vector3[] markers11 = new Vector3[2];
        markers11[0]  = new Vector3(430,   0,  -500);
        markers11[1]  = new Vector3(70,   0,  -500);
        //_____________________________________________________________________________________________
        Vector3[] markers12 = new Vector3[2];
        markers12[0]  = new Vector3(0,   0,  -430);
        markers12[1]  = new Vector3(0,   0,  -70);
        //_____________________________________________________________________________________________
        road1 = roadNetwork.CreateRoad("road 1", roadType, markers1);
        road2 = roadNetwork.CreateRoad("road 2", roadType, markers2);
        road3 = roadNetwork.CreateRoad("road 3", roadType, markers3);
        road4 = roadNetwork.CreateRoad("road 4", roadType, markers4);
        road5 = roadNetwork.CreateRoad("road 5", roadType, markers5);
        road6 = roadNetwork.CreateRoad("road 6", roadType, markers6);
        road7 = roadNetwork.CreateRoad("road 7", roadType, markers7);
        road8 = roadNetwork.CreateRoad("road 8", roadType, markers8);
        road9 = roadNetwork.CreateRoad("road 9", roadType, markers9);
        road10 = roadNetwork.CreateRoad("road 10", roadType, markers10);
        road11 = roadNetwork.CreateRoad("road 11", roadType, markers11);
        road12 = roadNetwork.CreateRoad("road 12", roadType, markers12);
        road1 = roadNetwork.ConnectRoads(road1, road2);
        road1 = roadNetwork.ConnectRoads(road1, road3);
        road1 = roadNetwork.ConnectRoads(road1, road4);
        road1 = roadNetwork.ConnectRoads(road1, road5);
        road1 = roadNetwork.ConnectRoads(road1, road6);
        road1 = roadNetwork.ConnectRoads(road1, road7);
        road1 = roadNetwork.ConnectRoads(road1, road8);
        road1 = roadNetwork.ConnectRoads(road1, road9);
        road1 = roadNetwork.ConnectRoads(road1, road10);
        road1 = roadNetwork.ConnectRoads(road1, road11);
        road1 = roadNetwork.ConnectRoads(road1, road12);
        road1.ClosedTrack(true);
        //road12 = roadNetwork.ConnectRoads(road12, road1);

        //road1 = roadNetwork.ConnectRoads(road1, road1);
        


        //go = Resources.Load("dynamic prefabs/zeg_T")  as GameObject;             // 
        //go.transform.Translate(3.5f, 3.5f, 3.5f);
        //go.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        //Instantiate(go, new Vector3(500, 0, 0), Quaternion.identity);
        //Instantiate (Resources.Load ("Category1/Hatchet")) as GameObject;


    }


    void Update()
    {

    }

}


