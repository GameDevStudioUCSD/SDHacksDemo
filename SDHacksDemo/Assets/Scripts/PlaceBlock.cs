using UnityEngine;
using System.Collections;

public class PlaceBlock : MonoBehaviour {
    // All public variables can be edited from the Unity Editor itself
    // This holds a copy of the Block prefab that we'll be instantiating
    public GameObject objectToPlace;
    // This holds a copy of the Camera's transform. We'll need this to see
    // where we should place our block in the vertical access
    public Transform cameraTransform;
    // This variable will help prevent the block from clipping through the floor
    public float minYVal = 1;
    // This variable will determine how far away the block will be placed from 
    // the player
    public float distanceFromPlayer = 5;

    // This holds a reference to the current block the player is holding
    private GameObject placedObject;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // This checks if the left mouse button has been pressed. The number 
        // passed to the function checks for different clicks
        // 0 Represents a left click
        // 1 Represents a right click
        // 2 Represents a middle click
        if (Input.GetMouseButtonDown(0)) // This method only fires once per click
        {
            // Determine the object to place's position in the xz plane
            float xzAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            float x = ( Mathf.Sin(xzAngle));
            float z = ( Mathf.Cos(xzAngle));
            Vector3 placementVector = new Vector3(x, .25f, z);
            placedObject = (GameObject)GameObject.Instantiate(objectToPlace, transform.position + (distanceFromPlayer * placementVector), transform.rotation);
            // This will make the player object the parent of our block in the 
            // scene hierarchy and will make the block move and rotate with the 
            // player. 
            placedObject.transform.parent = transform;
        }
        if (Input.GetMouseButton(0)) // This method returns true while the button is held
        {
            // Move the block up and down the y-axis
            float yAngle = cameraTransform.rotation.eulerAngles.x * Mathf.Deg2Rad;
            float y = 1.5f - 5*Mathf.Sin(yAngle);
            if (y < minYVal)
                y = minYVal;
            Vector3 oldVect = placedObject.transform.position;
            Vector3 newVect = new Vector3(oldVect.x, y, oldVect.z);
            placedObject.transform.position = newVect;

        }
        // This method checks if the mouse click has been released
        if (Input.GetMouseButtonUp(0)) // This method fires once per release
        {
            // This removes the parent-child dependency from the block to 
            // player
            placedObject.transform.parent = null;
            // A rigid body allows physics and gravity to affect the block
            placedObject.AddComponent<Rigidbody>();
        }
	}
}
