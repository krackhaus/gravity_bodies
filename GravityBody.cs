using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody))]

public class GravityBody : MonoBehaviour
{

  protected List<GravityAttractor> attractors = new List<GravityAttractor>();
  int currentAttractorIndex;

  /**
   * A counter to keep track of collisions with ground objects.
   */
  private int groundings = 0;
  
  
  /*
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
    get { return (rigidbody.transform.position - Attractor.transform.position).normalized; }
  }

  /**
   * The down vector, relative to the current gravity attractor.
   */
  public Vector3 down
  {
    get { return up * -1; }
  }

  /**
   * Apply the given force downward to the Rigidbody.
   */
  public void AddDownwardForce(float force)
  {
    rigidbody.AddForce(down * force * rigidbody.mass);
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
    // Simple Setup of Attractors
    currentAttractorIndex = 1;

    GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

    foreach (GameObject go in planets)
      attractors.Add(go.GetComponent<GravityAttractor>());

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

    if (Attractor != null) {
      Attractor.Attract(this);
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
  
  #region Planet Hopping
  
  public void CycleNextAttractor()
  {
    if (currentAttractorIndex == (attractors.Count-1))
      currentAttractorIndex = 0;
    else
      currentAttractorIndex++;
  }
  
  public GravityAttractor Attractor
  {
    get { return attractors[currentAttractorIndex]; }
    set { attractors.Add(value); currentAttractorIndex = attractors.IndexOf(value); }
  }
  
  #endregion
}
