using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float lerpK;
    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    private Vector3 position;
    private float FOV;

    private new Camera camera;
    public static CameraController Instance { get; private set; }
    public Camera Camera { get { return camera; } }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        camera = GetComponent<Camera>();

        position = transform.position;
        
    }

    private void Update()
    {
        FOV += -Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;

        FOV = Mathf.Clamp(FOV, minFOV, maxFOV);

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, FOV, lerpK * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out float entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                position = transform.position + (dragStartPosition - dragCurrentPosition);

                position = new Vector3(Mathf.Clamp(position.x, minX, maxX), position.y, Mathf.Clamp(position.z, minZ, maxZ));
            }
        }

        transform.position = Vector3.Lerp(transform.position, position, lerpK * Time.deltaTime);
    }
}
