using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunMount : MonoBehaviour
{

    public MissileBase missile;
    public Queue<MissileBase> missilePool = new Queue<MissileBase>( );
    public List<Aerolite> goalAerolites = new List<Aerolite>( );
    public LineRenderer line;
    public Transform missileLocker;
    public Vector3 worldPosition;
    public Aerolite targetAerolite;
    public int switchNumber;

    private RectTransform rect;
    private RaycastHit2D hit;
    private AeroliteManager am;
    private Camera mainCamera;
    public Aerolite goalAerolite;
    private List<Aerolite> tempAerolites = new List<Aerolite>( );

    void Awake( )
    {
        rect = GetComponent<RectTransform>( );
        mainCamera = Object.FindObjectOfType<Camera>( ) as Camera;
        am = Object.FindObjectOfType(typeof(AeroliteManager)) as AeroliteManager;
    }

    void Start( )
    {
        FindWorldPosition( );
        line.gameObject.SetActive(false);
        SetLineLength(transform.up * 16);
    }

    void Update( )
    {
        //Debug.Log("position :" + transform.position);
    }

    void FixedUpdate( )
    {
        RayCastHit( );
        RayCastHitAll( );
    }

    void RayCastHitAll( )
    {
        tempAerolites.Clear( );
        foreach(Aerolite aerolite in am.aeroliteGroup) {
            RaycastHit2D tempHit = Physics2D.Raycast(worldPosition, aerolite.transform.position - worldPosition, distance: Vector3.Distance(aerolite.transform.position, worldPosition), layerMask: 11);
            Debug.DrawLine(worldPosition, aerolite.transform.position, Color.blue);
            if(tempHit) {
                if(tempHit.collider.tag == "Aerolite") {
                    Aerolite al = tempHit.collider.GetComponent<Aerolite>( );
                    if(!tempAerolites.Contains(al) && al.sr.isVisible) {
                        tempAerolites.Add(al);
                    }
                }
            }
        }
        goalAerolites = tempAerolites;
    }

    public void RemoveGoalAerolite( )
    {
        goalAerolite = null;
    }

    public void SetGoalTarget( )
    {
        if(goalAerolite != null) {
            switchNumber = GetSwitchNumber( );
            DirectionTheTargetAerolite(goalAerolite.transform);
        }
        else {
            Aerolite[ ] aerolites = goalAerolites.ToArray( );
            aerolites = SortAerolites(aerolites);
            int temp = Mathf.FloorToInt(aerolites.Length / 2);
            if(aerolites.Length != 0) {
                goalAerolite = aerolites[temp];
                goalAerolite.SetGunMount(this);
                switchNumber = GetSwitchNumber( );
                DirectionTheTargetAerolite(goalAerolite.transform);
            }
            else {
                goalAerolite = null;
            }
        }
    }

    int GetSwitchNumber( )
    {
        int temp = 0;
        Aerolite[ ] aerolites = goalAerolites.ToArray( );
        aerolites = SortAerolites(aerolites);
        for(int i = 0; i < aerolites.Length; i++) {
            if(goalAerolite == aerolites[i]) {
                temp = i;
            }
        }
        return temp;
    }

    public void SwitchGoalAerolite( int space )
    {
        Aerolite[ ] aerolites = goalAerolites.ToArray( );
        aerolites = SortAerolites(aerolites);

        if(space < 0) {
            if(switchNumber >= Mathf.Abs(space) && switchNumber - Mathf.Abs(space) < aerolites.Length) {
                goalAerolite = aerolites[switchNumber - Mathf.Abs(space)];
            }
            else {
                goalAerolite = aerolites[0];
            }
            goalAerolite.SetGunMount(this);
        }
        else {
            if(switchNumber <= aerolites.Length - Mathf.Abs(space) - 1) {
                goalAerolite = aerolites[switchNumber + Mathf.Abs(space)];
            }
            else {
                goalAerolite = aerolites[aerolites.Length - 1];
            }
            goalAerolite.SetGunMount(this);
        }

        if(goalAerolite != null) {
            DirectionTheTargetAerolite(goalAerolite.transform);
        }
    }

    public void DirectionTheTargetAerolite( Transform target )
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    Aerolite[ ] SortAerolites( Aerolite[ ] aerolites )
    {
        Aerolite tempAerolite;
        float tempFloat;
        bool swapped;
        Aerolite[ ] tempArray = aerolites;

        float[ ] tempFloats = new float[tempArray.Length];

        for(int i = 0; i < tempArray.Length; i++) {
            tempFloats[i] = Vector3.Angle(tempArray[i].transform.position - worldPosition, Vector3.up);
            if(tempArray[i].transform.position.x - worldPosition.x < 0) {
                tempFloats[i] = -tempFloats[i];
            }
        }

        string tempString1 = "";
        for(int i = 0; i < tempArray.Length; i++) {
            tempString1 += tempArray[i].name + "[" + tempFloats[i] + "]" + ",";
        }
        Debug.Log("tempString 1:" + tempString1);

        for(int i = 0; i < tempArray.Length; i++) {
            swapped = false;
            for(int j = 0; j < tempArray.Length - 1 - i; j++) {
                if(tempFloats[j] > tempFloats[j + 1]) {
                    //if(tempArray[j].transform.position.x > tempArray[j + 1].transform.position.x) {
                    tempFloat = tempFloats[j];
                    tempFloats[j] = tempFloats[j + 1];
                    tempFloats[j + 1] = tempFloat;

                    tempAerolite = tempArray[j];
                    tempArray[j] = tempArray[j + 1];
                    tempArray[j + 1] = tempAerolite;
                    if(!swapped) {
                        swapped = true;
                    }
                }
            }
            if(!swapped) {
                continue;
            }
        }

        string tempString2 = "";
        for(int i = 0; i < tempArray.Length; i++) {
            tempString2 += tempArray[i].name + "[" + tempFloats[i] + "]" + ",";
        }
        Debug.Log("tempString 2:" + tempString2);

        return tempArray;
    }

    void FindWorldPosition( )
    {
        Vector2 newVector = new Vector2(720 / 2 + rect.localPosition.x, 1280 / 2 + rect.localPosition.y);
        Vector2 newVector1 = new Vector2((float)(newVector.x / 720), (float)(newVector.y / 1280));
        Vector3 vector = mainCamera.ViewportToWorldPoint(newVector1);
        worldPosition = new Vector3(vector.x, vector.y, 0);
        Debug.Log("worldPosition :" + vector);
    }

    void SetLineLength( Vector2 vector )
    {
        line.SetPosition(0, worldPosition);
        line.SetPosition(1, worldPosition + transform.up * Vector2.Distance(vector, new Vector2(worldPosition.x, worldPosition.y)));
    }

    public void RayCastHit( )
    {
        hit = Physics2D.Raycast(worldPosition, transform.up, distance: 11.0f, layerMask: 11);
        if(hit) {
            if(hit.collider.tag == "Aerolite") {
                targetAerolite = hit.collider.GetComponent<Aerolite>( );
                Number number = hit.collider.GetComponent<Aerolite>( ).number;

                if(number.number == 1) {
                    targetAerolite = null;
                }
                SetLineLength(hit.point);
            }
            else {
                targetAerolite = null;
                SetLineLength(transform.up * 16);
            }
        }
        else {
            targetAerolite = null;
            SetLineLength(transform.up * 16);
        }
    }

    public void ShowLine( )
    {
        line.gameObject.SetActive(true);
    }

    public void CloseLine( )
    {
        line.gameObject.SetActive(false);
    }

    public void ShootingMissile( )
    {
        if(targetAerolite != null) {
            MissileBase mb;
            if(missilePool.Count > 0) {
                mb = missilePool.Dequeue( );
            }
            else {
                mb = Instantiate(missile) as MissileBase;
            }
            //Debug.Log("shooting");
            mb.gameObject.layer = 11;
            mb.transform.position = worldPosition;
            mb.transform.SetParent(missileLocker);
            mb.GetGunMount(this);
            mb.GetTargetAerolite(targetAerolite);
            mb.gameObject.SetActive(true);
            targetAerolite = null;
        }
    }

    public void Rotate( float origin, float offset )
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = Vector3.forward * (origin - offset * 0.5f);
        transform.rotation = rotation;
    }
}

