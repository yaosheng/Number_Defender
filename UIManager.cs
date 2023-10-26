using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image restartUI;
    public RectTransform earthHealth;
    public Vector2 sizeDelta;

    void Start( )
    {
        restartUI.gameObject.SetActive(false);
        sizeDelta = new Vector2(earthHealth.sizeDelta.x, earthHealth.sizeDelta.y);
    }

    public void ShowRestartUI( )
    {
        restartUI.gameObject.SetActive(true);
    }

    public void HitEarth(float health , float maxHealth)
    {
        earthHealth.sizeDelta = new Vector2(sizeDelta.x * (health / maxHealth), sizeDelta.y);
    }
}
