using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //atrybuty
    private int itemID;
    private string name;
    //string description;
    //atrybuty end
   
    //metody - settery
    public void setItemID(int id){
        this.itemID = id;
    }
    public void setName(string name){
        this.name=name;
    }
    //metody - settery end

    //metody - gettery
    public int getItemID(){
        return this.itemID;
    }
    public string getName(){
        return this.name;
    }
    //metody - gettery end
   
}
