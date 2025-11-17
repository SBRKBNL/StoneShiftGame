using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class SurrController : MonoBehaviour
{
    public MapKeeper mk = new MapKeeper();
    public string[,] mKC;
    //Collider2D[] hit0 = new Collider2D[10];
    public string[] _incInterractType = new string[18];
    public string createdName;
    public bool localSwipedOrHadTo;
    public Vector2[] dummyPos = new Vector2[5];

    public ObjectCreator objectCreator;
    
    private DictController _dictController;
    public Collider2D[] hit0 = new Collider2D[5];

    public ShiftController shiftController = new ShiftController();

    
    //Dictionary<float, string> sub = new Dictionary<float, string>();

    void Start()
    {   
        _dictController = FindObjectOfType<DictController>();
        shiftController.HizmetSinifiAtama(_dictController);//Burada gönderdikten sonra da newle uyandırıyor muhtemelen bura ile alakalı bir sorun var çöz!!
                                                            //Çözüldü
    }

    [Obsolete]
    public bool isSurExp(int incInteractTypeCLicked, int incInteractTypeOther, Vector2 incPosKeepClicked, Vector2 incPosKeepShifted)
    {

        /*Burada sadece objenin çevresinde bulunan diğer elemanlar patlamaya uygun mu onu kontrol edecez onun için bir yerde mapKeeperı lokal bir yere kopyalamalıyız.
        daha sonra kopyalanan bu mapKeeperZart'ı burada çağırarak kenar köşede bulunan elemanlar patlamaya uygun mu ona bakacaz*/
        mKC = mk.lvl1;


        return areTheySameH(incPosKeepClicked, incPosKeepShifted, incInteractTypeCLicked);


    }



    public bool areTheySameH(Vector2 worldPosClicked, Vector2 worldPosSwifted, int incInteractType)
    {

        //rastgele atandı değişecek
        dummyPos[0].x = worldPosClicked.x - 0.5f;
        dummyPos[0].y = worldPosClicked.y;
        //dummyPos.y = worldPos.y + 0.5f;

        //sağ-sol kontrol

        _incInterractType[0] = _dictController.searchInDictionary(worldPosSwifted);
        //Debug.Log("VAlue : " + _incInterractType[0]);
        //Burada tüm çevresel elemanlar kontrol edilecek gene ilk önce kaydırmanın yönüne karar verilmeli daha sonra ilk tıklanan elemanın çevresinden önce yatay kontrol daha sonra dikey ve aralar şeklinde kontrol sağlanır.
        _incInterractType[1] = _dictController.searchInDictionary(dummyPos[0]);
        //Debug.Log("VAlue : " + _incInterractType[1]);
        dummyPos[1].x = dummyPos[0].x - 0.5f;
        dummyPos[1].y = dummyPos[0].y;
        _incInterractType[2] = _dictController.searchInDictionary(dummyPos[1]);
        //Debug.Log("VAlue : " + _incInterractType[1]);


        if (_incInterractType[1] == _incInterractType[0])
        {


            if (_incInterractType[2] == _incInterractType[0])
            {
                if (Physics2D.OverlapPoint(worldPosClicked) != null && Physics2D.OverlapPoint(dummyPos[0]) != null && Physics2D.OverlapPoint(dummyPos[1]) != null)
                {
                    hit0[0] = Physics2D.OverlapPoint(worldPosClicked);//Cliked object itself
                    //Debug.Log(worldPosClicked);
                    hit0[1] = Physics2D.OverlapPoint(dummyPos[0]);
                    //Debug.Log(dummyPos[0]);
                    hit0[2] = Physics2D.OverlapPoint(dummyPos[1]);
                    //Debug.Log(dummyPos[1]);

                    string keepName1 = hit0[0].name;
                    //Debug.Log(hit0[0].name);
                    string keepName2 = hit0[1].name;
                    //Debug.Log(hit0[1].name);
                    string keepName3 = hit0[2].name;
                    //Debug.Log(hit0[2].name);

                    explodeFunc(hit0[0], hit0[1], hit0[2]);


                    shiftController.findToShift(worldPosClicked,0, false);
                    shiftController.findToShift(shiftController.findToShift(dummyPos[0],0 , false),0 , false);
                    shiftController.findToShift(shiftController.findToShift(dummyPos[1],0 , false),0 , true);

                    

                    Debug.Log("0. interacttype: " + _incInterractType[0] + "1. interacttype: " + _incInterractType[1]+ "2. interacttype: " + _incInterractType[2]);
                    /*objectCreator.continueGame(worldPosClicked, keepName1);
                    objectCreator.continueGame(dummyPos[0], keepName2);
                    objectCreator.continueGame(dummyPos[1], keepName3);*/

                    return true; // Match found and explosion occurred
                }
                else
                {
                    Debug.Log("Hata!, null object var");
                    return false;
                }


            }else{
                Debug.Log("0. interacttype: " + _incInterractType[0] + "1. interacttype: " + _incInterractType[1]+ "2. interacttype: " + _incInterractType[2]);
                return false; // No match
            }

        }else{
            Debug.Log("0. interacttype: " + _incInterractType[0] + "1. interacttype: " + _incInterractType[1]+ "2. interacttype: " + _incInterractType[2]);
            return false; // No match
        }
    }

    public void explodeFunc(Collider2D hitO0,Collider2D hitO1,Collider2D hitO2)
    {           
        Destroy(hitO0.gameObject);
        Destroy(hitO1.gameObject);
        Destroy(hitO2.gameObject);
        Debug.Log("Patlatma işlemi Başarılı");        
        
    }
        

    
}
