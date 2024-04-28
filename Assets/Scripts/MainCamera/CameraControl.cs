using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 initialPosition;

    private void Start()
    {
        target = FindAnyObjectByType<Player>().gameObject;
    }
    
    private void Update()
    {
        Vector3 newPosition = new Vector3(target.transform.position.x,initialPosition.y + target.transform.position.y,initialPosition.z + target.transform.position.z);
        transform.position = newPosition;
    }
}
