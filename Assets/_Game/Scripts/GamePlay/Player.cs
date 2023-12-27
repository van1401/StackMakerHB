using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direct 
{ Forward, 
  Back, 
  Right, 
  Left,
  None,
}


public class Player : MonoBehaviour
{
    public LayerMask layerBrick;
    [SerializeField] float speed;


    public Transform playerBrickPrefab;
    public Transform playerSkin;
    public Transform brickHolder;

    private Vector3 mouseDown, mouseUp;
    private bool isMoving;
    private bool isControl;
    private Vector3 moveNextPoint;
    private List<Transform> playerBricks = new List<Transform>();



    public void OnInit()
    {
        isMoving = false;
        isControl = false;
        ClearBrick();
        playerSkin.localPosition = Vector3.zero; 

    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay) && !isMoving)
        {
            if (Input.GetMouseButtonDown(0) && !isControl)
            {
                isControl = true;
                mouseDown = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0) && isControl)
            {
                isControl = false;
                mouseUp = Input.mousePosition;
                moveNextPoint = GetNextPoint(GetDirect(mouseDown, mouseUp));

                Direct direct = GetDirect(mouseDown, mouseUp);
                if (direct != Direct.None)
                {
                    moveNextPoint = GetNextPoint(direct);
                    isMoving = true;
                }
            }
        }
        else if (isMoving)
        {
            if (Vector3.Distance(transform.position, moveNextPoint) < 0.1f)
            {
                isMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, moveNextPoint, Time.deltaTime * speed);          
        }

    }



    private Direct GetDirect(Vector3 mouseDown, Vector3 mouseUp)
    {
        Direct direct = Direct.None;
        
        float deltaX = mouseUp.x - mouseDown.x;
        float deltaY = mouseUp.y - mouseDown.y; 
        
        if(Vector3.Distance(mouseDown, mouseUp) < 100)
        {
            direct = Direct.None;
        }
        else
        {
            if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                //swipe up and down
                if (deltaY > 0)
                {
                    direct = Direct.Forward;
                }
                else
                {
                    direct = Direct.Back;
                }
            }
            else
            {
                //swipe right and left
                if (deltaX > 0)
                {
                    direct = Direct.Right;
                }
                else
                {
                    direct = Direct.Left;
                }
            }
        }
        return direct;
    }

    private Vector3 GetNextPoint(Direct direct)
    {
        RaycastHit hit;
        Vector3 nextPoint = transform.position;
        Vector3 dir = Vector3.zero;
        
        switch(direct)
        {
            case Direct.Forward:
                dir = Vector3.forward;
                break;
            case Direct.Back:
                dir = Vector3.back;
                break;
            case Direct.Right:
                dir = Vector3.right;
                break;
            case Direct.Left:
                dir = Vector3.left;
                break;
            case Direct.None:
                break;
            default:
                break;
        }

        for (int i = 0; i < 100; i++)
        {
            if (Physics.Raycast(transform.position + dir * i + Vector3.up *2, Vector3.down, out hit, 10f, layerBrick))
            {
                nextPoint = hit.collider.transform.position;
            }
            else
            {
                break;
            }
        }
        return nextPoint;
    }

    public void AddBrick()
    {
        int index = playerBricks.Count;

        Transform playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.localPosition = Vector3.down + index * 0.25f * Vector3.up;

        playerBricks.Add(playerBrick);

        playerSkin.localPosition = playerSkin.localPosition + Vector3.up * 0.25f;

    }
    public void RemoveBrick() 
    {
        int index = playerBricks.Count - 1;

        if (index >= 0) 
        {
            Transform playerBrick = playerBricks[index];
            playerBricks.Remove(playerBrick);
            Destroy(playerBrick.gameObject);

            playerSkin.localPosition = playerSkin.localPosition - Vector3.up * 0.25f;
        }

    }


    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++) 
        {
            Destroy(playerBricks[i].gameObject);
            playerSkin.localPosition = Vector3.zero;
        }
        playerBricks.Clear();
    }
}
