using UnityEngine;
using System.Collections;

public class pickUpable : MonoBehaviour {
    GameObject player1;
    GameObject player2;
    bool carrying;
    GameObject carriedObject;
    GameObject carrier;
    Vector3 tposition;

    // Use this for initialization
    void Start () {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        tposition = this.transform.position;
        carriedObject = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (carrying)
        {
            carry(carriedObject);
        }
        else
        {
            pickUp();
        }
	}

    void carry(GameObject o)
    {
        Vector3 nposition = carrier.transform.position + carrier.transform.forward * 1.25f;
        o.transform.position = nposition;
        if ((carrier == player1) && (Input.GetKeyDown(KeyCode.LeftControl))) {
            carrying = false;
            o.transform.position = new Vector3(nposition.x, tposition.y, nposition.z);
        }
        if ((carrier == player2) && (Input.GetKeyDown("[1]")))
        {
            carrying = false;
            o.transform.position = new Vector3(nposition.x, tposition.y, nposition.z);
        }
    }

    void pickUp()
    {
        if (((player1.transform.position - carriedObject.transform.position).sqrMagnitude < 3) &&
            (Input.GetKeyDown(KeyCode.LeftControl)))
        {
            carrier = player1;
            carrying = true;
        }

        if (((player2.transform.position - carriedObject.transform.position).sqrMagnitude < 3) &&
            (Input.GetKeyDown("[1]")))
        {
            carrier = player2;
            carrying = true;
        }

    }
}
