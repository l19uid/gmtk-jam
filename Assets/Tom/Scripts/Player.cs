using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    public bool isGrounded = false;

    public Vector2 input;
    public Vector2 velocity;
    
    public bool isResizing = false;
    // Start is called before the first frame update
    public string resizeDirection = "none";
    public GameObject head;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    
    public Vector2 moveToPosition = new Vector2(0, 0);
    public bool isDefaultSize = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isResizing = Input.GetKey(KeyCode.Space);
        if(isResizing)
            Resize();
        else
        {
            RevertSize();
            if (!isDefaultSize)
                return;
            input = new Vector2(Input.GetAxis("Horizontal"), 0);
        
            velocity = input * speed;
            Vector2 rbVelocity = rb.velocity;
            rbVelocity.x = velocity.x;
            rb.velocity = rbVelocity;
        }
    }

    public void RevertSize()
    {
        // lerps the head position sprite size collider size and collider center back to original size
        
        if(head.transform.position == new Vector3(0, 0, 0) 
           && spriteRenderer.size == new Vector2(1, 2) 
           && boxCollider2D.size == new Vector2(1.05f, 2.05f) 
           && boxCollider2D.offset == new Vector2(0, 0))
        {
            isDefaultSize = true;
            return;
        }
        isDefaultSize = false;
        
        var position = head.transform.localPosition;
        position = Vector3.Lerp(position, new Vector3(0, 0.5f, 0), 12 * Time.deltaTime);
        head.transform.localPosition = position;
        
        var size = spriteRenderer.size;
        size = Vector2.Lerp(size, new Vector2(1, 2), 12 * Time.deltaTime);
        spriteRenderer.size = size;
        
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = Vector3.Lerp(spritePosition, new Vector3(0, 0, 0), 12 * Time.deltaTime);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = Vector2.Lerp(colliderSize, new Vector2(1.05f, 2.05f), 12 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = Vector2.Lerp(colliderCenter, new Vector2(0, 0), 12 * Time.deltaTime);
        boxCollider2D.offset = colliderCenter;
        
        var playerPosition = transform.position;
        playerPosition = Vector2.Lerp(playerPosition,  moveToPosition, 12 * Time.deltaTime);
        transform.position = playerPosition;
    }


    public void Resize()
    {
        if (Input.GetKey(KeyCode.W) && spriteRenderer.size.y < 8 && (resizeDirection == "none" || resizeDirection == "up"))
        {
            if (transform.localScale.y <= 2)
            {
                resizeDirection = "none";
            }
            else
            {
                resizeDirection = "up";
            }
            
            UpsizeUp();
        }
        else if (Input.GetKey(KeyCode.S) && resizeDirection == "up")
        {
            DownsizeUp();
        }
        else if (Input.GetKey(KeyCode.S) && spriteRenderer.size.y > 8 && (resizeDirection == "none" ||  resizeDirection == "down"))
        {
            if (transform.localScale.y <= 2)
            {
                resizeDirection = "none";
            }
            else
            {
                resizeDirection = "down";
            }
            
            UpsizeDown();
        }
        else if (Input.GetKey(KeyCode.A) && spriteRenderer.size.x < 8 && (resizeDirection == "none" || resizeDirection == "left"))
        {
            if (transform.localScale.x <= 1)
            {
                resizeDirection = "none";
            }
            else
            {
                resizeDirection = "left";
            }
            
            UpsizeLeft();
        }
        else if (Input.GetKey(KeyCode.D) && spriteRenderer.size.x < 8 && (resizeDirection == "left"))
        {
            DownsizeLeft();
        }
        else if (Input.GetKey(KeyCode.D) && spriteRenderer.size.x < 8&& (resizeDirection == "none" || resizeDirection == "right"))
        {
            if (transform.localScale.x <= 1)
            {
                resizeDirection = "none";
            }
            else
            {
                resizeDirection = "right";
            }
            
            UpsizeRight();
        }
        else if (Input.GetKey(KeyCode.A) && spriteRenderer.size.x < 8 && (resizeDirection == "right"))
        {
            DownsizeRight();
        }
        moveToPosition = head.transform.position;
    }
    
    public void UpsizeLeft()
    {
        var position = head.transform.position;
        position = new Vector3(position.x - 4.5f * Time.deltaTime, position.y, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x + 3 * Time.deltaTime, size.y);
        if (size.x > 2.1)
        {
            size.y = Mathf.Lerp(size.y, 1, 6 * Time.deltaTime);
        }
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x - 3 * Time.deltaTime, spritePosition.y, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x + 3 * Time.deltaTime, colliderSize.y);
        if(colliderSize.x > 2.1)
            colliderSize.y = Mathf.Lerp(colliderSize.y, 1, 6 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x - 3 * Time.deltaTime, colliderCenter.y);
        boxCollider2D.offset = colliderCenter;
    }
    
    public void DownsizeLeft()
    {
        var position = head.transform.position;
        position = new Vector3(position.x + 4.5f * Time.deltaTime, position.y, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x - 3 * Time.deltaTime, size.y);
        if (size.x > 2.1)
        {
            size.y = Mathf.Lerp(size.y, 1, 6 * Time.deltaTime);
        }
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x + 3 * Time.deltaTime, spritePosition.y, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x - 3 * Time.deltaTime, colliderSize.y);
        if(colliderSize.x > 2.1)
            colliderSize.y = Mathf.Lerp(colliderSize.y, 1, 6 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x + 3 * Time.deltaTime, colliderCenter.y);
        boxCollider2D.offset = colliderCenter;
    }
    
    public void UpsizeRight()
    {
        var position = head.transform.position;
        position = new Vector3(position.x + 4.5f * Time.deltaTime, position.y, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x + 6 * Time.deltaTime, size.y);
        if (size.x > 2.1)
            size.y = Mathf.Lerp(size.y, 1, 6 * Time.deltaTime);
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x + 3 * Time.deltaTime, spritePosition.y, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x + 6 * Time.deltaTime, colliderSize.y);
        if(colliderSize.x > 2.1)
            colliderSize.y = Mathf.Lerp(colliderSize.y, 1, 6 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x + 6 * Time.deltaTime, colliderCenter.y);
        boxCollider2D.offset = colliderCenter;
    }

    public void DownsizeRight()
    {
        var position = head.transform.position;
        position = new Vector3(position.x - 4.5f * Time.deltaTime, position.y, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x - 3 * Time.deltaTime, size.y);
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x - 3 * Time.deltaTime, spritePosition.y, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x - 3 * Time.deltaTime, colliderSize.y);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x - 3 * Time.deltaTime, colliderCenter.y);
        boxCollider2D.offset = colliderCenter;
    }
    
    public void UpsizeUp()
    {
        var position = head.transform.position;
        position = new Vector3(position.x, position.y + 4.5f * Time.deltaTime, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x, size.y + 3 * Time.deltaTime);
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x, spritePosition.y + 3 * Time.deltaTime, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x, colliderSize.y + 3 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x, colliderCenter.y + 3 * Time.deltaTime);
        boxCollider2D.offset = colliderCenter;
    }
    
    public void DownsizeUp()
    {
        var position = head.transform.position;
        position = new Vector3(position.x, position.y - 4.5f * Time.deltaTime, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x, size.y - 3 * Time.deltaTime);
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x, spritePosition.y - 3 * Time.deltaTime, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x, colliderSize.y - 3 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x, colliderCenter.y - 3 * Time.deltaTime);
        boxCollider2D.offset = colliderCenter;
    }
    
    public void UpsizeDown()
    {
        var position = head.transform.position;
        position = new Vector3(position.x, position.y - 4.5f * Time.deltaTime, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x, size.y - 3 * Time.deltaTime);
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x, spritePosition.y - 3 * Time.deltaTime, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x, colliderSize.y - 3 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x, colliderCenter.y - 3 * Time.deltaTime);
        boxCollider2D.offset = colliderCenter;
    }
    
    public void DownsizeDown()
    {
        var position = head.transform.position;
        position = new Vector3(position.x, position.y + 4.5f * Time.deltaTime, position.z);
        head.transform.position = position;
            
        var size = spriteRenderer.size;
        size =  new Vector2(size.x, size.y + 3 * Time.deltaTime);
        spriteRenderer.size = size;
        var spritePosition = spriteRenderer.transform.localPosition;
        spritePosition = new Vector3(spritePosition.x, spritePosition.y + 3 * Time.deltaTime, spritePosition.z);
        spriteRenderer.transform.localPosition = spritePosition;
        
        var colliderSize = boxCollider2D.size;
        colliderSize = new Vector2(colliderSize.x, colliderSize.y + 3 * Time.deltaTime);
        boxCollider2D.size = colliderSize;
        var colliderCenter = boxCollider2D.offset;
        colliderCenter = new Vector2(colliderCenter.x, colliderCenter.y + 3 * Time.deltaTime);
        boxCollider2D.offset = colliderCenter;
    }
}
