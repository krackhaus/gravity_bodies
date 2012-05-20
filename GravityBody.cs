using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class GravityBody : MonoBehaviour
{
  /**
   * Current gravitational velocity.
   */
  private float velocity = 0f;

  /**
   * A counter to keep track of collisions with ground objects.
   */
  private int groundings = 0;
  
  /**
   * Current attractor.
   */
  public GravityAttractor attractor;

  /**
   * Returns whether this object is currently touching a ground object.
   */
  public bool grounded
  {
    get { return groundings > 0; }
  }

  /**
   * The up vector, relative to the current gravity attractor.
   */
  public Vector3 up
  {
    get { return (rigidbody.transform.position - attractor.transform.position).normalized; }
  }

  /**
   * The down vector, relative to the current gravity attractor.
   */
  public Vector3 down
  {
    get { return up * -1; }
  }

  /**
   * Apply gravitational force toward the attractor at the given rate of
   * acceleration.
   */
  public void Gravitate(float gravity)
  {
    // If we're grounded or just switched attractors, don't gravitate and
    // reset our velocity.
    if (grounded) {
      velocity = gravity;
    }
    // Otherwise, accelerate and apply force
    else {
      velocity = velocity + (Time.fixedDeltaTime * attractor.gravity);

      rigidbody.AddForce(down * rigidbody.mass * velocity, ForceMode.Acceleration);
    }
  }

  /**
   * Uprights the game object at the given rate, relative to the current
   * attractor.
   */
  public void UprightPosition(float rate)
  {
    Quaternion rotation = Quaternion.FromToRotation(transform.up, up) * transform.rotation;

    // Rotate the game object at the given rate
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rate);
  }

  /**
   * Sets up the attached Rigidbody component.
   */
  private void Start()
  {

    rigidbody.WakeUp();
    rigidbody.useGravity = false;
    rigidbody.freezeRotation = true;
  }

  /**
   * Increments collision counter when we hit ground objects.
   */
  private void OnCollisionEnter(Collision c)
  {
    if (c.gameObject.tag == "Ground") {
      groundings++;
    }
  }

  /**
   * Decrements collision counter when we hit ground objects.
   */
  private void OnCollisionExit(Collision c) {

    if (c.gameObject.tag == "Ground") {
      groundings--;
    }

  }

  /**
   * Applies the attractor to this object upon updates.
   */
  private void FixedUpdate() {

    if (attractor != null) {
      attractor.Attract(this);
    }
  }

  /**
   * Adjusts the Rigidbody drag, based on grounded state.
   */
  private void LateUpdate() {

    if (grounded) {
      rigidbody.drag = 0.1f;
    }
    else {
      rigidbody.drag = 1.0f;
    }

  }

}
