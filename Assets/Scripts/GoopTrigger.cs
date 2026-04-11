using UnityEngine;

public class GoopTrigger : MonoBehaviour
{

    public int gooBounceAmount = 15;

    public int gooSpeedMultiplier = 5;

    public bool isJumpGoo;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isJumpGoo)
            {
                col.gameObject.GetComponent<GoopHandler>().TriggerGoopJumpEvent(this.gameObject);
            }
            else
            {
                col.gameObject.GetComponent<GoopHandler>().TriggerGoopRunSound();
                col.gameObject.GetComponent<GoopHandler>().TriggerGoopRun(this.gameObject);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isJumpGoo)
            {
                col.gameObject.GetComponent<GoopHandler>().TriggerGoopJumpEvent(this.gameObject);
            } else
            {
                col.gameObject.GetComponent<GoopHandler>().TriggerGoopRun(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!isJumpGoo)
            {
                col.gameObject.GetComponent<GoopHandler>().TriggerStopGoopRun(this.gameObject);
            }
        }
    }

}

