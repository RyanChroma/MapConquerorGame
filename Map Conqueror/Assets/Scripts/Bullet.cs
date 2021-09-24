using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float currentDuration;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private LineRenderer line;
    private Color baseColor;

    // Start is called before the first frame update
    public void Setup(Color color, Vector3 from, Vector3 to)
    {
        line.startColor = color;
        line.endColor = color;
        baseColor = color;
        currentDuration = fadeDuration;
        line.SetPosition(0, from);
        line.SetPosition(1, to);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDuration > 0)
		{
            currentDuration -= Time.deltaTime;
		}
        if(currentDuration < 0)
		{
            currentDuration = 0;
            Destroy(this.gameObject);
		}
        baseColor.a = currentDuration / fadeDuration;
        line.startColor = baseColor;
        line.endColor = baseColor;
    }
}
