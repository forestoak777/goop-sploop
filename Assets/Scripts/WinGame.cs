using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (col.gameObject.GetComponentInParent<PlayerManager>())
            {
                col.gameObject.GetComponentInParent<PlayerManager>().StopTimeCounter();
            } else
            {
                Debug.LogError("Player parent doesnt have playermanager script");
            }

            Destroy(this.gameObject);
        }
    }
}
