using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;
//___________________________
using System.Xml;
using System.IO;
using System;   

public class XODR_Basics : MonoBehaviour{

    public ERRoad road1;
	public ERRoadNetwork roadNetwork;
//__________________________________________
	public GameObject go;

    public enum PathType : ushort{
    None = 0,
    Line = 1,
    Arc = 2,
    Spiral = 3
    }

    void Start()
    {

        roadNetwork = new ERRoadNetwork();
        //_____________________________________________________________________________________________
        ERRoadType roadType = new ERRoadType();
		roadType.roadWidth = 4;
		roadType.roadMaterial = Resources.Load("Materials/roads/road material") as Material;
       
        //____________________________________________________________________________________________
        var LineRoads = new List<ERRoad>();                                             //   The container individually keeps road components
        var linePaths = new List<LinePath>();                                       //   The container individually keeps road components with the line geometry
        var ArcRoads  = new List<ERRoad>();                                             //   The container individually keeps road components
        var arcPaths  = new List<ARCPath>();                                        //   The container individually keeps road components with the arc geometry
        //var SpiralRoads  = new List<ERRoad>();                                             //   The container individually keeps road components
        //var spiralPaths  = new List<SpiralPath>();    // TO DO : !!!!!               //   The container individually keeps road components with the spiral geometry

        //_____________________________________________________________________________________________
        Vector3[] markers1 = new Vector3[5];
		markers1[0]  = new Vector3( 0,     0,    0);
        markers1[1]  = new Vector3( 5,     0,    50);
        markers1[2]  = new Vector3(10,     0,    0);
        markers1[3]  = new Vector3(30,     0,    0);
        markers1[4]  = new Vector3(50,     0,    0);
        //_____________________________________________________________________________________________

        road1 = roadNetwork.CreateRoad("road 1", roadType, markers1);

        //LinePath l1 = new LinePath( 0, 0.0f, 0.0f, 400.0f, 0f);
        //LineRoads.Add(new ERRoad());
        //LineRoads[0] = roadNetwork.CreateRoad("line"+ l1.pathIndex.ToString(), roadType, l1.markers);
        //ARCPath  a1 = new ARCPath( 0, 2.61f, -325.3f, 7.93f, -4.02f, -0.086f);                                               //0.212f, 0f, -0.002f);                       //l1.xEnd, l1.yEnd, 800.0f, 0f, -0.002f);
        //ArcRoads.Add(new ERRoad());
        //ArcRoads[0] = roadNetwork.CreateRoad("line"+ a1.pathIndex.ToString(), roadType, a1.markers);            // put parameters into the relevant
    }


    void Update()
    {

    }

}

