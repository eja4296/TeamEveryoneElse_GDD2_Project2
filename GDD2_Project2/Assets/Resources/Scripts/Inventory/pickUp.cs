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
    public bool hidden = false;
    bool gotItem;
    TextMesh textObj;

    void Start()
    {
        text = GameObject.FindGameObjectWithTag("text");
        m = GameObject.FindGameObjectWithTag("mesh").GetComponent<SkinnedMeshRenderer>();
        textObj = text.GetComponent<TextMesh>();
        text.SetActive(false);
        robber = GetComponent<Robber>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost")
        {
            textObj.text = "Press E to search";
            text.SetActive(true);
        }

        else if (col.gameObject.tag == "car" || col.gameObject.tag == "dump")
        {
            textObj.text = "Press E to hide";
            text.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col)
    {
        //look through children for existing icon
        if (col.gameObject.tag == "donut" || col.gameObject.tag == "disguise" || col.gameObject.tag == "boost")
        {
            textObj.text = "Press E to search";

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

                col.tag = "Untagged";

                textObj.text = "";
            }
        }

        if (col.gameObject.tag == "car" || col.gameObject.tag == "dump")
        {
            if (active)
                textObj.text = "Press E to hide";

            if (Input.GetKeyDown(KeyCode.E))
            {
                m.enabled = false;
                active = false;
                robber.moving = false;
                hidden = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        text.SetActive(false);
    }

    void Update()
    {
        if (!active && cooldown >= 0)
            cooldown -= Time.deltaTime;
        else if (!active && cooldown <= 0)
        {
            textObj.text = "Press E to get out";

            if (Input.GetKeyDown(KeyCode.E))
            {
                robber.moving = true;
                active = true;
                m.enabled = true;
                hidden = false;
                cooldown = 0.5f;
            }
        }


    }
}
