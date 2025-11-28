using UnityEngine;

/// Attach this script to a Camera GameObject (not the Player).
/// Make sure the Camera's Z (tai distance) on scene on oikea; script s‰ilytt‰‰ kameran Z-aseman.
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
        // Jos haluat, varmista ett‰ offset.z on oikea (esim. -10 for 2D).
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1) Muunna hiiren ruutukoordinaatti maailmaksi
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, cam.transform.position.z * -1f));
        // korjaa Z: k‰ytet‰‰n vain X/Y suunnan laskuihin (2D), asetetaan sama Z kuin camera pit‰‰ sen oikealla et‰isyydell‰
        mouseWorld.z = target.position.z;

        // 2) Laske haluttu seurauspiste: piste pelaajan ja hiiren v‰lilt‰ + offset
        Vector3 between = Vector3.Lerp(target.position, mouseWorld, cursorWeight);
        Vector3 desiredPosition = between + offset;

        // S‰ilyt‰ kameran z-arvo (ei muuta kameran et‰isyytt‰)
        desiredPosition.z = transform.position.z;

        // 3) Sujuva liike Lerp:ll‰
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
