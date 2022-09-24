using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    // Camera Information
    public Transform cameraTransform;
    private Vector3 orignalCameraPos;
    public GameObject player;

    // Shake Parameters
    public float shakeAmount = 0.7f;
    public float shakeTimer = 0;
    private float shakeMax;



    // Start is called before the first frame update
    void Start()
    {
        shakeMax = shakeAmount;
    }

    // Update is called once per frame
    void Update()
    {
        StartCameraShakeEffect();
        if(shakeTimer <= 0)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
        if(shakeAmount > shakeMax) { shakeAmount -= Time.deltaTime; }
    }

    public void StartCameraShakeEffect()
    {
        if (shakeTimer > 0)
        {
            cameraTransform.position = player.transform.position + Random.insideUnitSphere * shakeAmount;
            cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10);
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
            
        }
    }

}