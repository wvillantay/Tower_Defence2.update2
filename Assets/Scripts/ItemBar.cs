using UnityEngine;

public class ItemBar : MonoBehaviour
{
    public GameObject[] Turret_Points; // public array of gameobjects to hold all of our turret points
    private Color32 Highlight_Colour = new Color32(76, 107, 255, 127); // saving our highlight colour inside of a color32 variable
    private Color32 Default_Colour = new Color32(252, 255, 203, 71); // saving our default colour inside of a color32 variable
    private bool ready_to_place; // bool to determine whether the player is ready to place the turret or not
    public Camera cam; // getting a reference to our camera
    public GameObject Turret; // getting a reference to the turret prefab

    public void Highlight_Turret_Points()
    {
        if (!ready_to_place) // if we are not ready to place a  turret
        {
            foreach (GameObject turret_point in Turret_Points) // loop through each gameobject in the array
            {
                turret_point.transform.GetComponent<SpriteRenderer>().color = Highlight_Colour; // set its colour to the highlight colour
                ready_to_place = true; // set ready to place to true
            }
        }
        else
        {
            ready_to_place = false; // set ready to place to false
            foreach (GameObject turret_point in Turret_Points) // loop through each gameobject in the array
            {
                turret_point.transform.GetComponent<SpriteRenderer>().color = Default_Colour; // set its colour to the default colour
            }
        }
    }

    private void Update() // every frame
    {
        bool left_click = Input.GetMouseButton(0);  // converting an event to a bool to determine when we are left clicking

        if (left_click && ready_to_place && Economy.Instance.TryPlaceTurret(false) == true) // if the player left clicks, is ready to place a turret and has enough money to do so
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // getting the world position of our cursor and saving it to a vector 3 variable
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y); // convert the mouse position vector 3 into a vector 2 for later use

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero); // shoot a raycast at the mousepos2D and send the data to a Raycasthit2d 

            if (hit.collider != null) // if we hit something 
            {
                if (hit.transform.gameObject.CompareTag("Turret_Point")) // if we hit a turret point
                {
                    GameObject turret = Instantiate(Turret); // instantiate a turret under a new gameobject variable

                    turret.transform.position = hit.point; // set the turrets position to where we clicked in the circle
                    hit.transform.gameObject.SetActive(false); // set the turrer point to inactive

                    ready_to_place = false; // set ready to place to false

                    foreach (GameObject turret_point in Turret_Points) // loop through each gameobject in the array
                    {
                        turret_point.transform.GetComponent<SpriteRenderer>().color = Default_Colour; // set its colour to the default colour
                    }
                }
            }
        }
    }
}
