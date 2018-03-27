using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pickUp : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject[] inventoryIcons;
    bool getItem;
    public Transform popUpText;

    void OnTriggerEnter(Collider col)
    {
        if (!getItem)
        {
            if (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost")
            {
                popUpText.GetComponent<TextMesh>().text = "Press E to pick up ability";
                Instantiate(popUpText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), popUpText.rotation);
                Debug.Log("enter");
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        //look through children for existing icon
        if (!getItem && (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost"))
        {
            Debug.Log("in");
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (Transform child in inventoryPanel.transform)
                {
                    //if item already in inventory
                    if (child.gameObject.tag == col.gameObject.tag)
                    {
                        string c = child.Find("Text").GetComponent<Text>().text;
                        int tcount = System.Int32.Parse(c) + 1;
                        child.Find("Text").GetComponent<Text>().text = "" + tcount;
                        return;
                    }
                }


                GameObject i;

                if (col.gameObject.tag == "donut")
                {
                    i = Instantiate(inventoryIcons[0]);
                    i.transform.SetParent(inventoryPanel.transform);
                }
                else if (col.gameObject.tag == "disguise")
                {
                    i = Instantiate(inventoryIcons[1]);
                    i.transform.SetParent(inventoryPanel.transform);
                }
                else if (col.gameObject.tag == "boost")
                {
                    i = Instantiate(inventoryIcons[2]);
                    i.transform.SetParent(inventoryPanel.transform);
                }

                getItem = false;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        popUpText.GetComponent<TextMesh>().text = "";
        Instantiate(popUpText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), popUpText.rotation);
        Debug.Log("Exit");
    }

}
