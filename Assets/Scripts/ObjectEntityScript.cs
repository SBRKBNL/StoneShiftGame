using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectEntity", menuName = "Scriptable Objects/ObjectEntity")]
public class ObjectEntity : ScriptableObject
{
    public Array[] objectData;
    public Sprite objectSprite;
    public string objectName;
    public int objectType; //Object Type 0 -> main game
                           //1 -> blocks
                           //2 -> jokers
    public int interactType; //Object Type 0
                             //InteractType 0 -> Red   
                             //1 -> Blue
                             //2 -> Green
                             //3 -> Yellow

                             /*Object Type 1
                             InteractionType 0 -> Grass
                             1 -> Grass x2
                             2 -> Dolap(?)
                             3 -> Door
                             4 -> Box
                             5 -> Tabak        
                             */ 

                            /*Object Type 2
                             InteractionType 0 -> Fırfır
                             1 -> Renkli Patlangaç
                             2 -> Fırlama Yatay
                             3 -> Fırlama Dikey
                             4 -> Tnt
                             */ 





    public bool isActive;
    

}
