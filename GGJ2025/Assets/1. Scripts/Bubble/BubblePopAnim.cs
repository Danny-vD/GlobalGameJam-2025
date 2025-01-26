using UnityEngine;

public class BubblePopAnim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BubblePopParticles()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
