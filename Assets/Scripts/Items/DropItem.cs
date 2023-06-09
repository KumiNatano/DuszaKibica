using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public const float rotateSpeed = 60.0f;
    public const float swingAmplitude = 0.1f;
    public const float swingFrequency = 2.5f;
    public AudioClip[] pickupSounds;

    private Vector3 initPos;

    //atrybuty
    [SerializeField] int itemID;
    [SerializeField] string name;
    //string description;
    //atrybuty end
    private void Start()
    {
        transform.position += new Vector3(0f, 0.3f, 0f);
        initPos = transform.position;
    }

    protected virtual void Update()
    {
        Rotate();
        Swing();
    }

    public virtual void OnPickup(Player player) { }
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
    public void Rotate(){
        transform.Rotate(Vector3.forward * degreesPerSecond * Time.deltaTime);
    }
    public void Swing(){
        transform.position = initPos + Vector3.up * Mathf.Sin(GetInstanceID() + Time.time * swingFrequency) * swingAmplitude;
    }


    [Obsolete("Use Rotate method instead.")]
    public void rotating(){
        Rotate();
    }

    [Obsolete("use rotateSpeed instead")] int degreesPerSecond => (int)rotateSpeed;
}
