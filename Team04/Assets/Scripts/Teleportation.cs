using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private GameObject player;
    public GameObject[] teleportDestinations;
    private int currentDestinationIndex = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)||Input.GetButtonDown("js0"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            print("PLayer : " + player);
            

            if (teleportDestinations[currentDestinationIndex] != null)
            {
                print(player.transform.position );
                Vector3 position = teleportDestinations[currentDestinationIndex].transform.position;
                if(currentDestinationIndex==1)
                    currentDestinationIndex--;
                else 
                currentDestinationIndex++;
                position.y += 1f; //Add an offset to avoid clipping through the ground
                //position.x -= 2f;
                ////position.y -= 4f;
                //player.transform.position = position;
                Vector3 playerPosition =  player.transform.position;
                playerPosition.x= position.x;
                playerPosition.y= position.y;
                playerPosition.z = position.z;
                player.transform.position = playerPosition;
                print(position);
                print(player.transform.position);
                
            }
        }
    }

}
