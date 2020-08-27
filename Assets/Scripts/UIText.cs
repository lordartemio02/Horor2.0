using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIText UITextName;
    public Text UITextOut;
    public GameObject Text;
    private void Awake()
    {
        UITextName = this;
    }
    public void TakeNameTrue(string Name)
    {
        UITextOut.text = Name;
        UITextOut.gameObject.SetActive(true);
    }
    public void TakeNameFalse()
    {
        UITextOut.gameObject.SetActive(false);
    }
    private void Update()
    {
        TakeNameTrue(UITextOut.ToString());
        TakeNameFalse();
    }

}
