using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutStone : MonoBehaviour
{
    const int white = 0;
    const int black = 1;

    int stoneColor = white;
    public GameObject blackStone, whiteStone;
    GameObject stone;
    int[][] direction = new int[8][]; //오목인지 확인하는 방향 위부터 시계방향
    int[][] occupied = new int[19][]; //위치에 바둑알 있는지 확인
    Boolean win = false;
    void Start()
    {
        setDirection();
        setOccupied();
        if (stoneColor == white)
            stone = whiteStone;
        else stone = blackStone;
    }

    void setDirection()
    {
        for (int i = 0; i < 8; i++)
        {
            direction[i] = new int[2];
        }

        direction[0][0] = 0;
        for (int i = 1; i < 4; i++)
            direction[i][0] = 1;
        direction[4][0] = 0;
        for (int i = 5; i < 8; i++)
            direction[i][0] = -1;

        for(int i = 0;i < 2; i++)
            direction[i][1] = 1;
        direction[2][1] = 0;
        for (int i = 3; i < 6; i++)
            direction[i][1] = -1;
        direction[6][1] = 0;
        direction[7][1] = 1;
    }

    void setOccupied()
    {
        for (int i = 0; i < 19; i++)
            occupied[i] = new int[19];
        for(int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
                occupied[i][j] = 0;
        }
    }

    void Update()
    {
        //클릭시 위치에 바둑알 둠
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            int x, y;
            x = getCoor(point.x);
            y = getCoor(point.y);
            if (occupied[x][y] == 0)
            {
                Instantiate(stone, new Vector3(getOrigCoor(x), getOrigCoor(y), 0), Quaternion.identity);
                occupied[x][y] = stoneColor;
            }
        }
    }

    int getCoor(float x) // 클릭 좌표->occupied 행렬 좌표
    {
        x *= 2;
        x += 9.5f;
        return (int)x;
    }

    float getOrigCoor(float x) // occupied 행렬 좌표->화면 바둑판 좌표
    {
        x -= 9;
        x /= 2;
        return x;
    }

    void searchLine5(int x, int y)
    {
        int checkX = x, checkY = y;
        for(int i = 0;i < 8;i++)
        {
            for(int j = 0;j < 5; j++)
            {
                checkX += direction[i][0];
                checkY += direction[i][1];
                if (occupied[checkX][checkY]!=stoneColor)
                    break;
                if (j == 4)
                {
                    win = true;
                    return;
                }
            }
            checkX = x;
            checkY = y;
        }
    }
}
