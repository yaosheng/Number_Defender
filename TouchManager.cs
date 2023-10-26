using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
    private GunMount gunMount;
    //public Aerolite targetAerolite;
    public float positionX;
    public float rotationZ;
    private float tempPositionX;

    void Awake( )
    {
        gunMount = GetComponent<GunMount>( );
    }

    public void OnDrag( PointerEventData data )
    {
        //gunMount.RayCastHit( );
        float temp = (data.position.x - positionX) / 20;
        int tempInt = 0;
        for(int i = 0; i < 20; i++) {
            if(temp >= -0.5f && temp < 0.5f) {
                tempInt = 0;
            }
            else if(temp >= 0.5f + i && temp < 1.5f + i) {
                tempInt = i;
            }
            else if(temp >= -1.5f - i && temp < -0.5f - i) {
                tempInt = -i;
            }
        }
        Debug.Log("tempInt :" + tempInt);
        gunMount.SwitchGoalAerolite(tempInt);
        //gunMount.Rotate(rotationZ, temp);
    }

    public void OnPointerDown( PointerEventData data )
    {
        //Debug.Log("point down");
        gunMount.ShowLine( );
        //gunMount.RayCastHit( );
        gunMount.SetGoalTarget( );
        positionX = data.position.x;
        rotationZ = transform.rotation.eulerAngles.z;
    }

    public void OnPointerUp( PointerEventData data )
    {
        //Debug.Log("point up");
        gunMount.CloseLine( );
        StartShootingMissile( );
    }

    void StartShootingMissile( )
    {
        gunMount.ShootingMissile( );
    }

    void OnPointerClick( PointerEventData data )
    {
        Debug.Log("click");
    }
}

//Debug.Log("drag position :" + data.position.x);
//int temp = data.delta.x
//Debug.Log("data.delta.x :" + data.delta.x);
//Debug.Log("temp :" + temp);
//gunMount.SwitchTargetAerolite((int)data.delta.x);
//Debug.Log(data.dragging);
//Debug.Log("position :" + data.position);
//if(temp >= -0.5f && temp < 0.5f) {
//    tempInt = 0;
//}
//else if(temp >= 0.5f && temp < 1.5f) {
//    tempInt = 1;
//}
//gunMount.SwitchGoalAerolite(Mathf.FloorToInt(temp));
//Debug.Log(data.)
//gunMount.Rotate(rotationZ, temp);
//Debug.Log("drag");

//void FindWorldPosition( )
//{
//    Vector2 newVector = new Vector2(720 / 2 + rect.localPosition.x, 1280 / 2 + rect.localPosition.y);
//    Vector2 newVector1 = new Vector2((float)(newVector.x / 720), (float)(newVector.y / 1280));
//    Vector3 vector = mainCamera.ViewportToWorldPoint(newVector1);
//    worldPosition = new Vector3(vector.x, vector.y, 0);
//    Debug.Log("worldPosition :" + vector);
//}
