using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pickUp : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject[] inventoryIcons;
    public Transform popUpText;
    bool active = true;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost")
        {
            popUpText.GetComponent<TextMesh>().text = "Press E to pick up ability";
            Instantiate(popUpText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), popUpText.rotation);
        }

        if (col.gameObject.tag == "car" || col.gameObject.tag == "dump")
        {
            popUpText.GetComponent<TextMesh>().text = "Press E to hide";
            Instantiate(popUpText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), popUpText.rotation);
        }
    }

    void OnTriggerStay(Collider col)
    {
        //look through children for existing icon
        if (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost")
        {
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
            }
        }

        if (col.gameObject.tag == "car" || col.gameObject.tag == "dump")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                active = false;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        Destroy(popUpText, 1.0f);
    }

    private void Update()
    {
        while(!active)
        {
            popUpText.GetComponent<TextMesh>().text = "Press E to hide";
            Instantiate(popUpText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), popUpText.rotation);

            if (Input.GetKeyDown(KeyCode.E))
            {
                active = true;
                this.gameObject.SetActive(true);
            }
        }
    }
}
