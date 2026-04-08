using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    int numTimesWorldGenerated = 0;

    public GameObject genSphere;

    public int worldSizeInBlocks = 4;
    // Basically the basis for how far apart individual prefabs will spawn
    public float baseBlockSpacing = 16;
    public Transform buildingBlocksParent;

    public void GenerateWorld(Transform worldSpawnPoint)
    {
        var buildingBlockSpawnIndex = 0;

        var basePosition = worldSpawnPoint.position;

        var blockSpacing = baseBlockSpacing + Mathf.Log((numTimesWorldGenerated*numTimesWorldGenerated+10));


        //Generate using given prefab list around my location
        for(int x = -worldSizeInBlocks; x < worldSizeInBlocks; x++)
        {
            for(int y = -(worldSizeInBlocks/3); y < worldSizeInBlocks; y++)
            {
                for(int z = -worldSizeInBlocks; z < worldSizeInBlocks; z++)
                {
                    buildingBlockSpawnIndex = Mathf.RoundToInt(Random.Range(0, buildingBlocksParent.childCount-1));

                    var clone = Instantiate(
                        buildingBlocksParent.GetChild(buildingBlockSpawnIndex).gameObject, 
                        new Vector3(basePosition.x + x * blockSpacing + Random.Range(-blockSpacing, blockSpacing), basePosition.y + y * blockSpacing + Random.Range(-blockSpacing, blockSpacing), basePosition.z + z * blockSpacing + Random.Range(-blockSpacing, blockSpacing)),
                        Quaternion.identity
                    );

            
                }
            }
        }

        //spawn next gen sphere
           var clone2 = Instantiate(
                genSphere, 
                new Vector3(basePosition.x + (worldSizeInBlocks / 2f) * blockSpacing, basePosition.y + worldSizeInBlocks * blockSpacing + 10f, basePosition.z + (worldSizeInBlocks / 2f) * blockSpacing),
                Quaternion.identity
            );  

        numTimesWorldGenerated ++;
    }
}
