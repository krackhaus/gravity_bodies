using UnityEngine;

public class PlayerGravityBody : GravityBody {
  public float maxSpeed = 4.5f;
  public float force = 8.0f;
  public float jumpingForce = 30.0f;

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
   * Private fixed update class method
   */
  void FixedUpdate() {
    if (attractor != null) {
      attractor.Attract(this);
    }

    // Check if rigid body velocity is within limits and grounded
    if (rigidbody.velocity.magnitude < maxSpeed && grounded) {
      // Apply forces to rigid body
      rigidbody.AddForce(transform.rotation * Vector3.forward * verticalForce);
      rigidbody.AddForce(transform.rotation * Vector3.right * horizontalForce);
    }

    if (jumping == true) {
      // Apply jump forces and reset flag
      rigidbody.AddForce(up * jumpingForce, ForceMode.VelocityChange);
      //rigidbody.velocity = rigidbody.velocity + (rigidbody.transform.up * jumpSpeed);
      jumping = false;
    }
  }

  public void Jump()
  {
    if (grounded) {
      jumping = true;
    }
  }

}
