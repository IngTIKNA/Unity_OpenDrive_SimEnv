using UnityEngine;
using System.Collections;
using EasyRoads3Dv3;

public class ERVegetationStudio : ScriptableObject {

	static public bool VegetationStudio(){
		#if VEGETATION_STUDIO 
		return true;
		#else
		return false;
		#endif
	}

	static public void CreateVegetationMaskLine(GameObject go, float grassPerimeter, float plantPerimeter, float treePerimeter, float objectPerimeter, float largeObjectPerimeter){
		#if VEGETATION_STUDIO
		AwesomeTechnologies.VegetationMaskLine vegetationMaskLine = go.GetComponent<AwesomeTechnologies.VegetationMaskLine>();

		if (vegetationMaskLine == null)
		{
			vegetationMaskLine = go.AddComponent<AwesomeTechnologies.VegetationMaskLine>();
		}

		vegetationMaskLine.AdditionalGrassPerimiter = grassPerimeter; 
		vegetationMaskLine.AdditionalPlantPerimiter = plantPerimeter; 
		vegetationMaskLine.AdditionalTreePerimiter = treePerimeter;
		vegetationMaskLine.AdditionalObjectPerimiter = objectPerimeter;
		vegetationMaskLine.AdditionalLargeObjectPerimiter = largeObjectPerimeter;
		#endif
	}


	static public void UpdateVegetationMaskLine(GameObject go, ERVSData[] vsData, float grassPerimeter, float plantPerimeter, float treePerimeter, float objectPerimeter, float largeObjectPerimeter){
		#if VEGETATION_STUDIO
		AwesomeTechnologies.VegetationMaskLine vegetationMaskLine = go.GetComponent<AwesomeTechnologies.VegetationMaskLine>();

		if (vegetationMaskLine == null)
		{
			vegetationMaskLine = go.AddComponent<AwesomeTechnologies.VegetationMaskLine>();
			vegetationMaskLine.AdditionalGrassPerimiter = grassPerimeter; 
			vegetationMaskLine.AdditionalPlantPerimiter = plantPerimeter; 
			vegetationMaskLine.AdditionalTreePerimiter = treePerimeter;
			vegetationMaskLine.AdditionalObjectPerimiter = objectPerimeter;
			vegetationMaskLine.AdditionalLargeObjectPerimiter = largeObjectPerimeter;
		}

		vegetationMaskLine.ClearNodes();


		foreach (ERVSData data in vsData)
		{
			vegetationMaskLine.AddNodeToEnd(data.position, data.width, data.active);
		}


	//	vegetationMaskLine.AddNodeToEnd(nodePositions[0], widths[0], activeStates[0]);

		vegetationMaskLine.UpdateVegetationMask();
		#endif
	}

	static public void UpdateHeightmap(Bounds bounds){
		#if VEGETATION_STUDIO
		AwesomeTechnologies.VegetationStudio.VegetationStudioManager.RefreshTerrainHeightMap();
		#endif
	}



}

