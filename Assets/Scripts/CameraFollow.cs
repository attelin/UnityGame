using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Transform of the player / object to follow")]
    public Transform target;

    [Tooltip("Offset from the target position (useful to keep camera behind/above)")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Tooltip("How quickly the camera moves. Larger = snappier")]
    public float smoothSpeed = 6f;

    [Tooltip("How much the cursor pulls the camera towards itself (0 = ignore cursor, 1 = camera centered between player and cursor)")]
    [Range(0f, 1f)]
    public float cursorWeight = 0.25f;

    Camera cam;

    void Start()
    {
        if (target == null)
        {
            var t = GameObject.FindWithTag("Player");
            if (t != null) target = t.transform;
        }
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, cam.transform.position.z * -1f));
        mouseWorld.z = target.position.z;

        Vector3 between = Vector3.Lerp(target.position, mouseWorld, cursorWeight);
        Vector3 desiredPosition = between + offset;

        desiredPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
