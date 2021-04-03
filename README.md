# Road Generator | Development of simulation environment in Unity based on OpenDRIVE file to simulate vehicle behavior and point cloud data
______________________________________________________________________________________________________________________________________________________
<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/1_1.png">
______________________________________________________________________________________________________________________________________________________


Game engines can be used for validation of environment simulation, vehicle dynamics, and complex AD algorithms in different scenarios. The goal of this project was to develop a road generator script for creating a simulation environment in Unity3D based on the predefined OpenDRIVE file to simulate vehicle behavior and point-cloud data.


This research work focuses on the development of a road generator script for creating a road structure and corresponding visualization data in driving simulator based on the OpenDRIVE file, which is a standardized file format. The OpenDRIVE files contain a dataset which describes the road structure and covers all the properties regarding a road geometry.


![image](https://user-images.githubusercontent.com/29532729/113411704-cc8c2680-93b6-11eb-8141-b8b7f7225606.png)

To display the road and its environment in Unity, EasyRoads3D toolset which contains dynamic crossing prefabs and side objects, such as bridges, guard rails, traffic signs is used. 


______________________________________________________________________________________________________________________________________________________
# OPEN DRIVE – ROAD DESCRIPTION STANDARD 

Because of many companies in automotive industry which need similar dataset for their simulators, such as the description of road network and road objects, several standardized file formats were developed. OpenDRIVE which is one of the standardized file formats’ major examples was developed by VIRES. 

The idea behind creating a file format which contains all properties of road structure was to simplify the exchange of data among different companies and simulators. OpenDRIVE's first public version was released in 2006. Then, this standard has been recognized by a high number of companies. OpenDRIVE is aimed at storing the data in a XML format. The nodes of the XML file indicate various properties of the road, such as junction points, elevation, and lane parameters. Hierarchical architecture of OpenDRIVE structure is shown in the below figure.


![image](https://user-images.githubusercontent.com/29532729/113411818-0a894a80-93b7-11eb-8ef8-8cd8f72dfde8.png)

______________________________________________________________________________________________________________________________________________________
# Road Geometry

To represent geographic data, it is required to handle several tasks, such as mapping, visualization, etc. Therefore, the suitable type of data and object representation must be provided by the geodata models.
The road layout in OpenDRIVE, as previously mentioned, consists of analytical formulation of the road segments’ geometry based on the chord line and in the track coordinate system. To visualize road layout in the simulators, its geometry has to be created relative the center of 3D world.

![image](https://user-images.githubusercontent.com/29532729/113411887-399fbc00-93b7-11eb-9503-1111135585d8.png)

In OpenDRIVE standard, each road segment’s layout is described by some geometric records based on the its type, like line, arc, and spiral. Each of those records contains:

• An s offset,

• A world coordinate which defines starting point,

• A heading angle which describes the initial orientation of the road segment (In
 OpenDRIVE standard, heading angle is defined in terms of radians.)

• The length of the road segment.


Moreover, each geometry record is an attribute which characterizes the type of the current geometry. There are several kind of geometries, such as:

• Lines – have no additional parameters 

• Arcs – have a constant curvature parameter 

• Spiral – have two curvature parameters for the start and end of the segment 


______________________________________________________________________________________________________________________________________________________

![image](https://user-images.githubusercontent.com/29532729/113412006-7ec3ee00-93b7-11eb-812a-4e916c4a3e53.png)



- Line Geometry :  In case of a line geometry as shown in the above figure, its ending points can be found using the following equations:

  • xEnding  =  xStarting + cos(hdg) ∗ (sEnding − sGeom )

  •	yEnding  =  yStarting  + sin(hdg)  ∗ (sEnding − sGeom )


______________________________________________________________________________________________________________________________________________________


- Arc Geometry :  Arc geometry is bit more complicated that the line geometry. By using curvature parameter, the radius of the arc can be computed as:
 
  •	radius = | 1 / curvature |

![image](https://user-images.githubusercontent.com/29532729/113454566-d8083d80-9408-11eb-8261-22369df76716.png)

Since the initial heading angle and arc radius are known, the center of the arc can be computed depending on the curvature sign:

  •	xArc =  xStarting + cos (hdg + ( (pi/2) * Sign * (-1)) − π) ∗ radius
  
  •	yArc =  yStarting + sin (hdg  + ( (pi/2)  * Sign * (-1)) − π) ∗ radius

To compute the central angle of the arc in terms of radians, the following equation is used.

	θcentral =  length_arc / radius 

______________________________________________________________________________________________________________________________________________________
# DESIGN and DEVELOPMENT OF ROAD GENERATOR SCRIPT

![image](https://user-images.githubusercontent.com/29532729/113454915-aa6fc400-9409-11eb-954c-71fb0a94e497.png)


As can be seen in the above fiure, the RoadGenerator script has several subcomponents that handle different tasks, like parsing and reading XML file, getting node attributes, etc. 

The RoadGenerator script is responsible for creating the segments of the road geometry, which is described in OpenDRIVE file, in Unity platform. Below figure indicates an overview of RoadGenerator script's flowchart.

![image](https://user-images.githubusercontent.com/29532729/113454962-c4110b80-9409-11eb-9244-d7d55a0e0b37.png)

To be able to use the assets of EasyRoads3D toolset, it is required to create instances of ERRoadNetwork and ERRoadType classes. Moreover, the roadWidth and roadMaterial. 

![image](https://user-images.githubusercontent.com/29532729/113454989-d428eb00-9409-11eb-9a4d-1314b57ac15a.png)

Based on the order of geometry nodes in OpenDRIVE file, the Lineroads and Arcroads containers individually hold the road segment assets as a list. Likewise, the linePaths and arcPaths hold road geometry parameters such as, starting points, ending points, heading angle, etc. 

![image](https://user-images.githubusercontent.com/29532729/113455014-dd19bc80-9409-11eb-8753-9c7c22a72b12.png)


parseFile function deals with reading the file in given directory and selecting the nodes based on the desired tag name which is geometry. It returns those nodes in a list.

![image](https://user-images.githubusercontent.com/29532729/113455029-e30f9d80-9409-11eb-8e16-250b3921f6d2.png)


In the OpenDRIVE file, the geometric parameters of road layout are represented as attributes of child nodes under geometry node. To be able to perform arithmetic calculations by using attributes, it is required to convert them from string to float format. As depicted in the below figure, the attribute format is converted from string to float. 

![image](https://user-images.githubusercontent.com/29532729/113455113-1eaa6780-940a-11eb-9e8b-e3dbd67d3b4e.png)


All types of road geometries have common parameters such as starting points in X and Y axes, road segment id, to describe road geometry. Therefore, an abstract class which is named paths was created to hold these information as its variables. Other classes which are responsible for holding information about relevant road type like, LinePath and ArcPath, are derived from the paths abstract class.

![image](https://user-images.githubusercontent.com/29532729/113455129-2a962980-940a-11eb-9dac-b7d569ab5f78.png)


The constructor of LinePath class performs to compute ending point of a road segment based on the given starting point, road length, and heading angle.  Both starting and ending points of road segment are stored in a 3D Vector to be used for creating game objects in Unity via EasyRoads3D toolset.

![image](https://user-images.githubusercontent.com/29532729/113455157-397cdc00-940a-11eb-95a4-81b658031022.png)


In addition to the arguments taken by the LinePath class' constructor, the constructor of ArcPath class also takes the curvature parameter as an argument. By doing so, it acquires all parameters required for computing the starting point, final point, central point, and the points between starting and endpoint.


![image](https://user-images.githubusercontent.com/29532729/113455170-400b5380-940a-11eb-9c65-23e9f29c70eb.png)


To visualize road segments in Unity platform based on the calculated points, the assets of the EasyRoads3D toolset are used as game objects.

![image](https://user-images.githubusercontent.com/29532729/113455180-48638e80-940a-11eb-83dd-2057bd60ccc4.png)

______________________________________________________________________________________________________________________________________________________


<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/1_2.png">


# EVALUATION 

To see how accurately the road generator script can create road networks, the road network, which was generated in Unity game engine, was compared with the road network simulated in SUMO simulator.  The below figure indicates the comparison of the road networks simulated in different platforms.

<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/verification.png">
