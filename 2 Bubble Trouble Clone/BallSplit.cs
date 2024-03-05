using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplit : MonoBehaviour
{
    [SerializeField] BallTypeSO[] ballsV2;

    public static BallSplit Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void createSplittedBalls(Transform pos, string ballType, int ballSizeValue)
    {
        CutFruitParticle.Instance.createCutParticle(ballType, ballSizeValue, pos);
        if (ballSizeValue != 0)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                Vector2 createPos = new Vector2(pos.position.x + (i * 0.5f), pos.position.y);
                BallController newBall = Instantiate(ballsV2[ballSizeValue - 1].getBall(), createPos, pos.rotation, pos.parent.transform).GetComponent<BallController>();
                newBall.gameObject.SetActive(true);
                newBall.addForceInSplit(i);
            }
        }
        else
        {
            if(GameObject.FindGameObjectsWithTag("Ball").Length == 1){
                Debug.Log("WELL DONE!!!!!!!"); // Win
                LevelCompleted.Instance.levelCompleted();

            }
        }
    }
}
