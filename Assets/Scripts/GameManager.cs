using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Camera camera;
    public GameObject shelf;
    public GameObject lastLevel;
    public RoomScrolling roomScrolling;

    public GameObject player;
    public GameObject ground;
    public ItemShift itemShift;

    public GameObject firstStageObjects;
    public GameObject secondStageObjects;
    public GameObject thirdstageObjects;

    public GameObject cookie;

    public Vector3 cookiePosition;
    public Vector3 cookieRotation;
    public float cookieScale;

    public float lerpSpeed = 1;

    public static bool isPlayerAlive = true;

    private bool startedGame = false;
    private bool endedGame = false;

    private bool waiting = true;
    private float nextRetryTime;

    private Vector2 lastSavePos;

    private static GameManager gameManager;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if (waiting && Input.anyKey && !startedGame)
        {
            waiting = false;
            doGameStart();
        }
        else if (Input.GetKey(KeyCode.R) && nextRetryTime < Time.time && !endedGame)
        {
            nextRetryTime = Time.time + 0.5f;
            isPlayerAlive = true;
            player.transform.position = lastSavePos;
            player.GetComponent<CharacterHealth>().resetPlayer();

            camera.setToLastSavePoint(lastSavePos);
            roomScrolling.setLastSavePoint(lastSavePos);
        }
    }

    public static void playerDied()
    {
        isPlayerAlive = false;
    }

    public static void setLastSavePosition(Vector2 position)
    {
        gameManager.lastSavePos = position;
    }

    void doGameStart()
    {
        StartCoroutine(doCookieLerp());
    }

    IEnumerator doCookieLerp()
    {
        float lerpVal = 0;
        while (cookie.transform.position.x < cookiePosition.x)
        {
            lerpVal += lerpSpeed * Time.deltaTime;
            cookie.transform.position = Vector3.Lerp(cookie.transform.position, cookiePosition, lerpVal);
            cookie.transform.Rotate(0, lerpVal * cookieRotation.z, 0);
            cookie.transform.localScale = Vector3.Lerp(cookie.transform.localScale, new Vector3(cookieScale, cookieScale), lerpVal);

            yield return new WaitForEndOfFrame();
        }
    }

    public static void beginningShift()
    {
        BackgroundManager.switchBackground(BackgroundManager.BackgroundState.hell);
        gameManager.ground.transform.position = new Vector3(0, -2.4f, gameManager.ground.transform.position.z);
        gameManager.firstStageObjects.SetActive(false);
        gameManager.secondStageObjects.SetActive(true);

        gameManager.itemShift.doShift();
        PanelManager.hideText();
        //RoomScrolling.isScrolling = true;
        gameManager.startedGame = true;
    }

    public static void endingShift()
    {
        BackgroundManager.switchBackground(BackgroundManager.BackgroundState.normal);
        //RoomScrolling.isScrolling = false;
        //gameManager.thirdstageObjects.SetActive(true);
        Destroy(LavaWaves.getGameObj());

        gameManager.lastLevel.SetActive(false);

        gameManager.shelf.transform.position = gameManager.player.transform.position - new Vector3(3f, 0.2f, 0);
        gameManager.shelf.SetActive(true);
        Rigidbody2D rb = gameManager.shelf.GetComponentInChildren<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(15, 0));

        gameManager.ground.SetActive(false);

        gameManager.endedGame = true;
        playerDied();
    }
}
