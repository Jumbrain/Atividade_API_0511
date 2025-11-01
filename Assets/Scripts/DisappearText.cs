using UnityEngine;

public class DisappearText : MonoBehaviour
{
    private float timer;
    private void OnEnable()
    {
        timer = 1.5f;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
