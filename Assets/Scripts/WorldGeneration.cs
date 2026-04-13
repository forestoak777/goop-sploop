using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    int numTimesWorldGenerated = 0;

    public GameObject genSphere;

    public int worldSizeInBlocks = 4;
    // Basically the basis for how far apart individual prefabs will spawn
    public float baseBlockSpacing = 16;
    public Transform[] buildingBlocksParents;

    public void GenerateWorld(Transform worldSpawnPoint)
    {
        var buildingBlockSpawnIndex = 0;

        var basePosition = worldSpawnPoint.position;

        var selectedBuildingBlocksParent = buildingBlocksParents[0];

        if(basePosition.y > 500)
        {
            selectedBuildingBlocksParent = buildingBlocksParents[1];
        }
        
        var blockSpacing = baseBlockSpacing + Mathf.Log((numTimesWorldGenerated*numTimesWorldGenerated+10));


        //Generate using given prefab list around my location
        for(int x = -worldSizeInBlocks; x < worldSizeInBlocks; x++)
        {
            for(int y = -(worldSizeInBlocks/3); y < worldSizeInBlocks; y++)
            {
                for(int z = -worldSizeInBlocks; z < worldSizeInBlocks; z++)
                {
                    buildingBlockSpawnIndex = Mathf.RoundToInt(Random.Range(0, selectedBuildingBlocksParent.childCount-1));

                    var clone = Instantiate(
                        selectedBuildingBlocksParent.GetChild(buildingBlockSpawnIndex).gameObject, 
                        new Vector3(basePosition.x + x * blockSpacing + Random.Range(-blockSpacing, blockSpacing), basePosition.y + y * blockSpacing + Random.Range(-blockSpacing, blockSpacing), basePosition.z + z * blockSpacing + Random.Range(-blockSpacing, blockSpacing)),
                        selectedBuildingBlocksParent.GetChild(buildingBlockSpawnIndex).rotation
                    );

            
                }
            }
        }

        //spawn next gen sphere
           var clone2 = Instantiate(
                genSphere, 
                new Vector3(basePosition.x + Random.Range(-worldSizeInBlocks, worldSizeInBlocks) * blockSpacing, basePosition.y + worldSizeInBlocks * blockSpacing + Random.Range(7f, 10f), basePosition.z + + Random.Range(-worldSizeInBlocks, worldSizeInBlocks) * blockSpacing),
                Quaternion.identity
            );  

        numTimesWorldGenerated ++;
    }
}
