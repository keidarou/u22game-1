using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetheball : MonoBehaviour
{

    //--------------------変数-------------------//
    bool idouchuu = true;//移動中ならfalse
    Transform mokutekidown, nowdown, mokutekiup, nowup;//mokutekiは行くべき場所、nowは今の位置,upは上向きのボール、downは下向きのボール
    int nowrotation;//今のスマホの回転度
    GetAcc acc;//どれくらい回転しているかをみるため
    public GameObject mapgenerator, balldown, ballup;//それぞれのゲームオブジェクト、分からなければ連絡よろ
    int[,] map = new int[50, 50];//マップ
    int down, up;//downは重力に従って落ちるボールが何マス落ちるか、upはその逆
    public int nowx, nowy;//グリッド表示での現在の座標
    public float haba;//移動する量、つまり一マスの間隔
    //-------------------------------------------------
    //内容としては、玉が動き終わって、スマホの方向、位置が与えられたとき、どの方向に何マス動くかというのを返します。
    //返すといっても変数の中に格納しておくだけです。返り値はないです。

    int Selectrange(int x, int y, int genzaix, int genzaiy)//このx,yはx方向にどれだけ、y方向も同様なので、通常x,y=1,0・0,1・-1,0・0,-1
    {
        int count = 0;
        while (true)
        {
            if (map[genzaix + x, genzaiy + y] == 1)
            {
                return count;//壁に当たったら、何マス行けたかを返す
            }
            genzaix += x; genzaiy += y; count++;
        }
    }

    void selectDirectionandrange(int Direction, int x, int y)//このx,yはgenzaix,genzaiyの意味です。すみません
    {
        //方向を把握していないので、適当に0を上、1を右、2を下、3を左にします
        if (Direction == 2)//上向きの重力
        {
            up = Selectrange(0, -1, x, y);
            down = Selectrange(0, 1, x, y);
        }
        else if (Direction == 1)//右
        {
            up = Selectrange(-1, 0, x, y);
            down = Selectrange(1, 0, x, y);
        }
        else if (Direction == 0)//下
        {
            up = Selectrange(0, 1, x, y);
            down = Selectrange(0, -1, x, y);
        }
        else//左
        {
            up = Selectrange(1, 0, x, y);
            down = Selectrange(-1, 0, x, y);
        }
        //これで代入おーわり。あとはこれに従っていどうして
    }

    bool ballmove()//玉を動かすぞい
    {
        nowdown.position = balldown.transform.position;//ダウンボールの位置を代入
        nowup.position = ballup.transform.position;//アップボールの位置を代入
        int movexhoukou = 0, moveyhoukou = 0;//下向きがどの方向に行くか、つまり、x=1,y=0で右方向に１進むみたいな
        if (nowrotation == 0) { movexhoukou = 0; moveyhoukou = -1; }//もし、下向きなら
        if (nowrotation == 1) { movexhoukou = 1; moveyhoukou = 0; }//右
        if (nowrotation == 2) { movexhoukou = 0; moveyhoukou = 1; }//上
        if (nowrotation == 3) { movexhoukou = -1; moveyhoukou = 0; }//左
        mokutekidown.position = nowdown.position;//とりあえず初期化
        mokutekidown.position += new Vector3(movexhoukou * haba*down, moveyhoukou * haba*down, 0.00f);//目的なので、それに方向×距離を足す
        mokutekiup.position = nowup.position;//同様
        mokutekiup.position += new Vector3(-1 * movexhoukou * haba*up, -1 * moveyhoukou * haba*up, 0.00f);//同様
        Vector3 upvectormokuteki, upvectornow, downvectornow, downvectormokuteki;//移動にはvectorにする必要がある
        upvectornow = nowup.position;//今
        upvectormokuteki = mokutekiup.position;//目的
        downvectornow = nowdown.position;//同じ
        downvectormokuteki = mokutekidown.position;//同じ
        ballup.transform.position = Vector3.Lerp(upvectornow, upvectormokuteki, 2);//Lerpですすむ、AnimationCurveであとで速さとか調節、第三引数ようわからないのでデバッグ
        balldown.transform.position = Vector3.Lerp(downvectornow, downvectormokuteki, 2);

        return true;
    }

    // Use this for initialization
    void Start()
    {
        mapgenerator = GameObject.Find("mapgenerator");//mapgeneratorからmapの配列をひくため、ただ、これ呼ばれる順番が怪しい
        map = GetComponent<mapgenerator>().map;//これ、こっちの方が速く実行されていたらしぬので、そこを注意
        acc = GetComponent<GetAcc>();//GetAccスクリプト
        nowrotation = 0;//最初のスマホの角度代入
    }

    // Update is called once per frame
    void Update()
    {
        if (idouchuu == true && nowrotation != acc.getDirection())//もし移動中じゃないかつスマホの向きが変わっていたら（回転されたら
        {
            idouchuu = false;//移動中
            nowrotation = acc.getDirection();//スマホの角度代入
            selectDirectionandrange(nowrotation, nowx, nowy);//上向きに何マス、下向きに何マス移動するかをメモ
            ballmove();//動かす！
            idouchuu = true;//もう動いていないよお
        }
    }
}
