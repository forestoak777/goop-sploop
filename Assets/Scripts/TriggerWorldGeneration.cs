using UnityEngine;

public class TriggerWorldGeneration : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger!");
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<WorldGeneration>().GenerateWorld(transform);
            Destroy(gameObject);
        }
    }
}
