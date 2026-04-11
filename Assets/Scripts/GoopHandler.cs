using UnityEngine;

public class GoopHandler : MonoBehaviour
{

    KinematicCharacterController.Examples.ExampleCharacterController charController;

    float coolDown = 0.1f;
    float currentCooldown = 0.0f;

    float initMaxSpeed = 0.0f;
    float initMaxAirSpeed = 0.0f;

    float currentMaxSpeed = 0.0f, currentMaxAirSpeed = 0.0f;

    public AudioClip goopTouchSoundClip;
    public AudioSource playerAudioSource;

    void Start() {
        charController = GetComponent<KinematicCharacterController.Examples.ExampleCharacterController>();
        initMaxSpeed = charController.MaxStableMoveSpeed;
        initMaxAirSpeed = charController.MaxAirMoveSpeed;
        currentMaxSpeed = initMaxSpeed;
        currentMaxAirSpeed = initMaxAirSpeed;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            currentCooldown = 0f;
        }
    }

    public void TriggerGoopJumpEvent(GameObject goop)
    {
            //Bounce goo
            Debug.Log("Adding Velocity!");
            if(currentCooldown <= 0.05f){
                playerAudioSource.PlayOneShot(goopTouchSoundClip, 0.6f); 
                charController.AddVelocity(goop.transform.TransformDirection(Vector3.forward) * (goop.GetComponent<GoopTrigger>().gooBounceAmount + Mathf.Abs(GetComponent<Rigidbody>().linearVelocity.y)));
            }
            currentCooldown = coolDown;
    }

    
    public void TriggerGoopRunSound()
    {
        playerAudioSource.PlayOneShot(goopTouchSoundClip, 0.1f); 
    }

    public void TriggerGoopRun(GameObject goop)
    {
            
            Debug.Log("More Speed!");
            currentMaxSpeed = initMaxSpeed * goop.GetComponent<GoopTrigger>().gooSpeedMultiplier;
            currentMaxAirSpeed = initMaxAirSpeed * goop.GetComponent<GoopTrigger>().gooSpeedMultiplier;

            charController.MaxStableMoveSpeed = currentMaxSpeed;
            charController.MaxAirMoveSpeed = currentMaxAirSpeed;
    }

        public void TriggerStopGoopRun(GameObject goop)
    {
            Debug.Log("More Speed!");
            currentMaxSpeed = initMaxSpeed;
            currentMaxAirSpeed = initMaxAirSpeed;

            charController.MaxStableMoveSpeed = currentMaxSpeed;
            charController.MaxAirMoveSpeed = currentMaxAirSpeed;
    }
}
