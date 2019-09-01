﻿/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;

public class GlobalStateManager : MonoBehaviour
{
    private int deadPlayers = 0;
    private int deadPlayerNumber = -1;

    //public static bool reset = false;
    //public GameObject player1Prefab;
    //public GameObject player2Prefab;

    public void Update()
    {
        //if (reset == true)
        //{
        //    deadPlayers = 0;
        //    deadPlayerNumber = 0;

        //    GameObject[] players = new GameObject[2];
        //    players = GameObject.FindGameObjectsWithTag("Player");
        //    for (int i = 0; i < players.Length; i++)
        //    {
        //        Destroy(players[i]);
        //    }

        //    Instantiate(player1Prefab, new Vector3(1, .5f, 7), Quaternion.identity);
        //    Instantiate(player2Prefab, new Vector3(8, .5f, 1), Quaternion.identity);
        //    reset = false;
        //}
    }

    public void PlayerDied (int playerNumber)
    {
        deadPlayers++;

        if (deadPlayers == 1)
        {
            deadPlayerNumber = playerNumber;
            Invoke("CheckPlayersDeath", .3f);
        }
    }

    public void CheckPlayersDeath()
    {
        if (deadPlayers == 1)
        {
            if (deadPlayerNumber == 1)
            {
                Debug.Log("Player 2 is the Winner!");
            }
            else
            {
                Debug.Log("Player 1 is the Winner!");
            }
        }
        else
        {
            Debug.Log("The game ended in a Draw!");
        }
    }
}
