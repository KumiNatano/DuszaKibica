using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    //atrybuty
    [SerializeField] int itemID;
    [SerializeField] string name;
    [SerializeField] int degreesPerSecond = 360;
    //string description;
    //atrybuty end
    private void Start()
    {
        transform.position += new Vector3(0f, 0.3f, 0f);
    }

    void Update()
    {
        rotating();
    }
   
    
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
    public void rotating(){
        transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
    }
}
