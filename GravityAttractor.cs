using UnityEngine;

public class GravityAttractor : MonoBehaviour {

  /**
   * The amount of gravity.
   */
  public float gravity = 10.0f;

  /**
   * The rate at which to upright rotationaly frozen objects.
   */
  public float uprightBodiesRate = 1.0f;

  /**
   * Whether or not to automatically bind to all gravity bodies in the scene.
   */
  public bool attractAllBodies = true;

  /**
   * Applies attractive forces upon and uprights the given GravityBody.
   */
  public void Attract(GravityBody body) {

    body.AddDownwardForce(gravity);

    if (body.rigidbody.freezeRotation) {
      body.UprightPosition(uprightBodiesRate);
    }
  }

  /**
   * Binds to all gravity bodies when the scene starts.
   */
  private void Start() {
    if (attractAllBodies) {
      foreach (GravityBody body in FindObjectsOfType(typeof(GravityBody))) {
        body.Attractor = this;
      }
    }
  }
}
