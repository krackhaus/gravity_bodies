using UnityEngine;
using System.Collections;

/**
 * Cyclically enumerates over the given collection.
 */
public class Cycler
{
  /**
   * Enumerator.
   */
  private IEnumerator enumerator;

  /**
   * Constructor.
   */
  public Cycler(ICollection collection)
  {
    enumerator = collection.GetEnumerator();
  }

  /**
   * Returns the next object in the enumerator and cycles back to the
   * beginning if the end is reached.
   */
  public object Next()
  {
    if (!enumerator.MoveNext()) {
      enumerator.Reset();
      enumerator.MoveNext();
    }

    return enumerator.Current;
  }
}
