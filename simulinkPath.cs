using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;


public class simulinkPath : MonoBehaviour{

	public ERRoadNetwork roadNetwork;
//__________________________________________	
    public ERRoad road0;
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
		roadType.roadWidth = 20;
		roadType.roadMaterial = Resources.Load("Materials/roads/road material") as Material;
        //____________________________________________________________________________________________

/*
        roads[0] = roadNetwork.CreateRoad("road 1", roadType, markers1);
        roads[1] = roadNetwork.CreateRoad("road 2", roadType, markers2);
       
        roads[0] = roadNetwork.ConnectRoads(roads[0], roads[1]);
*/
        //_____________________________________________________________________________________________
        Vector3[] markers0 = new Vector3[2];
		markers0[0]  = new Vector3(-80f,   0f,   0f);
        markers0[1]  = new Vector3(-40f, 0.0f, 0.0f);       
        //_____________________________________________________________________________________________
        Vector3[] markers1 = new Vector3[2];
		markers1[0]  = new Vector3(0f,       0f,    5f);
        markers1[1]  = new Vector3(162.2f, 0.0f, 30.82f);
        //_____________________________________________________________________________________________
        Vector3[] markers2 = new Vector3[2];
        markers2[0]  = new Vector3(257.0f,    0,  87.46f);
        markers2[1]  = new Vector3(685.2f,    0,  421.8f);		
        //_____________________________________________________________________________________________
        Vector3[] markers3 = new Vector3[2];
        markers3[0]  = new Vector3(771.8f,   0, 662.2f);
        markers3[1]  = new Vector3(755.8f,  0,  742.6f);		
        //_____________________________________________________________________________________________
        //Vector3[] markers4 = new Vector3[2];
        //markers4[0]  = new Vector3(1000,   0,  430);
        //markers4[1]  = new Vector3(1000,   0,  70);		
        //_____________________________________________________________________________________________
        //_____________________________________________________________________________________________
        road0 = roadNetwork.CreateRoad("road 0", roadType, markers0);
        road1 = roadNetwork.CreateRoad("road 1", roadType, markers1);
        road2 = roadNetwork.CreateRoad("road 2", roadType, markers2);
        //road3 = roadNetwork.CreateRoad("road 3", roadType, markers3);
        road0 = roadNetwork.ConnectRoads(road0, road1);
        road0 = roadNetwork.ConnectRoads(road0, road2);
        //road1 = roadNetwork.ConnectRoads(road1, road3);
        road1.ClosedTrack(false);
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


