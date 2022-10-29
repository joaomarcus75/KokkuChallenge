using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;
using System.Text.Json;


namespace AutoBattle
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            CharacterClass characterClass;
            CharacterClass enemyClass;
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            GridBox RandomLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            int randomXposition;
            int randomYposition;
            int random;

            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            Setup(); 


            void Setup()
            {

                GetPlayerChoice();
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "2":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {
               
                characterClass = (CharacterClass)classIndex;
                PlayerCharacter = new Character(characterClass);
                PlayerCharacter.Name = characterClass.ToString();
                if (PlayerCharacter.Name == "Paladin") { PlayerCharacter.Health = 200;}
                else { PlayerCharacter.Health = 100; }
                if(PlayerCharacter.Name == "Warrior") { PlayerCharacter.BaseDamage = 40 ;}
                else { PlayerCharacter.BaseDamage = 20; }

                PlayerCharacter.PlayerIndex = 0;
                Console.WriteLine($"Player Class Choice: {characterClass} , Health: {PlayerCharacter.Health}, Strength: {PlayerCharacter.BaseDamage} ");
                
                CreateEnemyCharacter();

            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                enemyClass = (CharacterClass)randomInteger;
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.Name = enemyClass.ToString();
                EnemyCharacter.Health = 100;
                EnemyCharacter.BaseDamage = 20;
                EnemyCharacter.PlayerIndex = 1;
                Console.WriteLine($"Enemy Class Choice: {enemyClass} , Health: {EnemyCharacter.Health}");
                StartGame();

            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();

            }

            void StartTurn(){

                if (currentTurn == 0)
                {
                    AllPlayers.Sort();  
                }

                foreach(Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }


                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(PlayerCharacter.Health == 0)
                {
                    Character TakeDamage = PlayerCharacter;
                    TakeDamage.TakeDamage(PlayerCharacter.Health); 
                    // GetPlayerChoice();
                   // Console.Write(Environment.NewLine + Environment.NewLine);
                    //Console.Write($"{PlayerCharacter.Name} + Dies");
                    //Console.Write(Environment.NewLine + Environment.NewLine);
                    
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key restart the game...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);


                    ConsoleKeyInfo key = Console.ReadKey();
                    Setup();
                    //return;
                }
                else if (EnemyCharacter.Health == 0)
                {
                    Character TakeDamage = EnemyCharacter;
                    TakeDamage.TakeDamage(EnemyCharacter.Health);
                    //Console.Write(Environment.NewLine + Environment.NewLine);
                    //Console.WriteLine($"{EnemyCharacter.Name}: Dies...\n");
                    //Console.Write(Environment.NewLine + Environment.NewLine);

                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key restart the game...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);


                    ConsoleKeyInfo key = Console.ReadKey();
                    Setup();

                    //return;
                } else
                {
                    Character startTurn = PlayerCharacter;
                    startTurn.StartTurn(grid);
                    
                   Console.Write(Environment.NewLine + Environment.NewLine);
                   Console.WriteLine("Click on any key to start the next turn...\n");
                   Console.Write(Environment.NewLine + Environment.NewLine);
                    

                    ConsoleKeyInfo key = Console.ReadKey();
                    startTurn.StartTurn(grid);

                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();

            }

            void AlocatePlayerCharacter()
            {
                random =  GetRandomInt(0, 4);
                RandomLocation = (grid.grids.ElementAt(random));
                if (!RandomLocation.ocupied)
                {
                    PlayerCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.currentgrid.ocupied = RandomLocation.ocupied;
                    randomXposition = GetRandomInt(0, 4);
                    randomYposition = GetRandomInt(0, 4);
                    RandomLocation.xIndex = randomXposition;
                    RandomLocation.yIndex = randomYposition;
                    grid.currentgrid.xIndex = RandomLocation.xIndex;
                    grid.currentgrid.yIndex = RandomLocation.yIndex;
                    
                    grid.grids[random] = RandomLocation;
                    
                    PlayerCharacter.currentBox = grid.grids[random];
                Console.Write($" {PlayerCharacter.Name},{random},Position:[{RandomLocation.xIndex},{RandomLocation.yIndex}]\n");
                    
                    grid.drawBattlefield(5, 5);
                    AlocateEnemyCharacter();
                } else
                {
                    
                    AlocatePlayerCharacter();
                    
                }
                
            }

            void AlocateEnemyCharacter()
            {
                 random = GetRandomInt(0, 4);
                RandomLocation = (grid.grids.ElementAt(random));
                
                if (!RandomLocation.ocupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.currentgrid.ocupied = RandomLocation.ocupied;
                    randomXposition = GetRandomInt(0, 4);
                    randomYposition = GetRandomInt(0, 4);
                    RandomLocation.xIndex = randomXposition;
                    RandomLocation.yIndex = randomYposition;
                    grid.currentgrid.xIndex = RandomLocation.xIndex;
                    grid.currentgrid.yIndex = RandomLocation.yIndex;

                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];

                    Console.Write($" {PlayerCharacter.Name},{random},Position:[{RandomLocation.xIndex},{RandomLocation.yIndex}]\n");
                    
                    grid.drawBattlefield(5 , 5);
                }
                else
                 {
                    
                    AlocateEnemyCharacter();
                }

                
            }

            
        }
    }
}
