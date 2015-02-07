using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class CameraControls : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public float moveSpeed = 0.01f;
    public static float m_margins = 10;
    Vector3 m_dragOrigin;
    ScreenBounds m_bounds;
    float m_ratio;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(m_margins, 0, 0), new Vector3(0, 100, 0));
    }
    public void Update()
    {
        //if (!Application.isPlaying) return;
        if (GUIMachine.SelectedButton != null) return;
 #if (UNITY_IPHONE || UNITY_ANDROID)&&!UNITY_EDITOR
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            // ... change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed*0.1f;

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);

        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float speed = moveSpeed*0.1f;
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(2))
        {
            m_dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 deltaPosition = Input.mousePosition - m_dragOrigin;
            float speed = moveSpeed * 0.05f;
            transform.Translate(-deltaPosition.x * speed, -deltaPosition.y * speed, 0);
            m_dragOrigin = Input.mousePosition;
            
        }
        if(Mathf.Abs(Input.mouseScrollDelta.y)>0.01f)
        {
            
            camera.orthographicSize -= Input.mouseScrollDelta.y * orthoZoomSpeed;
            
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
        }
#endif
        Camera mainCamera=Camera.main;
        float maxWidth = (m_bounds.right - m_bounds.left-2*m_margins) / 4;
        float maxHeight = (m_bounds.top - m_bounds.bottom - 2 * m_margins) / 4;
        if (mainCamera.orthographicSize > maxWidth)
            mainCamera.orthographicSize = maxWidth;
        float verticalSize = mainCamera.orthographicSize * m_ratio;
        if (verticalSize > maxHeight)
            mainCamera.orthographicSize = maxHeight / m_ratio;
        Vector3 translation= new Vector3();
        if (mainCamera.transform.position.x - mainCamera.orthographicSize*2 < m_bounds.left+m_margins)
            translation.x = m_bounds.left + m_margins + mainCamera.orthographicSize * 2 - mainCamera.transform.position.x;
        else if(mainCamera.transform.position.x + mainCamera.orthographicSize*2  >m_bounds.right-m_margins)
            translation.x = m_bounds.right - m_margins - mainCamera.orthographicSize * 2 - mainCamera.transform.position.x;
        if (mainCamera.transform.position.y - verticalSize * 2 < m_bounds.bottom+m_margins)
            translation.y = m_bounds.bottom + m_margins + verticalSize * 2 - mainCamera.transform.position.y;
        else if (mainCamera.transform.position.y + verticalSize * 2 > m_bounds.right-m_margins)
            translation.y = m_bounds.top - m_margins - verticalSize * 2 - mainCamera.transform.position.y;
        mainCamera.transform.Translate(translation);

        camera.transform.localScale = (Vector3)(Vector2.one * camera.orthographicSize)+Vector3.forward;
    }
    void Start()
    {
        m_bounds = Terrain.GetScreenBounds();
        m_ratio = (float)Screen.height / Screen.width;
    }
}
public struct ScreenBounds
{
    public float left;
    public float top;
    public float right;
    public float bottom;
}