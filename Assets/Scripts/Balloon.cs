using UnityEngine;

public class Balloon : MonoBehaviour
{
    public delegate void BalloonPopped(GameObject balloon);
    public event BalloonPopped OnBalloonPopped;
    public float ylimit = -2.5f;

    public void Update()
    {
        /*if (gameObject.transform.position.y < ylimit)
        {
            Destroy(gameObject);
        }*/
    }

    private void OnDestroy()
    {
        if (OnBalloonPopped != null)
        {
            OnBalloonPopped(gameObject);
        }
    }

    // Add methods here for popping the balloon, like when hit by an arrow.
}

