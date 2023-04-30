using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private GameObject player;
    public GameObject[] teleportDestinations;
    private bool isPointerOnLevel1=false;
    private bool isPointerOnLevel2=false;

    private void Teleport(int index){
        if (teleportDestinations[index] != null)
            {
                print(player.transform.position );
                Vector3 position = teleportDestinations[index].transform.position;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)||Input.GetButtonDown("js10")) // Keyboard T, Android js2 (A)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            print("PLayer : " + player);
            if(isPointerOnLevel1)
            {
                Teleport(1);
            }
            if(isPointerOnLevel2)
            {
                Teleport(0);
            }

            
        }
    }

    public void OnPointerEnterLevel1()
    {
        isPointerOnLevel1 = true;
    }

    public void OnPointerExitLevel1()
    {
        isPointerOnLevel1 = false;
    }

    public void OnPointerEnterLevel2()
    {
        isPointerOnLevel2 = true;
    }

    public void OnPointerExitLevel2()
    {
        isPointerOnLevel2 = false;
    }

}
