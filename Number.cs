using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour
{
    [SerializeField]
    private float offset;
    [SerializeField]
    private SpriteRenderer[ ] sprites;
    private Aerolite aerolite;
    //public AeroliteManager am;
    public int number;

    public Digit[ ] digits = new Digit[4];


    void Awake( )
    {
        aerolite = transform.parent.GetComponent<Aerolite>();
    }

    public void SetNumber( int num )
    {
        this.number = num;
        ShowNumber(num);
        //SetDigits(num);
        //SetLocalPosition(num);
        SetLocalScale(num);
        SetRender(num);
        NumberAnimation( );
    }

    void SetRender(int num)
    {
        if(num == 2 || num == 3 || num == 5) {
            aerolite.SetRenderColor(new Color32(62, 62, 62, 255));
        }
        else if(num == 1) {
            aerolite.SetRenderColor(new Color32(255, 0, 0, 255));
        }
        else {
            aerolite.SetRenderColor(new Color32(255, 255, 255, 255));
        }
    }

    void ShowNumber( int temp )
    {
        int length = temp.ToString( ).Length;
        for(int i = 0; i < digits.Length; i++) {
            if(i == length - 1) {
                digits[i].gameObject.SetActive(true);
                digits[i].SetDigit(temp);
            }
            else {
                digits[i].gameObject.SetActive(false);
            }
        }

        //for(int i = 0; i < length; i++) {
        //    string s = temp.ToString( ).Substring((length - i - 1), 1);
        //    int temp1 = int.Parse(s);
        //    sprites[i].sprite = am.numbers[temp1];
        //}
    }

    void SetDigits( int num )
    {
        int temp = num.ToString( ).Length;

        for(int i = 0; i < sprites.Length; i++) {
            if(i < temp) {
                sprites[i].gameObject.SetActive(true);
            }
            else {
                sprites[i].gameObject.SetActive(false);
            }
        }
    }

    void SetLocalPosition( int num )
    {
        int temp = num.ToString( ).Length;
        transform.localPosition = Vector3.right * (-0.35f) * (4 - temp);
        switch(temp) {
            case 1:
            if(num == 2 || num == 3 || num == 5) {
                transform.localPosition = Vector3.right * -0.63f;
            }
            else if(num == 1) {
                transform.localPosition = Vector3.right * -0.62f;
            }
            else {
                transform.localPosition = Vector3.right * -0.667f;
            }
            break;
            case 2:
            transform.localPosition = Vector3.right * -0.32f;
            break;
            case 3:
            transform.localPosition = Vector3.right * -0.12f;
            break;
            case 4:
            transform.localPosition = Vector3.zero;
            break;
        }
    }

    void SetLocalScale( int num )
    {
        int temp = num.ToString( ).Length;
        switch(temp) {
            case 1:
            //transform.localScale = Vector3.one * 0.6f;
            if(num == 2 || num == 3 || num == 5) {
                aerolite.transform.localScale = Vector3.one * 0.75f;
            }
            else if (num == 1) {
                aerolite.transform.localScale = Vector3.one * 0.5f;
            }
            else {
                aerolite.transform.localScale = Vector3.one;
            }
            break;
            case 2:
            //transform.localScale = Vector3.one * 0.45f;
            aerolite.transform.localScale = Vector3.one * 1.3f;
            break;
            case 3:
            //transform.localScale = Vector3.one * 0.32f;
            aerolite.transform.localScale = Vector3.one * 1.5f;
            break;
            case 4:
            //transform.localScale = Vector3.one * 0.25f;
            aerolite.transform.localScale = Vector3.one * 1.8f;
            break;
        }
    }

    //public void SetAeroliteManager( AeroliteManager am )
    //{
    //    this.am = am;
    //}

    public void NumberAnimation( )
    {
        for(int i = 0; i < digits.Length; i++) {
            if(digits[i].gameObject.activeSelf) {
                Vector3 vector = digits[i]. transform.localScale;
                iTween.ScaleTo(digits[i].gameObject, iTween.Hash("scale", vector * 1.25f, "time", 0.05f, "easeType", iTween.EaseType.easeOutCubic));
                iTween.ScaleTo(digits[i].gameObject, iTween.Hash("scale", vector, "time", 0.05f, "delay", 0.05f, "easeType", iTween.EaseType.easeOutCubic));
            }
        }
        //Vector3 vector = transform.localScale;
        //iTween.ScaleTo(this.gameObject, iTween.Hash("scale", vector * 1.25f, "time", 0.2f, "easeType", iTween.EaseType.easeOutCubic));
        //iTween.ScaleTo(this.gameObject, iTween.Hash("scale", vector, "time", 0.2f, "delay", 0.2f, "easeType", iTween.EaseType.easeOutCubic));
    }
}

//void SetComposition(int temp )
//{
//    SetDigits(temp);
//    SetLocalPosition(temp);
//    SetLocalScale(temp);
//}

//void ChangeNumberComposition(int num )
//{
//    SetDigits(num);
//    SetLocalPosition(num);
//    SetLocalScale(num);
//    //switch(number.ToString( ).Length) {
//    //    case 1:
//    //    SetComposition(1);

//    //    break;
//    //    case 2:
//    //    SetComposition(2);

//    //    break;
//    //    case 3:
//    //    SetComposition(3);

//    //    break;
//    //    case 4:
//    //    SetComposition(4);

//    //    break;
//    //}
//}
