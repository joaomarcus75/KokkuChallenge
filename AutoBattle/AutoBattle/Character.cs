using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using System.Diagnostics.CodeAnalysis;


namespace AutoBattle
{
    public class Character:IComparable<Character>
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public GridBox currentBox;
        public int PlayerIndex;
        public Character Target { get; set; }
        
        bool left;
        bool right;
        bool up;
        bool down;

        public Character(CharacterClass characterClass)
        {

        }


        public bool TakeDamage(float amount)
        {
            float ArcherLessDamage = Target.BaseDamage - 5;
            if (Name == "Archer")
            {
                Target.BaseDamage = ArcherLessDamage;
            }

            Target.Health -= Target.BaseDamage;

            if((Target.Health -= Target.BaseDamage) <= 0)
            {
                Target.Health = 0;
                Die();
                return true;
            }
            
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine($"{Target.Name} Dies...\n");
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield)
        {

            if (CheckCloseTargets(battlefield)) 
            {
                Attack(Target);
                

                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if(this.currentBox.xIndex > Target.currentBox.xIndex)
                {
                    if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        //currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;

                        currentBox.yIndex = currentBox.yIndex - 1;

                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {Name} walked left, Current position: [{currentBox.xIndex},{currentBox.yIndex}]\n");
                        battlefield.currentgrid.yIndex = currentBox.yIndex;
                        Grid currentPosition = new Grid(currentBox.xIndex, currentBox.yIndex);


                        if (CheckCloseTargets(currentPosition) == true)
                        {
                            Attack(Target);
                            battlefield.drawBattlefield(5, 5);
                        }
                        battlefield.drawBattlefield(5, 5);

                        return;
                    }
                    return;
                } else if(currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    
                    battlefield.grids[currentBox.Index] = currentBox;
                    //currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.ocupied = true;
                    
                    
                    currentBox.yIndex = currentBox.yIndex + 1;
                    
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {Name} walked right, Current position: [{currentBox.xIndex},{currentBox.yIndex}]\n ");
                    battlefield.currentgrid.yIndex = currentBox.yIndex;

                    Grid currentPosition = new Grid(currentBox.xIndex,currentBox.yIndex );//(currentBox.xIndex, currentBox.yIndex);

                    if (CheckCloseTargets(currentPosition) == true)
                    {
                        Attack(Target);
                        battlefield.drawBattlefield(5, 5);
                    }
                    battlefield.drawBattlefield(5, 5);
                    return;
                }

                if (this.currentBox.yIndex > Target.currentBox.yIndex)
                {
                    battlefield.drawBattlefield(5, 5);

                    this.currentBox.ocupied = false;

                    battlefield.grids[currentBox.Index] = currentBox;
                    //this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght));
                    this.currentBox.ocupied = true;

                    currentBox.yIndex = currentBox.xIndex - 1;

                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {Name} walked up, , Current position: [{currentBox.xIndex},{currentBox.yIndex}]\n");
                    battlefield.currentgrid.xIndex = currentBox.xIndex;

                    if (CheckCloseTargets(battlefield) == true)
                    {
                        Attack(Target);
                        battlefield.drawBattlefield(5, 5);
                    }

                    //battlefield.drawBattlefield(5, 5);
                    return;
                }
                else if(this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    this.currentBox.ocupied = true;

                    battlefield.grids[currentBox.Index] = this.currentBox;
                    //this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
                    this.currentBox.ocupied = false;

                    currentBox.yIndex = currentBox.xIndex + 1;

                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {Name} walked down, , Current position: [{currentBox.xIndex},{currentBox.yIndex}]\n");
                    battlefield.currentgrid.xIndex = currentBox.xIndex;
                    Grid currentPosition = new Grid(currentBox.xIndex, currentBox.yIndex);

                    if (CheckCloseTargets(currentPosition) == true)
                    {
                        Attack(Target);
                        battlefield.drawBattlefield(5, 5);
                    }

                    battlefield.drawBattlefield(5, 5);

                    return;
                }
                return;
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            
            
            var NewLeftYPosition = currentBox.yIndex - 1;
            var NewRightYPosition = currentBox.yIndex + 1;
            var NewUpPosition = currentBox.xIndex + 1;
            var NewDownPosition = currentBox.Index - 1;

            if(NewLeftYPosition > 4) {NewLeftYPosition = 4;}
            if(NewRightYPosition > 4 ) {NewRightYPosition = 4;}
            if(NewUpPosition > 4) {NewUpPosition = 4;}
            if(NewDownPosition > 4) {NewDownPosition = 4;}
            
            if(NewLeftYPosition < 0) { NewLeftYPosition = 1; }
            if (NewRightYPosition > 0) { NewRightYPosition = 1; }
            if (NewUpPosition > 0) { NewUpPosition = 0; }
            if (NewDownPosition > 0) { NewDownPosition = 0; }
            if (NewLeftYPosition < 0) { NewLeftYPosition = 0; }

            if (currentBox.yIndex == (NewLeftYPosition) && currentBox.xIndex == Target.currentBox.xIndex)
            {
                left = true;
            }
            if (currentBox.yIndex == (NewRightYPosition) && currentBox.xIndex == Target.currentBox.xIndex) 
            {
                right = true;
            }
            if (currentBox.xIndex == (NewUpPosition) && currentBox.yIndex == Target.currentBox.yIndex)
            {
                up = true;
            }
            if (currentBox.xIndex == (NewDownPosition) && currentBox.yIndex == Target.currentBox.yIndex)
            {
                down = true;
            }
            if (left || right || up || down) 
            {
                return true;
            }
            return false; 
        }

        public void Attack (Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player: {Name} is attacking the player {Target.Name} and did {BaseDamage} damage\n");
            Console.WriteLine($"Player: {Name}, Health: {Health}, Player: {Target.Name}, Health: {Target.Health} \n");
        }

        public int CompareTo(Character other)
        {
            return this.Health.CompareTo(other.Health);
            
        }
    }
}
