using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public LayerMask shootableLayers;
    public Transform shootPoint;
    public Transform mainCamera;
    public GameObject jumpGoopSplat, runGoopSplat;

    public bool standingOnBounceGel = false;

    public TextMeshPro heightText, timeText;

    float timeCount = 0;

    int destroyGoopAfterTime = 120;

    bool countTime = true;

    public void StopTimeCounter()
    {
        countTime = false;
        timeText.color = Color.green;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    int frameCounter = 0;

    // Update is called once per frame
    void Update()
    {

        heightText.text = "height:\n" + Mathf.RoundToInt(mainCamera.position.y).ToString() + "m";

        if (countTime)
        {
            timeCount += Time.deltaTime;
        }

        timeText.text = "Time: " + timeCount.ToString("0.###");

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(0);
        }

       if (Input.GetMouseButton(0))
        {
            if(frameCounter % 16 == 0)
                Shoot(0);
        }

             if (Input.GetMouseButtonDown(1))
        {
            Shoot(1);
        }

       if (Input.GetMouseButton(1))
        {
            if(frameCounter % 4 == 0)
                Shoot(1);
        }

        frameCounter++;
        if(frameCounter >= 100)
        {
            frameCounter = 0;
        }
    }

    RaycastHit hit;

    void Shoot(int type)
    {
        if(Physics.Raycast(shootPoint.position, mainCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, shootableLayers))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Goo"))
            {
                return;
            } else {
                if(type == 0){
                    var clone = Instantiate(jumpGoopSplat, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(clone, destroyGoopAfterTime);
                } else
                {
                    var clone = Instantiate(runGoopSplat, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(clone, destroyGoopAfterTime);
                }
            }
        }
    }
}
