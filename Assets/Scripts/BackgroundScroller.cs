using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0f;
    public Renderer bgRend;
    public Renderer GameRoadRend;
    private float backgroundspeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //speed for cave background 0.02f, need to move it to a variable
        bgRend.material.mainTextureOffset += new Vector2(backgroundspeed * Time.deltaTime, 0);
        GameRoadRend.material.mainTextureOffset += new Vector2(ObstacleSpawnController.Instance.speedAmountToAdd * Time.deltaTime, 0) / 20;

        if (ObstacleSpawnController.Instance.speedAmountToAdd == 0)
        {
            backgroundspeed = 0f;
        }
    }

}
