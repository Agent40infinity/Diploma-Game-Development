using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f, jump = 10f;
    public LayerMask hitLayers;
    public float rayDistance = 10f;
    public bool isGrounded = false;
    private Rigidbody rigid;
    #region Unity Events
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(groundRay, rayDistance, hitLayers);
    }
    private void OnTriggerEnter(Collider col)
    {
        Item item = col.GetComponent<Item>();
        if (item)
        {
            item.Collect();
        }
    }
    private void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Move(inputH, inputV);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    #endregion
    #region Custom
    private void Jump()
    {
        if (isGrounded)
        {
            rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    private void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0, inputV);

        // [Optional] Move with camera
        Vector3 euler = Camera.main.transform.eulerAngles;
        direction = Quaternion.Euler(0, euler.y, 0) * direction; // Convert direction to relative direction to camera only on Y

        rigid.AddForce(direction * speed);
    }
	public bool Shoot()
	{
		Ray ray = new Ray(transform.position, Vector3.forward);
		RaycastHit hit;
		// Cast a ray forward
		if (Physics.Raycast(ray, out hit))
		{
			// If ray hit an item
			Item item = hit.collider.GetComponent<Item>();
			if (item)
			{
				// Collect it
				item.Collect();
				// Ray hit an item!
				return true;
			}
		}
		// Ray didnt hit anything
		return false;
	}
	#endregion
}

