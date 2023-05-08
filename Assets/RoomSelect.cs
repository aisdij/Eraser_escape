using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelect : MonoBehaviour
{
    private Main main;
    public List<RoomSelect> connectedRooms;
    public RoomContent roomContent;
    
        
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindObjectOfType<Main>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        main.onClickedRoom(this);
    }

}
