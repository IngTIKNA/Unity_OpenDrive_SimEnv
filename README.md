# Developing road generator script for creating a simulation environment in Unity based on OpenDRIVE file to simulate vehicle behavior and point cloud data
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




![image](https://user-images.githubusercontent.com/29532729/113412006-7ec3ee00-93b7-11eb-812a-4e916c4a3e53.png)



- Line Geometry :  In case of a line geometry as shown in the above figure, its ending points can be found using the following equations:

  • xEnding  =  xStarting + cos(hdg) ∗ (sEnding − sGeom )

  •	yEnding  =  yStarting  + sin(hdg)  ∗ (sEnding − sGeom )














______________________________________________________________________________________________________________________________________________________












<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/1_2.png">

<img src="https://raw.githubusercontent.com/IngTIKNA/Unity_OpenDrive_SimEnv/main/pics/MultiLane/verification.png">
