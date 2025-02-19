﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collect3 : MonoBehaviour
{
    // [Head, Body1, Body2, ...]
    private static LinkedList<Direction> snakePosition = new LinkedList<Direction>();
    public GameObject pomme;
    protected const float SNAKE_SPEED = 0.4f;

    //public List<GameObject> pommes;
    public static Collect3 body;
    private double x;
    private double y;
    public static int MAX_X = 8;
    public static int MAX_Y = 8;
    public Direction direction;
    public Direction precDirection;
    public Boolean hasChangedDirection;
    SpriteRenderer spriteRenderer;
    GameObject boutton_up;
    GameObject boutton_down;
    GameObject boutton_right;
    GameObject boutton_left;
    public static int maxPommes = 7;
    public static int nbPommes;
    public Sprite pomme1, pomme2, pomme3, pomme4, pomme5, pomme6, pomme7;
    public static bool start;

    public enum Direction
    {
        LEFT,
        UP,
        RIGHT,
        DOWN,
    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (name == "snake_head")
        {
            body = this;
            direction = Direction.RIGHT;
            precDirection = Direction.RIGHT;
            hasChangedDirection = false;
        }
    }

    private void Move()
    {

        x = 0.0;
        y = 0.0;
        if (hasChangedDirection)
        {
            int r = 0;
            if (body.precDirection == Direction.RIGHT && body.direction == Direction.UP)
            {
                r = 90;
            }
            if (body.precDirection == Direction.RIGHT && body.direction == Direction.DOWN)
            {
                r = -90;
            }
            if (body.precDirection == Direction.UP && body.direction == Direction.RIGHT)
            {
                r = -90;
            }
            if (body.precDirection == Direction.UP && body.direction == Direction.LEFT)
            {
                r = 90;
            }
            if (body.precDirection == Direction.LEFT && body.direction == Direction.UP)
            {
                r = -90;
            }
            if (body.precDirection == Direction.LEFT && body.direction == Direction.DOWN)
            {
                r = 90;
            }
            if (body.precDirection == Direction.DOWN && body.direction == Direction.RIGHT)
            {
                r = 90;
            }
            if (body.precDirection == Direction.DOWN && body.direction == Direction.LEFT)
            {
                r = -90;
            }
            body.transform.Rotate(0, 0, r);
            body.hasChangedDirection = false;
            body.precDirection = body.direction;

        }
        // translation
        switch (body.direction)
        {
            case Direction.LEFT:
                x -= 1;
                break;
            case Direction.UP:
                y += 1;
                break;
            case Direction.DOWN:
                y -= 1;
                break;
            case Direction.RIGHT:
                x += 1;
                break;
        }
        body.transform.position += new Vector3(
           (float)x * SNAKE_SPEED * Time.deltaTime,
           (float)y * SNAKE_SPEED * Time.deltaTime,
           0
        );


    }

    private void Update()
    {
        if (start)
        {
            if (name.StartsWith("apple"))
            {
                switch (nbPommes)
                {
                    case 1:
                        spriteRenderer.sprite = pomme1;
                        break;
                    case 2:
                        spriteRenderer.sprite = pomme2;
                        break;
                    case 3:
                        spriteRenderer.sprite = pomme3;
                        break;
                    case 4:
                        spriteRenderer.sprite = pomme4;
                        break;
                    case 5:
                        spriteRenderer.sprite = pomme5;
                        break;
                    case 6:
                        spriteRenderer.sprite = pomme6;
                        break;
                    case 7:
                        spriteRenderer.sprite = pomme7;
                        break;
                }
            }
            Move();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (body.direction != Direction.DOWN)
            {
                body.direction = Direction.UP;
                body.hasChangedDirection = true;
                Debug.Log("change direction");
                if (!start)
                    start = true;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (body.direction != Direction.UP)
            {
                body.direction = Direction.DOWN;
                body.hasChangedDirection = true;
                Debug.Log("change direction");
                if (!start)
                    start = true;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (body.direction != Direction.RIGHT)
            {
                body.direction = Direction.LEFT;
                body.hasChangedDirection = true;
                Debug.Log("change direction");
                if (!start)
                    start = true;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (body.direction != Direction.LEFT)
            {
                body.direction = Direction.RIGHT;
                body.hasChangedDirection = true;
                Debug.Log("change direction");
                if (!start)
                    start = true;
            }
        }
    }



    private void OnMouseDown()
    {

        switch (this.name)
        {
            case "Button up":
                if (body.direction != Direction.DOWN)
                    body.direction = Direction.UP;
                break;
            case "Button down":
                if (body.direction != Direction.UP)
                    body.direction = Direction.DOWN;
                break;
            case "Button left":
                if (body.direction != Direction.RIGHT)
                    body.direction = Direction.LEFT;
                break;
            case "Button right":
                if (body.direction != Direction.LEFT)
                    body.direction = Direction.RIGHT;
                break;
        }
        if (!start)
            start = true;
        body.hasChangedDirection = true;
        Debug.Log("change direction");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (name.StartsWith("snake"))
        {
            if (collision.gameObject.name.StartsWith("mur"))
            {
                Debug.Log("Snake se prend un mur !");
                Debug.Log("Défaite");
                SceneManager.LoadScene("RamboACAABB");
            }
            else
            {
                nbPommes++;
                if (nbPommes == maxPommes)
                {
                    Debug.Log("Victoire");
                    SceneManager.LoadScene("PilotageACAABA");
                }
                pomme.transform.position = new Vector3(
                    UnityEngine.Random.Range(-3.25f, 3.25f),
                    UnityEngine.Random.Range(-3.25f, 3.25f),
                    0
                );
                double x = pomme.transform.position.x;
                double y = pomme.transform.position.y;
                while (x > -2.66 && x < 2.63 && y < 0.5 && y > -0.54)
                {
                    pomme.transform.position = new Vector3(
                        UnityEngine.Random.Range(-3.25f, 3.25f),
                        UnityEngine.Random.Range(-3.25f, 3.25f),
                        0
                    );
                    x = pomme.transform.position.x;
                    y = pomme.transform.position.y;
                }
            }
        }
    }
}
