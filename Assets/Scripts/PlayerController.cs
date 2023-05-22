using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 movedir;
    public Vector2 Initialmovedir;
    public LayerMask detectLayer;
    public Sprite cat_right;
    public Sprite cat_left;
    public Sprite cat_up;
    public Sprite cat_down;

   
    List<Vector2> Allmodir;
    List<Vector3> AllPosition;
    List<Box> boxes;
    
    bool isOk = false;

    private void Start()
    {
        Allmodir = new List<Vector2>();
        AllPosition = new List<Vector3>();
        boxes = new List<Box>();

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            movedir = Vector2.right;
         

        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movedir=Vector2.left;
         
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            movedir = Vector2.up;
            
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            movedir = Vector2.down;
          
        }
       
       

        if(movedir!=Vector2.zero)
        {
            if(CanMoveToDir(movedir))
            { ChangePlayerDir(movedir);
               Allmodir.Add(movedir);

              

                AllPosition.Add(transform.position);
               if(isOk==false) boxes.Add(null);
                Move(movedir);
             

            }
            Debug.Log(boxes.Count);
            Debug.Log(AllPosition.Count);
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {

                Back();
                
              
            }
        }

        movedir= Vector2.zero;
        isOk = false;
    }

    void Back()
    {
      
        
        int index = Allmodir.Count - 1 ;
        int i = AllPosition.Count - 1 ;

      
       
        if(index>=0&&i>=0)
        {
            transform.position = AllPosition[i ];
            ChangePlayerDir(Allmodir[index ]);

            Allmodir.RemoveAt(index );
            AllPosition.RemoveAt(i );

            if (boxes[i] != null)
            {
                boxes[i].Back();
                Debug.Log(boxes[i]);
                
            }
                boxes.RemoveAt(i);

        }

       

        Debug.Log(boxes.Count);
        Debug.Log(AllPosition.Count);

       
    }
    bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,1.0f,detectLayer);

        if (!hit)
        {
           
            return true;
        }
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
            {
                isOk = true;
               
                boxes.Add(hit.collider.GetComponent<Box>());

                return hit.collider.GetComponent<Box>().CanMoveToDir(dir);

            }
            else
            {
               
                return false;
            }
        }
    }
    private void Move(Vector2 dir)
    {
      
        transform.Translate(dir);
    }


    void ChangePlayerDir(Vector2 dir)
    {
        if (dir == Vector2.right)
        {
           
            GetComponent<SpriteRenderer>().sprite = cat_right;

        }
        else if (dir==Vector2.left)
        {
           
            GetComponent<SpriteRenderer>().sprite = cat_left;
        }
        else if (dir==Vector2.up)
        {
           
            GetComponent<SpriteRenderer>().sprite = cat_up;
        }
        else if (dir==Vector2.down)
        {
           
            GetComponent<SpriteRenderer>().sprite = cat_down;
        }
    }
}
