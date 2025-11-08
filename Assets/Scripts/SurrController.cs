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
    
    private DictController dictController;
    public Collider2D[] hit0 = new Collider2D[5];

    public ShiftController shiftController = new ShiftController();

    
    //Dictionary<float, string> sub = new Dictionary<float, string>();

    [Obsolete]
    public void isSurExp(int incInteractTypeCLicked, int incInteractTypeOther, Vector2 incPosKeepClicked, Vector2 incPosKeepShifted)
    {
        dictController = FindObjectOfType<DictController>();
        /*Burada sadece objenin çevresinde bulunan diğer elemanlar patlamaya uygun mu onu kontrol edecez onun için bir yerde mapKeeperı lokal bir yere kopyalamalıyız.
        daha sonra kopyalanan bu mapKeeperZart'ı burada çağırarak kenar köşede bulunan elemanlar patlamaya uygun mu ona bakacaz*/
        mKC = mk.lvl1;
        
        
        areTheySameH(incPosKeepClicked, incPosKeepShifted, incInteractTypeCLicked);


    }



    public void areTheySameH(Vector2 worldPosClicked, Vector2 worldPosSwifted, int incInteractType)
    {
        
        //rastgele atandı değişecek
        dummyPos[0].x = worldPosClicked.x - 0.5f;
        dummyPos[0].y = worldPosClicked.y;
        //dummyPos.y = worldPos.y + 0.5f;

        //sağ-sol kontrol

        _incInterractType[0] = dictController.searchInDictionary(worldPosSwifted);
        Debug.Log("VAlue : " + _incInterractType[0]);
        //Burada tüm çevresel elemanlar kontrol edilecek gene ilk önce kaydırmanın yönüne karar verilmeli daha sonra ilk tıklanan elemanın çevresinden önce yatay kontrol daha sonra dikey ve aralar şeklinde kontrol sağlanır.
        _incInterractType[1] = dictController.searchInDictionary(dummyPos[0]);
        Debug.Log("VAlue : " + _incInterractType[1]);
        dummyPos[1].x = dummyPos[0].x - 0.5f;
        dummyPos[1].y = dummyPos[0].y;
        _incInterractType[2] = dictController.searchInDictionary(dummyPos[1]);
        Debug.Log("VAlue : " + _incInterractType[1]);
        

        if (_incInterractType[1] == _incInterractType[0])
        {


            if (_incInterractType[2] == _incInterractType[0])
            {
                if (Physics2D.OverlapPoint(worldPosClicked) != null && Physics2D.OverlapPoint(dummyPos[0]) != null && Physics2D.OverlapPoint(dummyPos[1]) != null)
                {
                    hit0[0] = Physics2D.OverlapPoint(worldPosClicked);//Cliked object itself
                    Debug.Log(worldPosClicked);
                    hit0[1] = Physics2D.OverlapPoint(dummyPos[0]);
                    Debug.Log(dummyPos[0]);
                    hit0[2] = Physics2D.OverlapPoint(dummyPos[1]);
                    Debug.Log(dummyPos[1]);

                    string keepName1 = hit0[0].name;
                    Debug.Log(hit0[0].name);
                    string keepName2 = hit0[1].name;
                    Debug.Log(hit0[1].name);
                    string keepName3 = hit0[2].name;
                    Debug.Log(hit0[2].name);

                    explodeFunc(hit0[0], hit0[1], hit0[2]);
                    //Burada önce aşşağıya shift fonksiyonu koymalıyım.

                    
                    
                    objectCreator.continueGame(shiftController.findToShift(worldPosClicked));//Burada find to shiftten gelen değerleri doğrudan continue game içine vererek orada oluşturulacak olan yeni taşların pozisyonuna karar verdik.
                    dictController.manageDictionary(worldPosClicked);
                    
                    objectCreator.continueGame(shiftController.findToShift(shiftController.findToShift(dummyPos[0])));

                    
                    objectCreator.continueGame(shiftController.findToShift(shiftController.findToShift(dummyPos[1])));

                    /*objectCreator.continueGame(worldPosClicked, keepName1);
                    objectCreator.continueGame(dummyPos[0], keepName2);
                    objectCreator.continueGame(dummyPos[1], keepName3);*/
                    
                }
                else
                {
                    Debug.Log("Hata!, null object var");
                }
                

            }else
                Debug.Log("2 ve 0 eşit değil");            
    
        }else
            Debug.Log("1 ve 0 eşit değil");
    }

    public void explodeFunc(Collider2D hitO0,Collider2D hitO1,Collider2D hitO2)
    {           
        Destroy(hitO0.gameObject);
        Destroy(hitO1.gameObject);
        Destroy(hitO2.gameObject);
        Debug.Log("Patlatma işlemi Başarılı");        
        
    }
        

    
}