//string tempString = "";
//for(int i = 0; i < aerolites.Length; i++) {
//    tempString += aerolites[i].name;
//}
//Debug.Log(tempString);
//int value = Mathf.Abs(temp);

//int temp1 = 0;
//for(int i = 0; i < aerolites.Length; i++) {
//    if(goalAerolite == aerolites[i]) {
//        if(space < 0) {
//            if(i >= Mathf.Abs(space)) {
//                goalAerolite = aerolites[i - Mathf.Abs(space)];
//            }
//            else {
//                goalAerolite = aerolites[0];
//            }
//            //DirectionTheTargetAerolite(goalAerolite.transform);
//            goalAerolite.SetGunMount(this);
//            temp1++;
//        }
//        else {
//            if(i <= aerolites.Length - Mathf.Abs(space) - 1) {
//                goalAerolite = aerolites[i + Mathf.Abs(space)];
//            }
//            else {
//                goalAerolite = aerolites[aerolites.Length - 1];
//            }
//            //DirectionTheTargetAerolite(goalAerolite.transform);
//            goalAerolite.SetGunMount(this);
//            temp1++;
//        }
//    }
//}

//if(space < 0 && i >= Mathf.Abs(space)) {
//    goalAerolite = aerolites[i - Mathf.Abs(space)];
//    goalAerolite.SetGunMount(this);
//    temp1++;
//}

