using UnityEngine;
using System.Collections;

public class Digit : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer[ ] sprites;
    private AeroliteManager am;

    void Awake( )
    {
        am = Object.FindObjectOfType(typeof(AeroliteManager)) as AeroliteManager;
    }

    public void SetDigit(int temp)
    {
        int length = temp.ToString( ).Length;

        if(length == sprites.Length) {
            for(int i = 0; i < length; i++) {
                string s = temp.ToString( ).Substring((length - i - 1), 1);
                int temp1 = int.Parse(s);
                sprites[i].sprite = am.numbers[temp1];
            }
        }
    }
}
