using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    //Used to keep track of how many times the world has been generated.
    //Right now it just slightly increases the distance between prefabs
    //Meant to increase difficulty
    int numTimesWorldGenerated = 0;

    //This is the sphere that will trigger world generation. It's spawned above each
    //generated "chunk"
    public GameObject genSphere;

    //How big the world size is measured in chunks i guess.
    //wow is is really small but it works in the actual game so okie.
    public int worldSizeInBlocks = 4;

    // Basically the basis for how far apart individual prefabs will spawn
    public float baseBlockSpacing = 16;
    
    //So these are the containers of the prefabs that will spawn.
    //Index 0 is for level 1 prefabs, so on.
    public Transform[] buildingBlocksParents;

    public void GenerateWorld(Transform worldSpawnPoint)
    {
        var buildingBlockSpawnIndex = 0;

        //This is like the position of the spawner orb (genSphere) that gets triggered
        //Used as a basis for generating the rest of the world and shi
        var basePosition = worldSpawnPoint.position;

        //By default use the level 1 prefabs.
        var selectedBuildingBlocksParent = buildingBlocksParents[0];


        //If the generation is triggered above y level 500, change the prefabs that are spawned
        //to spice things up a bit.
        if(basePosition.y > 500)
        {
            //Level 2 okie alryee
            selectedBuildingBlocksParent = buildingBlocksParents[1];
        }
        
        
        var blockSpacing = baseBlockSpacing + Mathf.Log((numTimesWorldGenerated*numTimesWorldGenerated+10));


        //Generate using given prefab list around my location

        //Using a 3d array to spawn objects in a 3d grid. each "voxel" size is "blockSpacing"
        for(int x = -worldSizeInBlocks; x < worldSizeInBlocks; x++)
        {
            for(int y = -(worldSizeInBlocks/3); y < worldSizeInBlocks; y++)
            {
                for(int z = -worldSizeInBlocks; z < worldSizeInBlocks; z++)
                {

                    //Spawn a random prefab from the selected building block library
                    buildingBlockSpawnIndex = Mathf.RoundToInt(Random.Range(0, selectedBuildingBlocksParent.childCount-1));

                    //Spawn the shit.
                    var clone = Instantiate(
                        selectedBuildingBlocksParent.GetChild(buildingBlockSpawnIndex).gameObject, 
                        new Vector3(basePosition.x + x * blockSpacing + Random.Range(-blockSpacing, blockSpacing), basePosition.y + y * blockSpacing + Random.Range(-blockSpacing, blockSpacing), basePosition.z + z * blockSpacing + Random.Range(-blockSpacing, blockSpacing)),
                        selectedBuildingBlocksParent.GetChild(buildingBlockSpawnIndex).rotation
                    );

            
                }
            }
        }

            //spawn the next gen sphere
           var clone2 = Instantiate(
                genSphere, 
                new Vector3(basePosition.x + Random.Range(-worldSizeInBlocks, worldSizeInBlocks) * blockSpacing, basePosition.y + worldSizeInBlocks * blockSpacing + Random.Range(7f, 10f), basePosition.z + + Random.Range(-worldSizeInBlocks, worldSizeInBlocks) * blockSpacing),
                Quaternion.identity
            );  

        numTimesWorldGenerated ++;
    }
}
