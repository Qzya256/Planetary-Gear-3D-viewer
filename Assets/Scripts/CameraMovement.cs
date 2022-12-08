using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    private Transform previousTarget;
    [SerializeField] private float distanceToTarget = 10;
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeed;
    private bool moveToComponent;
    [SerializeField] private GameObject[] gearComponents;
    private int zoomValue;

    private Vector3 previousPosition;

    private void Awake()
    {
        zoomValue = 60;
        previousTarget = target;
    }
    private void Update()
    {
        if (cam.fieldOfView != zoomValue)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, zoomValue, zoomSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            moveToComponent = false;
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));


            previousPosition = newPosition;
        }
        else if (moveToComponent)
        {
            cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(target.position.x, target.position.y, target.position.z - distanceToTarget), speed);
            cam.transform.LookAt(target);
        }
    }
    public void ChangeTarget(Transform newTarget)
    {
        moveToComponent = true;
        target = newTarget;
        zoomValue = 30;

        for (int i = 0; i < gearComponents.Length; i++)
        {
            if (gearComponents[i].name == newTarget.name)
            {
                gearComponents[i].SetActive(true);
            }
            else
            {
                gearComponents[i].SetActive(false);
            }
        }
    }
    public void PlanetaryGearVisibleOn()
    {
        zoomValue = 60;
        moveToComponent = true;
        target = previousTarget;
        for (int i = 0; i < gearComponents.Length; i++)
        {
            gearComponents[i].SetActive(true);
        }
    }
}