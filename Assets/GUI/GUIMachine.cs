using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
//[ExecuteInEditMode]
public class GUIMachine: MonoBehaviour {
    public static GameObject SelectedPrefab{get;set;}
    public GameObject m_buttonPrefab;
    public GameObject m_canvas;
    GameObject m_rotatedObject;
    float m_basicRenderSize;
    public static CreateObjectButton SelectedButton { get; set; }
    public List<CreatableObject> m_creatables = new List<CreatableObject>();
    
    public static readonly Rect ButtonRect = new Rect(0, 0, 50, 50);
    //public bool EditorListCollapsed;
	// Use this for initialization
	void Start () 
    {
	    for(int i=0; i<m_creatables.Count; i++)
        {
            CreateObjectButton button = (Instantiate(m_buttonPrefab) as GameObject).GetComponent<CreateObjectButton>();
            button.transform.SetParent(m_canvas.transform, false);
            button.m_prefab = m_creatables[i];
            button.InitButton(i);
        }
        
	}
	
	// Update is called once per frame
    public void OnClick(BaseEventData data)
    {
        
        if (data as PointerEventData == null) return;
        if ((data as PointerEventData).button != PointerEventData.InputButton.Left) return;
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (SelectedPrefab != null)
        {

            
            position.z = 10;
            GameObject obj = (Instantiate(SelectedPrefab) as GameObject);
            obj.SendMessage("DragOnCretaion", (Vector2)position);
            obj.SendMessage("Place");
            SelectedPrefab = null;
            SelectedButton.Release();
        }
    }
    public void OnDragStart(BaseEventData data)
    {
       
        Vector3 position = Camera.main.ScreenToWorldPoint((data as PointerEventData).position);
        if (SelectedPrefab != null)
        {

            position.z = 10;
            m_rotatedObject = Instantiate(SelectedPrefab) as GameObject;
            m_rotatedObject.SendMessage("DragOnCretaion", (Vector2)position);
            m_basicRenderSize = m_rotatedObject.GetComponent<Renderer>().bounds.extents.x/transform.localScale.x;
            m_rotatedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            m_rotatedObject.GetComponent<Collider2D>().enabled = false;
            SelectedPrefab = null;
            
        }
    }
         
    public void OnDrag(BaseEventData data)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint((data as PointerEventData).position);
        
        if (m_rotatedObject == null) return;
        
        if(Input.touchCount==2)
        {
            Vector3 firstTouch=Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3 secondTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

            float angle = Vector2.Angle(Vector2.right, secondTouch - firstTouch);
            Debug.Log(angle);
            position = (secondTouch + firstTouch) / 2;
            position.z = 10;
            if (secondTouch.y < firstTouch.y)
              angle = -angle;
            float scale = (firstTouch - secondTouch).magnitude / m_basicRenderSize;
            m_rotatedObject.SendMessage("SetAngleOnCreation", angle);
            m_rotatedObject.SendMessage("SetScaleOnCreation", scale);
            m_rotatedObject.SendMessage("DragOnCretaion", (Vector2)position);
        }
        else
            m_rotatedObject.SendMessage("DragOnCretaion", (Vector2)position);
        
        
    }
    public void OnDragScroll(BaseEventData data)
    {
        //Debug.Log((data as PointerEventData).scrollDelta.y);
        if (m_rotatedObject == null) return;
        m_rotatedObject.SendMessage("RotateOnCreation", (data as PointerEventData).scrollDelta.y*10);
    }
    public void OnKeyPressed(BaseEventData data)
    {

    }
    public void OnDragFinish(BaseEventData data)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint((data as PointerEventData).position);
        position.z = 0;
        if (m_rotatedObject == null) return;
        m_rotatedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        m_rotatedObject.GetComponent<Collider2D>().enabled = true;
        m_rotatedObject.SendMessage("Place");
        m_rotatedObject = null;
        SelectedButton.Release();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Debug.Break();
    }
    static void InstantiatePrefab()
    {

    }
}
