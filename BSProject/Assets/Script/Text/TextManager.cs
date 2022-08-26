using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> TalkData_1;
    Dictionary<int, string[]> TalkData_2;
    Dictionary<int, Sprite> SpriteData_1;
    Dictionary<int, Sprite> SpriteData_2;
    public Sprite[] SpriteArr;
    void Awake()
    {
        TalkData_1 = new Dictionary<int, string[]>();
        TalkData_2 = new Dictionary<int, string[]>();
        SpriteData_1 = new Dictionary<int, Sprite>();
        SpriteData_2 = new Dictionary<int, Sprite>();
        CreateData();
    }
    void CreateData() //대사 목록
        //TalkData.Add(대사 ID, new string[] {대사 목록});
    {
        TalkData_1.Add(1000, new string[] { "나는;0", "잘생겼다;1", "ㅎㅎㅎㅎ;1"});
        TalkData_2.Add(1000, new string[] { ";0", "ㅎㅎㅎㅎ;1", "ㅎㅎㅎㅎ;1" });
        SpriteData_1.Add(1000 + 0, SpriteArr[0]);
        SpriteData_1.Add(1000 + 1, SpriteArr[1]);
        SpriteData_2.Add(1000 + 0, SpriteArr[0]);
        SpriteData_2.Add(1000 + 1, SpriteArr[1]);
        TalkData_1.Add(2000, new string[] { "너는;0", "못생겼다;1" });
        TalkData_2.Add(2000, new string[] { ";0", "...으;1" });
        SpriteData_1.Add(2000 + 0, SpriteArr[0]);
        SpriteData_1.Add(2000 + 1, SpriteArr[2]);
        SpriteData_2.Add(2000 + 0, SpriteArr[0]);
        SpriteData_2.Add(2000 + 1, SpriteArr[1]);
    }
    public string GetTalk_1(int ID, int TalkIndex)
    {
        if (TalkIndex == TalkData_1[ID].Length) return null;
        else return TalkData_1[ID][TalkIndex];
    }
    public string GetTalk_2(int ID, int TalkIndex)
    {
        if (TalkIndex == TalkData_2[ID].Length) return null;
        else return TalkData_2[ID][TalkIndex];
    }
    public Sprite GetLeftSprite(int ID, int SpriteIndex)
    {
        return SpriteData_1[ID + SpriteIndex];
    }
    public Sprite GetRightSprite(int ID, int SpriteIndex)
    {
        return SpriteData_2[ID + SpriteIndex];
    }
}