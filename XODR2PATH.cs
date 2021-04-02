using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Data.SqlTypes;
using System.Xml.Linq;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;
//___________________________
using System.Xml;
using System.IO;


    public enum PathType : ushort{
    None = 0,
    Line = 1,
    Arc = 2,
    Spiral = 3
    }


public class XODR2PATH : MonoBehaviour{

	public ERRoadNetwork roadNetwork;

    void Start()
    {
        //_______________________________________________//
        roadNetwork = new ERRoadNetwork();               //
        ERRoadType roadType = new ERRoadType();          //
        //_______________________________________________//
        //____________________________________________________________________________________________//
		roadType.roadWidth = 8.0f;                                                                    //  Road Width
		roadType.roadMaterial = Resources.Load("Materials/roads/road material") as Material;          //  Road Material -> Marking Type etc.
        roadType.layer = 1;                                                                           //  
        roadType.tag = "Untagged";                                                                    //
        //____________________________________________________________________________________________//
        //______________________________________________//
        //______________________________________________//
        var LineRoads = new List<ERRoad>();             //   The container individually keeps road components
        var linePaths = new List<LinePath>();           //   The container individually keeps road components with the line geometry
        //______________________________________________//
        var ArcRoads  = new List<ERRoad>();             //   The container individually keeps road components
        var arcPaths  = new List<ARCPath>();            //   The container individually keeps road components with the arc geometry
        //______________________________________________//
        //______________________________________________/


        //_____________________ XML PARSING _____________________//
        //_______________________________________________________//
        XML_File XODR_File = new XML_File();                     //
        XmlNodeList geoList = XODR_File.getGeoList();            //
        XmlNodeList signalsList = XODR_File.getSignalsList();    //
        XmlNodeList PlanViewList = XODR_File.getPlanViewList();  //
        //_______________________________________________________//
        //_______________________________________________________//

        PathType pathType;    
        double tmp;
        int numberOfSignal;
        
        int[] arrGeo    = new int[PlanViewList.Count];
        int[] arrSignal = new int[PlanViewList.Count];
 
        int cntr    = 0;
        int cntrGeo = 0;
        int cntrSig = 0;
        for(int i=0; i< PlanViewList.Count; i++){
        //******************************************************************//
            cntr = 0;                                                       //
            foreach(XmlNode child in PlanViewList[i].ChildNodes){           //
                cntr ++;                                                    //
            }                                                               //
            //Debug.Log("Num of Geo" + cntr);                               //
            arrGeo[i] = cntr;                                               //
            //**************************************************************//
            cntr = 0;                                                       //
            if(signalsList.Count != 0){                                     //
                foreach(XmlNode child in signalsList[i].ChildNodes){        //
                    cntr ++;                                                //
                }                                                           //
            }                                                               //
            //Debug.Log("Num of Signal" + cntr);                            //
            arrSignal[i] = cntr;                                            //
        }                                                                   //
        //******************************************************************//



        int generalGeoCounter = 0;
        int generalSignalCounter = 0;
        float x_refPos, y_refPos;


        for(int j=0; j< PlanViewList.Count; j++){

            float[] sValues = new float[10];
            float[] tValues = new float[10];
            trafficObjects newTrafficObject = new trafficObjects();

            if(signalsList.Count != 0){
                //Debug.Log("Signal list count : " + signalsList.Count);
                for(int q=0; q< arrSignal[j]; q++){
                    //Debug.Log("---------------------------------*******************");
                    foreach(XmlNode child in signalsList[generalSignalCounter].ChildNodes){
                        //Debug.Log(child.Name);
                        if(child.Name == "signal"){ 
                            newTrafficObject.place = 1; 
                            XmlElement trafficSign = (System.Xml.XmlElement)child;
                            var nameObj = trafficSign.GetAttribute("name");
                            //Debug.Log("Name of object" + nameObj);
                            //_______________________________
                            var s_Value = trafficSign.GetAttribute("s");
                            double.TryParse( s_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                            float sVal = Convert.ToSingle(tmp);
                            //_______________________________
                            var t_Value = trafficSign.GetAttribute("t");
                            double.TryParse( t_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                            float tVal = Convert.ToSingle(tmp);
                            //_______________________________
                            //sValues[0] = sVal;
                            //tValues[0] = tVal;
                            newTrafficObject.sPos = sVal; 
                            newTrafficObject.tPos = tVal;
                            //Debug.Log("                 Planview"  +  j  +",   S val -> " + sVal);
                            //Debug.Log("                 t val -> " + tVal.ToString());
                        }else
                        {
                            Debug.Log("Unknown Child --------------");
                        }
                    }
                }
            }
            
            generalSignalCounter++;

            for(int k=0; k< arrGeo[j]; k++){
                foreach(XmlNode child in geoList[generalGeoCounter].ChildNodes){
                    //Debug.Log(generalGeoCounter);
                    //Debug.Log(geoList.Count);
                    if(child.Name == "line")
                    {
                        //_______________________________________________________________________________________________________________________________
                        XmlElement geoA = (System.Xml.XmlElement)geoList[generalGeoCounter];
                        //_______________________________________________________________________________________________________________________________//
                        var s_Value = geoA.GetAttribute("s");
                        double.TryParse( s_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float sVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________
                        var x_Value = geoA.GetAttribute("x");
                        double.TryParse( x_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float xVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________
                        var y_Value = geoA.GetAttribute("y");
                        double.TryParse( y_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float yVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________
                        var heading = geoA.GetAttribute("hdg");
                        double.TryParse( heading, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float hdgVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________
                        var length  = geoA.GetAttribute("length");
                        double.TryParse( length, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float lenVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________
                        pathType = PathType.Line;
                        linePaths.Add(new LinePath( linePaths.Count, xVal, yVal, lenVal, hdgVal));  
                        LineRoads.Add(new ERRoad());
                        LineRoads[linePaths.Count-1] = roadNetwork.CreateRoad("line"+ (linePaths.Count-1).ToString(), roadType, linePaths[linePaths.Count-1].markers);                
                        LineRoads[linePaths.Count-1].SetResolution(1000f);
                        if( sVal == 0f){
                            x_refPos = xVal;
                            y_refPos = yVal;
                        }

                        if( newTrafficObject.place == 1 && (newTrafficObject.sPosStart < sVal ) ){
                            //Debug.Log("111111111111");
                            newTrafficObject.sPosStart = sVal ;
                            newTrafficObject.pathIndex = linePaths.Count-1;
                            newTrafficObject.pathType = PathType.Line;
                        }else if( newTrafficObject.place == 1 && (newTrafficObject.done == 0 && (newTrafficObject.sPosStart >= sVal))){
                            newTrafficObject.sPosEnd = sVal;
                            float ratio = Math.Abs(newTrafficObject.sPos - newTrafficObject.sPosStart) / Math.Abs( newTrafficObject.sPosStart - newTrafficObject.sPosEnd);
                            Debug.Log( "_____________________________________________");
                            Debug.Log( "x start " + linePaths[linePaths.Count-1].xStart + " x end" + linePaths[linePaths.Count-1].xEnd ); 
                            Debug.Log( "y start " + linePaths[linePaths.Count-1].yStart + " y end" + linePaths[linePaths.Count-1].yEnd ); 
                            newTrafficObject.done = 1;
                        }

                    }else if(child.Name == "arc"){                                   //
                        XmlElement geoA = (System.Xml.XmlElement)geoList[generalGeoCounter];
                        //_______________________________________________________________________________________________________________________________//
                        var s_Value = geoA.GetAttribute("s");
                        double.TryParse( s_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float sVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________
                        var x_Value = geoA.GetAttribute("x");
                        double.TryParse( x_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float xVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________//
                        var y_Value = geoA.GetAttribute("y");
                        double.TryParse( y_Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float yVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________//
                        var heading = geoA.GetAttribute("hdg");
                        double.TryParse( heading, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float hdgVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________//
                        var length  = geoA.GetAttribute("length");
                        double.TryParse( length, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float lenVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________//
                        var child_curvature = child.Attributes["curvature"];
                        double.TryParse( child_curvature.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                        float curveVal = Convert.ToSingle(tmp);
                        //_______________________________________________________________________________________________________________________________//
                        //______________________________________________________________________//
                        pathType = PathType.Arc;
                        arcPaths.Add(new ARCPath( arcPaths.Count , xVal , yVal , lenVal, hdgVal, curveVal));
                        ArcRoads.Add(new ERRoad());
                        ArcRoads[arcPaths.Count-1] = roadNetwork.CreateRoad("arc"+ (arcPaths.Count-1).ToString(), roadType, arcPaths[arcPaths.Count-1].markers);
                        ArcRoads[arcPaths.Count-1].SetResolution(1000f);

                        if( sVal == 0f){
                            x_refPos = xVal;
                            y_refPos = yVal;
                        }

                    }else if(child.Name == "spiral"){
                        //Debug.Log("Child Node Name ==> spiral"  + i.ToString());
                        pathType = PathType.Spiral;
                    }
                    generalGeoCounter++;
                }   
            }
        








        }


    }


    public XmlNodeList parseFile()
    {
    //____________________________________________________________
        XmlDocument doc = new XmlDocument();
        doc.Load("CrossingComplex8Course.xml");                                             // CrossingComplex8Course  // CrossingComplex8Course.xodr
    //____________________________________________________________
        XmlNodeList elemList = doc.GetElementsByTagName("road");            //Element("OpenDRIVE").Elements("road");
        //Debug.Log("1-");
        //Debug.Log("Number of planView      => " + elemList.Count);
    //____________________________________________________________
        XmlNodeList geoList = doc.GetElementsByTagName("geometry");         //Element("OpenDRIVE").Elements("road");
        //Debug .Log("2-");
        //Debug.Log("Num of geometry tags" + geoList.Count);
    //____________________________________________________________
        //XmlNodeList plan = doc.GetElementsByTagName("planView");          //Element("OpenDRIVE").Elements("road"); 
        //Debug.Log("************** Count" + plan.Count.ToString());
        return geoList;
    }
}



//_______________________________________ Traffic Objects _______________________________

public class trafficObjects{// : MonoBehaviour{
    //____________________________
    public float xPos;
    public float yPos;
    public float sPos;
    public float tPos;
    public float sPosStart;
    public float sPosEnd;
    public float tPosEnd;
    public PathType pathType;
    public int pathIndex;
    public int place;               // determines if the object will be placed into the sim env
    public int done;
    //____________________________
    public trafficObjects(){
        this.place = 0;
        this.xPos = 0;
        this.yPos = 0;
        this.sPos = 0;
        this.tPos = 0;
        this.sPosStart = 0;
        this.sPosEnd = 0;
        this.tPosEnd = 0;
        this.done = 0;
    }

}



//___________________________________________________________________________________________
public abstract class paths{// : MonoBehaviour{

    public float xStart;
    public float yStart;
    public int pathIndex;
    public abstract void createPath(); 

}

//_____________________________________________________________________________________________________________________________________________________________________
//____________________________________________________________________________ LINE GEOMETRY __________________________________________________________________________
//____________________________________________________________  _______________________________________________________________________________________________________
public class LinePath : paths{

    public float xEnd;
    public float yEnd;
    public float angle;
    public float length;
    //______________________________________
    public Vector3[] markers  = new Vector3[2];
    public LinePath(int road_index , float x_Start, float y_Start, float length, float hdg){
        //_________________________________//
        this.xStart    = x_Start;          //
        this.yStart    = y_Start;          //
        this.angle     = hdg;              //
        this.length    = length;           //
        this.pathIndex = road_index;       //
        //_________________________________//
        //________________________________________________________________________________________________//                                                               //
        this.markers[0]  = new Vector3(this.xStart,  0,  this.yStart);                                    //
        this.xEnd = this.xStart + Convert.ToSingle(Math.Cos(this.angle)) * this.length;                   //
        this.yEnd = this.yStart + Convert.ToSingle(Math.Sin(this.angle)) * this.length;                   //
        this.markers[1]  = new Vector3(this.xEnd,  0,   this.yEnd);	                                      //
        //________________________________________________________________________________________________// 
    }

    public override void createPath(){

    }

}
//_____________________________________________________________________________________________________________________________________________________________________
//_____________________________________________________________________________________________________________________________________________________________________
//_____________________________________________________________________________________________________________________________________________________________________




//_____________________________________________________________________________________________________________________________________________________________________
//____________________________________________________________________________ ARC GEOMETRY ___________________________________________________________________________
//_____________________________________________________________________________________________________________________________________________________________________
public class ARCPath : paths{

    public float xCentral;
    public float xEnd;
    public float xTemp;

    public float yCentral;
    public float yEnd;
    public float yTemp;
    public float angle;
    public float length;
    public float radius;
    public float centralAngle;
    public int arrSize;
    public int sign;
    public Vector3[] tmp_markers;
    public Vector3[] markers;


    public ARCPath(int road_index ,float x_Start, float y_Start, float length, float hdg, float curvature){
        this.xStart       = x_Start;
        this.yStart       = y_Start;
        this.length       = length;
        this.pathIndex    = road_index;
        this.radius       = (Math.Abs(1 / curvature));
        this.centralAngle = this.length / this.radius;   
        //______________________________________________________________________________//
        //____________________________ NUMBER OF ONCREMENTS ____________________________//
        this.arrSize =  Mathf.FloorToInt(this.length / 0.5f) + 2;                       // must be at least 2 
        //______________________________________________________________________________//
        this.sign      =  Math.Sign(curvature);                                         //
        //_________________________________________________________________________________________________//
        this.markers = new Vector3[this.arrSize];                                                          //
        //this.tmp_markers = new Vector3[this.arrSize];                                                    //
        //_________________________________________________________________________________________________//
        
        //_________________________________________________________________________________________________//
        //_________________________________ CENTRAL POINT _________________________________________________//
        this.xCentral = this.xStart + Convert.ToSingle( Math.Cos(hdg + (Math.PI / 2) * this.sign * (-1) - Math.PI)) * this.radius;   
        this.yCentral = this.yStart + Convert.ToSingle( Math.Sin(hdg + (Math.PI / 2) * this.sign * (-1) - Math.PI)) * this.radius;        
        //______________________________________________________________________________________________________________________________________________________________________//   
        //______________________________________________________________________________________________________________________________________________________________________//   
        this.xEnd = this.xCentral + Convert.ToSingle( Math.Cos( (hdg + (this.centralAngle) * this.sign)  - (Math.PI / 2) * this.sign ) ) * this.radius;     //
        this.yEnd = this.yCentral + Convert.ToSingle( Math.Sin( (hdg + (this.centralAngle) * this.sign)  - (Math.PI / 2) * this.sign ) ) * this.radius;     //
        //______________________________________________________________________________________________________________________________________________________________________//
        this.markers[this.arrSize-1] = new Vector3(this.xEnd, 0f, this.yEnd);                                                                                                   //  END
        this.markers[0]  = new Vector3((this.xStart), 0,  (this.yStart));                                                                                                       //  START
        //______________________________________________________________________________________________________________________________________________________________________//
        //______________________________________________________________________________________________________________________________________________________________________//
        //___________________________________________________________ POINT BETWEEN START and END POINTS ___________________________________________________________________________________________________//
        //__________________________________________________________________________________________________________________________________________________________________________________________________//
        for(int i =1; i<(this.arrSize-1); i++){                                                                                                                                                             //
            this.xTemp = this.xCentral + Convert.ToSingle( Math.Cos( (hdg + ((i) * (this.centralAngle/(this.arrSize-1))) * this.sign)  - (Math.PI / 2) * this.sign )) * this.radius;                        // TODO => sign of cuvature   
            this.yTemp = this.yCentral + Convert.ToSingle( Math.Sin( (hdg + ((i) * (this.centralAngle/(this.arrSize-1))) * this.sign)  - (Math.PI / 2) * this.sign )) * this.radius;                        // TODO => sign of cuvature       
            this.markers[i]  = new Vector3(this.xTemp,  0,   this.yTemp);	                                                                                                                                //
        }                                                                                                                                                                                                   //
        //__________________________________________________________________________________________________________________________________________________________________________________________________//
        //__________________________________________________________________________________________________________________________________________________________________________________________________//        

    }

    public override void createPath(){
    }

}
//_____________________________________________________________________________________________________________________________________________________________________
//_____________________________________________________________________________________________________________________________________________________________________
//_____________________________________________________________________________________________________________________________________________________________________



public class XML_File{
    
    //______________________//
    XmlDocument doc;        //
    //______________________//
    public XML_File(){
        doc = new XmlDocument(); 
        doc.Load("Ahmet3.xml"); // ("CrossingComplex8Course.xml");  // ("BME.xml"); // ("Town01.xml");
    }

    public void getRoadList(){
        XmlNodeList roadList = doc.GetElementsByTagName("road");            //
        //Debug.Log("Number of planView      => " + elemList.Count);    
        //Debug.Log("Number of roadList      => " + roadList.Count);
        //return roadList;  
    }

    public XmlNodeList getGeoList(){
        XmlNodeList geoList = doc.GetElementsByTagName("geometry");         //
        //Debug.Log("Num of geometry tags" + geoList.Count);
        return geoList;
    }
    public XmlNodeList getSignalsList(){
        XmlNodeList signalsList = doc.GetElementsByTagName("signals");       //
        //Debug.Log("Number of Signals in first element => " + signalList[0].ChildNodes.Count); numberOfSignal
        return signalsList;
    }

    public XmlNodeList getPlanViewList(){
        XmlNodeList planViewList = doc.GetElementsByTagName("planView");       //
        //Debug.Log("Number of planViewList      => " + planViewList.Count);
        return planViewList;
    }

}
