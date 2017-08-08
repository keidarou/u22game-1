using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class automaticgenerator : MonoBehaviour
{

    //------------------変数-------------------------//
    public int[,] map = new int[50, 50];//x座標、y座標
    bool[,] kakutei = new bool[50, 50];
    public int nowx, nowy;
    public int ippen;//一辺の長さ 
    public int magarukaisuu;
    public int previous = 0;//0は下向き、1は右向き、２は下向き、３は左向き
                            //-------------------------------------------------//
                            // Use this for initialization

    bool check(int x, int y, int range, int movex, int movey)
    {
        for (int i = 0; i < range; i++)//ゴールにたどり着くまで
        {
            if (map[y + movey, x + movex] == 1 && kakutei[y + movey, x + movex] == true)
            {
                return false;
            }
            x += movex; y += movey;
        }
        if (map[y + movey, x + movex] == 0 && kakutei[y + movey, x + movex] == true) { return false; }
        map[y + movey, x + movex] = 1; kakutei[y + movey, x + movex] = true;

        for (int i = 0; i < range; i++)
        {
            map[y, x] = 0; kakutei[y, x] = true;
            x -= movex; y -= movey;
        }
        return true;
    }

    void Awake()
    {
        kakutei[ippen, ippen - 2] = true; kakutei[ippen - 1, ippen - 2] = true; kakutei[ippen - 2, ippen - 2] = true; kakutei[ippen, ippen + 2] = true; kakutei[ippen + 1, ippen + 2] = true; kakutei[ippen + 2, ippen + 2] = true;
        map[ippen, ippen - 2] = 1; map[ippen - 1, ippen - 2] = 1; map[ippen - 2, ippen - 2] = 1; map[ippen, ippen + 2] = 1; map[ippen + 1, ippen + 2] = 1; map[ippen + 2, ippen + 2] = 1;
        for (int i = 0; i < ippen ; i++)
        {
            map[0, i] = 1; map[i, 0] = 1; map[ippen , i] = 1; map[i, ippen ] = 1;
            kakutei[0, i] = true; kakutei[i, 0] = true; kakutei[ippen , i] = true; kakutei[i, ippen ] = true;
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
                if (muki == (previous + 2) % 4) { continue; }previous = muki;

                if (muki == 0)//した 
                {
                    range = Random.Range(0, 15 - nowy);//壁を作る一歩手前で止まる
                    if (range % 2 == 1) { continue; }//もし距離が奇数なら
                    if (!check(nowx, nowy, range, 0, 1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy + range, nowx - 1 + i] = 1;//三マス塗る！
                        kakutei[nowy + range, nowx - 1 + i] = true;//
                    }
                    //----------------------------------------------//
                    if (kakutei[nowy - 1, nowx] == false)
                    {
                        while (true)
                        {
                            if (nowy == 1) { continue; }
                            hantairange = Random.Range(2, nowy);//ここ分からん！
                            if (hantairange % 2 == 0) { break; }
                        }
                        if (kakutei[nowy - hantairange, nowx] == false)//
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy - hantairange, nowx - 1 + i]) { continue; }
                                map[nowy - hantairange, nowx - 1 + i] = 1;//
                            }
                        }
                    }
                    nowy += range;//
                }
                if (muki == 1)
                {
                    range = Random.Range(0, 15 - nowx);//直す
                    if (range % 2 == 1) { continue; }
                    if (!check(nowy, nowx, range, 1, 0)) { continue; }//直す

                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy - 1 + i, nowx + range] = 1;//直す
                        kakutei[nowy - 1 + i, nowx + range] = true;//直す
                    }
                    //----------------------------------------------//
                    if (kakutei[nowy, nowx - 1] == false)//直す
                    {
                        while (true)
                        {
                            if (nowx == 1) { break; }//直す
                            hantairange = Random.Range(2, nowx);//直す
                            if (hantairange % 2 == 0) { break; }
                        }
                        if (kakutei[nowy, nowx - hantairange] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy - 1 + i, nowx - hantairange]) { continue; }//直す
                                map[nowy - 1 + i, nowx - hantairange] = 1;//直す
                            }
                        }
                    }
                    nowx += range;//直す
                }
                if (muki == 2)//上
                {
                    range = Random.Range(0, nowy);
                    if (range % 2 == 1) { continue; }
                    if (!check(nowy, nowx, range, 0, -1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy - range, nowx - 1 + i] = 1;//直す
                        kakutei[nowy - range, nowx - 1 + i] = true;//直す
                    }
                    //----------------------------------------------//
                    if (kakutei[nowy + 1, nowx] == false)//直す
                    {
                        while (true)
                        {
                            if (nowy == 15) { break; }//直す
                            hantairange = Random.Range(2, 15 - nowy);//直す
                            if (hantairange % 2 == 0) { break; }
                        }
                        if (kakutei[nowy + hantairange, nowx] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy + hantairange, nowx - 1 + i]) { continue; }//直す
                                map[nowy + hantairange, nowx - 1 + i] = 1;//直す
                            }
                        }
                    }
                    nowy -= range;
                }
                if (muki == 3)
                {
                    range = Random.Range(0,  nowx);//直す
                    if (range % 2 == 1) { continue; }
                    if (!check(nowy, nowx, range, -1, 0)) { continue; }//直す

                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy - 1 + i, nowx - range] = 1;//直す
                        kakutei[nowy - 1 + i, nowx - range] = true;//直す
                    }
                    //----------------------------------------------//
                    if (kakutei[nowy, nowx + 1] == false)//直す
                    {
                        while (true)
                        {
                            if (nowx == 15) { break; }//直す
                            hantairange = Random.Range(2, 15-nowx);//直す
                            if (hantairange % 2 == 0) { break; }
                        }
                        if (kakutei[nowy, nowx + hantairange] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy - 1 + i, nowx + hantairange]) { continue; }//直す
                                map[nowy - 1 + i, nowx + hantairange] = 1;//直す
                            }
                        }
                    }
                    nowx -= range;//直す
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
