using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Color finishColor;
    Color originColor;
    public List<Vector3> AllPosition;
    

    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
        FindObjectOfType<GameManager>().totalBoxes++;
        AllPosition = new List<Vector3>();
        AllPosition.Add(transform.position);
    }
   public bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+(Vector3)dir,dir,0.2f);
        if (!hit)
        {
            AllPosition.Add(transform.position);
            transform.Translate(dir);

           
            return true;
        }
        else return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Target"))
        {
            GetComponent<SpriteRenderer>().color = finishColor;
            FindObjectOfType<GameManager>().finishedBoxes++;

            FindObjectOfType<GameManager>().CheckFinish();

        }
    }
    public void Back()
    {
       
       
        int i = AllPosition.Count - 1 ;
      
        if (i>=0)
        {
            transform.position = AllPosition[i ];
            AllPosition.RemoveAt(i);
        }
       
        
       
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            GetComponent<SpriteRenderer>().color = originColor;
            FindObjectOfType<GameManager>().finishedBoxes--;

        }
    }


}
