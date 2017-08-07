using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class automaticgenerator : MonoBehaviour
{

    //------------------変数-------------------------//
    int[,] map = new int[50, 50];//x座標、y座標
    bool[,] kakutei = new bool[50, 50];
    public int nowx, nowy;
    public int ippen;//一辺の長さ 
    public int magarukaisuu;
    public int previous = 0;//0は下向き、1は右向き、２は下向き、３は左向き
                            //-------------------------------------------------//
                            // Use this for initialization

    bool check(int x, int y, int range, int movex, int movey)
    {
        for (int i = 0; i < range; i++)
        {
            if (map[y, x] == 1 && kakutei[y, x] == true)
            {
                return false;
            }
            x += movex; y += movey;
        }
        if (map[y, x] == 0 && kakutei[y,x] == true) { return false; }
        map[y, x] = 1; kakutei[y, x] = true;

        for (int i = 0; i < range; i++)
        {
            x -= movex; y -= movey;
            map[y, x] = 0; kakutei[y, x] = true;
        }
        return true;
    }

    void Start()
    {
        kakutei[ippen, ippen - 2] = true; kakutei[ippen - 1, ippen - 2] = true; kakutei[ippen - 2, ippen - 2] = true; kakutei[ippen, ippen + 2] = true; kakutei[ippen + 1, ippen + 2] = true; kakutei[ippen + 2, ippen + 2] = true;
        map[ippen, ippen - 2] = 1; map[ippen - 1, ippen - 2] = 1; map[ippen - 2, ippen - 2] = 1; map[ippen, ippen + 2] = 1; map[ippen + 1, ippen + 2] = 1; map[ippen + 2, ippen + 2] = 1;
        for (int i = 0; i < ippen * 2 + 1; i++)
        {
            map[0, i] = 1; map[i, 0] = 1; map[ippen * 2, i] = 1; map[i, ippen * 2] = 1;
            kakutei[0, i] = true; kakutei[i, 0] = true; kakutei[ippen * 2, i] = true; kakutei[i, ippen * 2] = true;
        }

        for (int nannkaime = 0; nannkaime < magarukaisuu; nannkaime++)
        {
            int muki, range = 0, hantairange = 0;
            //重力通りに動く玉から
            while (true)
            {
                //0は下向き、1は右向き、２は上向き、３は左向き
                muki = Random.Range(0, 4);
                if (muki == previous) { continue; }
                if (muki == (previous + 2) % 4) { continue; }

                if (muki == 0)//した
                {
                    range = Random.Range(0, nowy);//壁を作る一歩手前で止まる
                    if (range % 2 == 1) { continue; }
                    if (!check(nowx, nowy, range, 0, 1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[nowx - 1 + i, nowy - range] = 1;//
                        kakutei[nowx - 1 + i, nowy - range] = true;//
                    }
                    //----------------------------------------------//
                    if (kakutei[nowx, nowy + 1] == false)
                    {
                        while (true)
                        {
                            hantairange = Random.Range(2, 16 - nowy);
                            if (hantairange % 2 == 1) { break; }
                        }
                        if (kakutei[nowx, nowy + hantairange] == false)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                map[nowx - 1 + i, nowy + hantairange] = 1;//
                            }
                        }
                    }
                    nowy -= range;
                }
                if (muki == 1)
                {
                    range = Random.Range(0, 15 - nowx);
                    if (range % 2 == 1) { continue; }
                    if (!check(nowx, nowy, range, -1, 0)) { continue; }

                    for (int i = 0; i < 3; i++)
                    {
                        map[nowx +range, nowy - 1+i] = 1;//
                        kakutei[nowx + range, nowy - 1 + i] = true;////
                    }
                    //----------------------------------------------//
                    if (kakutei[nowx, nowy + 1] == false)
                    {
                        while (true)
                        {
                            hantairange = Random.Range(2, 16 - nowy);
                            if (hantairange % 2 == 1) { break; }
                        }
                        if (kakutei[nowx, nowy + hantairange] == false)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                map[nowx - 1 + i, nowy + hantairange] = 1;//
                            }
                        }
                    }
                    nowx += range;
                }
                if (muki == 2)
                {
                    range = Random.Range(0, 15 - nowy);
                    if (range % 2 == 1) { continue; }
                    if (!check(nowx, nowy, range, 0, -1)) { continue; }
                    nowy += range;
                }
                if (muki == 3)
                {
                    range = Random.Range(0, nowx);
                    if (range % 2 == 1) { continue; }
                    if (!check(nowx, nowy, range, 1, 0)) { continue; }
                    nowx -= range;
                }


                break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
