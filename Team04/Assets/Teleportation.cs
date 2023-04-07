using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private GameObject player;
    public GameObject TeleportationPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)||Input.GetButtonDown("js0"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            print("PLayer : " + player);
            if (TeleportationPoint != null)
            {
                print(player.transform.position );
                Vector3 position = TeleportationPoint.transform.position;
                position.y += 4f; //Add an offset to avoid clipping through the ground
                //position.x -= 2f;
                ////position.y -= 4f;
                //player.transform.position = position;
                //Vector3 playerPosition =  player.transform.position;
                //playerPosition.x= position.x;
                //playerPosition.z = position.z;
                player.transform.position = position;
                print(position);
                print(player.transform.position);
                
            }
        }
    }

}
