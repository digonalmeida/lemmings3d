using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Player
{
    None,
    Player1,
    Player2
};

public class LemmingCustomizer : NetworkBehaviour
{
    //Variables
    private LinkedList<Color> clothColorList;
    private LinkedList<Color> hairColorList;
    private Dictionary<Color, bool> clothColorAvailability;
    private Dictionary<Color, bool> hairColorAvailability;

    //SyncVar Variables
    [SyncVar]
    public string player1Name = "";
    [SyncVar]
    public string player2Name = "";
    [SyncVar]
    public Color player1HairColor = Color.gray;
    [SyncVar]
    public Color player2HairColor = Color.gray;
    [SyncVar]
    public Color player1ClothColor = Color.gray;
    [SyncVar]
    public Color player2ClothColor = Color.gray;

    //Singleton
    private static LemmingCustomizer instance;
    public static LemmingCustomizer Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //Start
    private void Start()
    {
        //Dont Destroy on Chance Scene
        DontDestroyOnLoad(this.gameObject);

        //Create Lists
        clothColorList = new LinkedList<Color>();
        hairColorList = new LinkedList<Color>();
        clothColorAvailability = new Dictionary<Color, bool>();
        hairColorAvailability = new Dictionary<Color, bool>();

        //Populate Color Lists
        clothColorList.AddLast(Color.blue);
        hairColorList.AddLast(Color.blue);
        clothColorAvailability.Add(Color.blue, true);
        hairColorAvailability.Add(Color.blue, true);

        clothColorList.AddLast(Color.red);
        hairColorList.AddLast(Color.red);
        clothColorAvailability.Add(Color.red, true);
        hairColorAvailability.Add(Color.red, true);

        clothColorList.AddLast(Color.black);
        hairColorList.AddLast(Color.black);
        clothColorAvailability.Add(Color.black, true);
        hairColorAvailability.Add(Color.black, true);

        clothColorList.AddLast(Color.white);
        hairColorList.AddLast(Color.white);
        clothColorAvailability.Add(Color.white, true);
        hairColorAvailability.Add(Color.white, true);

        clothColorList.AddLast(Color.green);
        hairColorList.AddLast(Color.green);
        clothColorAvailability.Add(Color.green, true);
        hairColorAvailability.Add(Color.green, true);

        clothColorList.AddLast(Color.yellow);
        hairColorList.AddLast(Color.yellow);
        clothColorAvailability.Add(Color.yellow, true);
        hairColorAvailability.Add(Color.yellow, true);
    }

    //Request Next Cloth Color
    public void requestNextClothColor(Player playerNum)
    {
        //Get Current Color by Player
        Color currentColor;
        if (playerNum == Player.Player1) currentColor = player1ClothColor;
        else currentColor = player2ClothColor;

        //Start Variables
        LinkedListNode<Color> node = clothColorList.Find(currentColor);
        if (node != null) node = node.Next;
        bool available = false;

        //Iterate Linked List
        while (true)
        {
            if (node == null) node = clothColorList.First;
            clothColorAvailability.TryGetValue(node.Value, out available);

            if(available)
            {
                //Update Nodes
                clothColorAvailability[node.Value] = false;
                clothColorAvailability[currentColor] = true;

                //Update Sync Var
                if (playerNum == Player.Player1) player1ClothColor = node.Value;
                else player2ClothColor = node.Value;
                return;
            }
            else node = node.Next;
        }
    }

    //Request Next Hair Color
    public void requestNextHairColor(Player playerNum)
    {
        //Get Current Color by Player
        Color currentColor;
        if (playerNum == Player.Player1) currentColor = player1HairColor;
        else currentColor = player2HairColor;

        //Start Variables
        LinkedListNode<Color> node = hairColorList.Find(currentColor);
        if (node != null) node = node.Next;
        bool available = false;

        //Iterate Linked List
        while (true)
        {
            if (node == null) node = hairColorList.First;
            hairColorAvailability.TryGetValue(node.Value, out available);

            if (available)
            {
                //Update Nodes
                hairColorAvailability[node.Value] = false;
                hairColorAvailability[currentColor] = true;

                //Update Sync Var
                if (playerNum == Player.Player1) player1HairColor = node.Value;
                else player2HairColor = node.Value;
                return;
            }
            else node = node.Next;
        }
    }

    //Request Previous Hair Color
    public void requestPreviousClothColor(Player playerNum)
    {
        //Get Current Color by Player
        Color currentColor;
        if (playerNum == Player.Player1) currentColor = player1ClothColor;
        else currentColor = player2ClothColor;

        //Start Variables
        LinkedListNode<Color> node = clothColorList.Find(currentColor);
        if (node != null) node = node.Previous;
        bool available = false;

        //Iterate Linked List
        while (true)
        {
            if (node == null) node = clothColorList.Last;
            clothColorAvailability.TryGetValue(node.Value, out available);

            if (available)
            {
                //Update Nodes
                clothColorAvailability[node.Value] = false;
                clothColorAvailability[currentColor] = true;

                //Update Sync Var
                if (playerNum == Player.Player1) player1ClothColor = node.Value;
                else player2ClothColor = node.Value;
                return;
            }
            else node = node.Previous;
        }
    }

    //Request Previous Cloth Color
    public void requestPreviousHairColor(Player playerNum)
    {
        //Get Current Color by Player
        Color currentColor;
        if (playerNum == Player.Player1) currentColor = player1HairColor;
        else currentColor = player2HairColor;

        //Start Variables
        LinkedListNode<Color> node = hairColorList.Find(currentColor);
        if (node != null) node = node.Previous;
        bool available = false;

        //Iterate Linked List
        while (true)
        {
            if (node == null) node = hairColorList.Last;
            hairColorAvailability.TryGetValue(node.Value, out available);

            if (available)
            {
                //Update Nodes
                hairColorAvailability[node.Value] = false;
                hairColorAvailability[currentColor] = true;

                //Update Sync Var
                if (playerNum == Player.Player1) player1HairColor = node.Value;
                else player2HairColor = node.Value;
                return;
            }
            else node = node.Previous;
        }
    }
}
