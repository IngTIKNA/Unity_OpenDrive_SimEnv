# Road Generator | Development of simulation environment in Unity based on OpenDRIVE file to simulate vehicle behavior and point cloud data
______________________________________________________________________________________________________________________________________________________
<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/1_1.png">
______________________________________________________________________________________________________________________________________________________

Game engines can be used for the validation of environment simulation, vehicle dynamics, and complex AD algorithms in different scenarios. The goal of this project was to create a simulation environment in Unity3D using the predefined OpenDRIVE file to simulate vehicle behavior and point-cloud data gathered by perception sensors. This research work focuses on the development of a road generator application for creating a road structure and corresponding visualization data in a driving simulator. The application is based on the OpenDRIVE file, which is a standardized file format. The OpenDRIVE files contain a dataset that describes the road structure and covers all the properties regarding road geometry.  


![image](https://user-images.githubusercontent.com/29532729/113411704-cc8c2680-93b6-11eb-8141-b8b7f7225606.png)

To display the road and its environment in Unity, EasyRoads3D toolset which contains dynamic crossing prefabs and side objects, such as bridges, guard rails, traffic signs is used. 


______________________________________________________________________________________________________________________________________________________
# OPEN DRIVE – ROAD DESCRIPTION STANDARD 

Several standardized file formats have been developed in the automotive industry for companies that require similar datasets for their simulators, including road networks and road objects. One of the most prominent standardized file formats developed by VIRES is OpenDRIVE. 

This file format was created in order to simplify the exchange of data between simulation programs and companies. The OpenDRIVE standard was recognized by many companies after the first public version was released in 2006. OpenDRIVE stores data in an XML format. In the XML file, nodes indicate various properties of the road, including intersections, elevations, and lane parameters. The figure below shows the hierarchical architecture of OpenDRIVE.


![image](https://user-images.githubusercontent.com/29532729/113411818-0a894a80-93b7-11eb-8ef8-8cd8f72dfde8.png)

______________________________________________________________________________________________________________________________________________________
# Road Geometry

Geospatial data needs to be represented in various ways, including mapping and visualization. Therefore, a suitable type of data and object representation must be provided by the geodata models. In OpenDRIVE, the geometry of road segments is derived analytically from the chord line and the track coordinate system. In simulators, road layouts must be visualized relative to the center of the 3D world.

![image](https://user-images.githubusercontent.com/29532729/113411887-399fbc00-93b7-11eb-9503-1111135585d8.png)

OpenDRIVE describes the layout of each road segment using geometric records based on its types, such as lines, arcs, and spirals. 

The following information is contained in each record:

• An s offset,

• A world coordinate which defines starting point,

• A heading angle which describes the initial orientation of the road segment (In
 OpenDRIVE standard, heading angle is defined in terms of radians.)

• The length of the road segment.


Further, each geometry record contains an attribute that describes the type of geometry. 

Geometries can be divided into several types:

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
# DESIGN and DEVELOPMENT OF ROAD GENERATOR APPLICATION

![image](https://user-images.githubusercontent.com/29532729/113470544-ad44d600-9456-11eb-9bdf-43488373403e.png)


As can be seen in the above fiure, the RoadGenerator application has several subcomponents that handle different tasks, like parsing and reading XML file, getting node attributes, etc. 

The RoadGenerator application is responsible for creating the segments of the road geometry, which is described in OpenDRIVE file, in Unity platform. Below figure indicates an overview of RoadGenerator application's flowchart.

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

We compared the road network generated in Unity with the SUMO simulator to determine how accurate the road generator can create road networks. The below figure indicates the comparison of the road networks simulated in different platforms.

<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/verification.png">
