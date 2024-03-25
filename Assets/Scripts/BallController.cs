using UnityEngine;

public class BallController : MonoBehaviour {
	public float initialVelocity = 600f;
	private Rigidbody rb;
	private bool ballInPlay;
 	void Awake ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
	if (Input.GetButtonDown("Fire1") && ballInPlay == false) {
		transform.parent = null; //unparent the ball from paddle
		ballInPlay = true;
		rb.isKinematic = false;
		rb.AddForce(new Vector3(initialVelocity,initialVelocity, 0));
	}
	}
}
