using UnityEngine;

public class PlayerGravityBody : GravityBody {
  public float maxSpeed = 4.5f;
  public float force = 8.0f;
  public float jumpSpeed = 5.0f;
  private bool jumping = false;

  public float horizontalForce {
    get { return Input.GetAxis("Horizontal") * this.force; }
  }
  
  
  /*
   * Public vertical force accessor
   */
  public float verticalForce {
    get { return Input.GetAxis("Vertical") * this.force; }
  }
  
  
  /*
   * Private update class method
   */
  private void Update() {
    // Check if jump button was pressed and ridig body is grounded
    if (Input.GetButtonDown("Jump") == true && this.grounded) {
      //jumping = true;
    }
  }
  
  /*
   * Private fixed update class method
   */
  void FixedUpdate() {
    if (attractors != null) {
      Attractor.Attract(this);
    }

    // Check if rigid body velocity is within limits and grounded
    if (rigidbody.velocity.magnitude < maxSpeed && grounded) {
      // Apply forces to rigid body
      rigidbody.AddForce(transform.rotation * Vector3.forward * verticalForce);
      rigidbody.AddForce(transform.rotation * Vector3.right * horizontalForce);
    }

    if (jumping == true) {
      // Apply jump forces and reset flag
      rigidbody.velocity = rigidbody.velocity + (rigidbody.transform.up * jumpSpeed);
			
	  jumping = false;
    }
  }
	
	public bool Jump
	{
		get { return jumping; }
		set { jumping = value; }
	}
	
	public bool Grounded
	{
		get { return grounded; }
	}
}