//else if(space > 0 && i <= aerolites.Length - Mathf.Abs(space) - 1){
//    goalAerolite = aerolites[i + Mathf.Abs(space)];
//    goalAerolite.SetGunMount(this);
//    temp1++;
//}

//if(temp1 == 0 && goalAerolites.Count != 0) {
//    goalAerolite = aerolites[0];
//    goalAerolite.SetGunMount(this);
//}

//if(temp < 0) {
//    int temp1 = 0;
//    for(int i = 0; i < aerolites.Length; i++) {
//        if(scratchAerolite == aerolites[i]) {
//            if(i != 0) {
//                scratchAerolite = aerolites[i - 1];
//            }
//            temp1++;
//        }
//    }
//    if(temp1 == 0 && targetAerolites.Count != 0) {
//        scratchAerolite = aerolites[0];
//    }
//}
//else if(temp > 0) {
//    int temp1 = 0;
//    for(int i = 0; i < aerolites.Length; i++) {
//        if(scratchAerolite == aerolites[i]) {
//            if(i != aerolites.Length - 1) {
//                scratchAerolite = aerolites[i + 1];
//            }
//            temp1++;
//        }
//    }
//    if(temp1 == 0 && targetAerolites.Count != 0) {
//        scratchAerolite = aerolites[0];
//    }
//}

//if(hit && hit.collider.tag == "Aerolite") {
//    Number number = hit.collider.GetComponent<Aerolite>( ).number;


//    targetAerolite = hit.collider.GetComponent<Aerolite>( );
//    isHit = true;
//    hitPoint = hit.point;

//}
//else {
//    isHit = false;
//    targetAerolite = null;
//}

//if(number.number != 1) {
//    targetAerolite = hit.collider.GetComponent<Aerolite>( );
//    isHit = true;
//    hitPoint = hit.point;
//}

//if(hit) {
//    if(hit.collider.tag == "Aerolite") {
//        targetAerolite = hit.collider.GetComponent<Aerolite>( );
//    }
//    else {
//        targetAerolite = null;
//    }
//    isHit = true;
//    Debug.Log(hit.collider.name);
//    hitPoint = hit.point;
//}
//else {
//    isHit = false;
//}

//public void ShootingMissile( Vector2 target )
//{
//    MissileBase mb;
//    if(missilePool.Count > 0) {
//        mb = missilePool.Dequeue( );
//    }
//    else {
//        mb = Instantiate(missile) as MissileBase;
//    }
//    mb.transform.position = worldPosition;
//    mb.transform.SetParent(missileLocker);
//    mb.GetGunMount(this);
//    mb.GetDirection(target);
//    //mb.GetTargetAerolite( );
//    mb.gameObject.SetActive(true);

//}

//public void RayCastHit( )
//{
//    RaycastHit2D hit = Physics2D.Raycast( transform.position, transform.up, distance:15.0f, layerMask:9);
//    Debug.DrawLine(transform.position, transform.position + transform.up * 15.0f);
//    if(hit) {
//        isHit = true;
//        Debug.Log(hit.collider.name);
//        hitPoint = hit.point;
//    }
//    else {
//        isHit = false;
//    }
//}