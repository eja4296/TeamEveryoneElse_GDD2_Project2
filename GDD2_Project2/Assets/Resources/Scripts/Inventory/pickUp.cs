using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pickUp : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject[] inventoryIcons;
    public GameObject text;
    public Transform popUpText;
    bool active = true;
    SkinnedMeshRenderer m;
    Robber robber;
    float cooldown = 1;

    void Start()
    {
        text = GameObject.FindGameObjectWithTag("text");
        m = GameObject.FindGameObjectWithTag("mesh").GetComponent<SkinnedMeshRenderer>();
        text.SetActive(false);
        robber = GetComponent<Robber>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost")
        {
            popUpText.GetComponent<TextMesh>().text = "Press E to interact";
            text.SetActive(true);
        }

        if (col.gameObject.tag == "car" || col.gameObject.tag == "dump")
        {
            popUpText.GetComponent<TextMesh>().text = "Press E to interact";
            text.SetActive(true);
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
                m.enabled = false;
                active = false;
                robber.moving = false;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        text.SetActive(false);
    }

    void Update()
    {
        if(!active)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                robber.moving = true;
                active = true;
                m.enabled = true; ;
            }
        }
    }
}
