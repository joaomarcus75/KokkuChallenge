﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using System.Diagnostics.CodeAnalysis;

namespace AutoBattle
{
    public class CharacterBase
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public GridBox currentBox;
        public int PlayerIndex;
        public Character Target { get; set; }
        public void StartTurn(Grid battlefield)
        {


            if (CheckCloseTargets(battlefield))
            {
                Attack(Target);


                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (this.currentBox.xIndex > Target.currentBox.xIndex)
                {
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked left\n");
                        battlefield.drawBattlefield(5, 5);

                        return;
                    }
                    return;
                }
                else if (currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {PlayerIndex} walked right\n");
                    battlefield.drawBattlefield(5, 5);
                    return;
                }

                if (this.currentBox.yIndex > Target.currentBox.yIndex)
                {
                    battlefield.drawBattlefield(5, 5);
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght));
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {PlayerIndex} walked up\n");
                    return;
                }
                else if (this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = this.currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {PlayerIndex} walked down\n");
                    battlefield.drawBattlefield(5, 5);

                    return;
                }
                return;
            }

            bool CheckCloseTargets(Grid battlefield)
            {
                bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
                bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
                bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied);
                bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied);

                if (left & right & up & down)
                {
                    return true;
                }
                return false;
            }
             void Attack(Character target)
            {
                var rand = new Random();
                target.TakeDamage(rand.Next(0, (int)BaseDamage));
                Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
            }
        }
    }
}