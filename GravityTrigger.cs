using UnityEngine;

class GravityTrigger : MonoBehaviour
{
  /**
   * The attractor found within this object.
   */
  GravityAttractor attractor;

  /**
   * Finds the first attractor within this object.
   */
  void Awake()
  {
    attractor = GetComponentInChildren<GravityAttractor>();
  }

  /**
   * If the game object that enters the trigger contains a GravityBody, the
   * first GravityAttractor component found in this object is assigned to it.
   */
  void OnTriggerEnter(Collider other)
  {
    if (attractor != null) {
      GravityBody body = other.gameObject.GetComponent<GravityBody>();

      if (body != null) {
        body.attractor = attractor;
      }
    }
  }
}
