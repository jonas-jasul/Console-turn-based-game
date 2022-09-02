using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace anotherObjektProg
{
    class Program
    {
        //Jonas Jasulevičius
        //PS-1/2
        //Objektinis programavimas

        //persists from the time it's constructed until the program ends, global access
        public static Player newPlayer = new Player();
        public static Enemy newEnemy = new Enemy();
        public static Combat combatInstance = new Combat();
        static void Main(string[] args)
        {
            
            Start();

            Random rnd = new Random();
            //Chooses a random enemy from two choices
            int enemyGenerate = rnd.Next(0, 2);
            switch (enemyGenerate)
            {
                case 0:
                    newEnemy.EnemyName = "Dragon";
                    break;

                case 1:
                    newEnemy.EnemyName = "Orc";
                    break;

            }
            Console.WriteLine("\nYou will be fighting: " + newEnemy.EnemyName);
            Console.WriteLine("Enemy has: " + newEnemy.health + " HP\n");


            while (newPlayer.IsAlive && newEnemy.IsAlive)
            {
                //create a new boolean, which is set to true when the playable character is defending

                bool isDef = false;

                if (newPlayer.PlayerClass == "Warrior")
                {

                    combatInstance.PrintWarriorOptions(newPlayer.health, newPlayer.stamina, newPlayer.healthPotion);

                    try
                    {
                        int input = Convert.ToInt32(Console.ReadLine());
                        switch (input)
                        {
                            case 1:
                                int dmg = rnd.Next(2, 20 * (1 + (newPlayer.strength) / 100));
                                if (newPlayer.stamina - 20 < 0)
                                {
                                    Console.WriteLine("\nNot enough stamina! " +
                                        "Wait for it to regenerate between turns!\n");
                                    continue;
                                }

                                else
                                {
                                    newPlayer.SwordBash(dmg);
                                    newEnemy.TakeDamage(dmg);
                                }

                                break;

                            case 2:
                                int dmg2 = rnd.Next(1, 12 * (1 + (newPlayer.strength) / 100));
                                newPlayer.BasicAttack(dmg2);
                                newEnemy.TakeDamage(dmg2);
                                break;

                            case 3:
                                if (newPlayer.healthPotion > 0)
                                {
                                    newPlayer.UseHealthPotion();
                                }
                                else
                                {
                                    Console.WriteLine("You are out of health potions!");
                                    continue;
                                }
                                break;

                            case 4:
                                newPlayer.Defend();
                                isDef = true;
                                break;

                            // Missing is only possible when trying to use a ranged item
                            case 5:
                                Sling newSling = new Sling("The Sling of Rage", "a pebble");
                                int dmgItem = rnd.Next(0, 15);
                                if (dmgItem == 0)
                                {
                                    Console.WriteLine("\nYou have missed!");
                                }
                                else
                                {
                                    newSling.UseRangedWeapon(dmgItem);
                                    newEnemy.TakeDamage(dmgItem);
                                }
                                break;

                            case 6:
                                int escapeChance = rnd.Next(1, 6);
                                if (escapeChance >= 3)
                                {
                                    newPlayer.EscapeSuccess();
                                    return;
                                }
                                else
                                {
                                    newPlayer.EscapeFail();
                                    break;
                                }

                            default:
                                Console.WriteLine("You have entered a wrong number!");
                                continue;
                        }
                    }
                    catch (System.FormatException ex)
                    {
                        Console.WriteLine("\nPlease enter a correct integer!\n");
                        //Console.WriteLine(ex);
                        continue;
                    }
                }

                else if (newPlayer.PlayerClass == "Mage")
                {
                    combatInstance.PrintMageOptions(newPlayer.health, newPlayer.mana, newPlayer.healthPotion, newPlayer.manaPotion);
                    try
                    {
                        int input = Convert.ToInt32(Console.ReadLine());

                        switch (input)
                        {
                            case 1:
                                int dmg = rnd.Next(2, 25 * (1 + (newPlayer.intelligence) / 100));
                                if (newPlayer.mana > 0)
                                {
                                    newPlayer.LightningStorm(dmg);
                                }
                                else
                                {
                                    Console.WriteLine("\nYou are out of mana!\n");
                                    continue;
                                }

                                newEnemy.TakeDamage(dmg);
                                break;

                            case 2:
                                int dmg2 = rnd.Next(1, 10 * (1 + (newPlayer.strength) / 100));
                                newPlayer.BasicAttack(dmg2);
                                newEnemy.TakeDamage(dmg2);
                                break;

                            case 3:
                                if (newPlayer.healthPotion > 0)
                                {
                                    newPlayer.UseHealthPotion();

                                }
                                else
                                {
                                    Console.WriteLine("You are out of health potions!");
                                    continue;
                                }
                                break;

                            case 4:
                                if (newPlayer.manaPotion > 0)
                                {
                                    newPlayer.UseManaPotion();
                                }
                                else
                                {
                                    Console.WriteLine("You are out of mana potions!");
                                    continue;
                                }
                                break;

                            case 5:
                                newPlayer.Defend();
                                isDef = true;
                                break;

                            case 6:
                                Sling newSling = new Sling("The Sling of Lightning", "an electrified stone");
                                int dmgItem = rnd.Next(0, 18);
                                if (dmgItem == 0)
                                {
                                    Console.WriteLine("\nYou have missed!");
                                }
                                else
                                {
                                    newSling.UseRangedWeapon(dmgItem);
                                    newEnemy.TakeDamage(dmgItem);
                                }
                                break;

                            case 7:
                                int escapeChance = rnd.Next(1, 6);
                                if (escapeChance >= 3)
                                {
                                    newPlayer.EscapeSuccess();
                                    return;
                                }
                                else
                                {
                                    newPlayer.EscapeFail();
                                    break;
                                }

                            default:
                                Console.WriteLine("You have entered a wrong number!");
                                continue;
                        }
                    }
                    catch (System.FormatException e)
                    {
                        Console.WriteLine("\nPlease enter a correct integer!\n");
                        Console.WriteLine(e);
                        Console.WriteLine("");
                        continue;
                    }
                }


                //Regenerate a random amount of stamina each turn (if Warrior)
                newPlayer.stamina += rnd.Next(1, 6);

                if (newEnemy.EnemyName == "Orc" && newEnemy.IsAlive)
                {
                    Console.WriteLine("Enemy has: " + newEnemy.health + " HP left");
                    Console.WriteLine("\n*****************************************");

                    //If the player is defending, enemy deals half the damage it would normally do
                    if (isDef == true)
                    {
                        int dmgDef = rnd.Next(1, 10) / 2;
                        newEnemy.OrcAttack(dmgDef);
                        newPlayer.TakeDamage(dmgDef);
                    }

                    else
                    {
                        int dmg = rnd.Next(1, 10);
                        newEnemy.OrcAttack(dmg);
                        newPlayer.TakeDamage(dmg);

                    }

                }

                else if (newEnemy.EnemyName == "Dragon" && newEnemy.IsAlive)
                {
                    Console.WriteLine("Enemy has: " + newEnemy.health + " HP left");
                    Console.WriteLine("\n*****************************************");

                    if (isDef == true)
                    {
                        int dmgDef = rnd.Next(2, 15) / 2;
                        newEnemy.DragonAttack(dmgDef);
                        newPlayer.TakeDamage(dmgDef);
                    }

                    else
                    {
                        int atkRandom = rnd.Next(0, 2);
                        switch (atkRandom)
                        {
                            case 0:
                                int dmg = rnd.Next(2, 15);
                                newEnemy.DragonAttack(dmg);
                                newPlayer.TakeDamage(dmg);
                                break;

                            case 1:
                                int dmgSpec = rnd.Next(4, 20);
                                newEnemy.DragonSpecialAttack(dmgSpec);
                                newPlayer.TakeDamage(dmgSpec);
                                break;
                        }


                    }

                }
                if (!newPlayer.IsAlive)
                {
                    combatInstance.PrintRemainingHealth(newPlayer.health);
                    Console.WriteLine("**********************");
                    Console.WriteLine("The player (" + newPlayer.name + ") lost!");
                    Console.WriteLine("\nTotal attack count: " + Entity.attackCount);
                    break;
                }

                else if (!newEnemy.IsAlive)
                {
                    combatInstance.PrintEnemyRemainingHealth(newEnemy.health);
                    Console.WriteLine("**********************");
                    Console.WriteLine("The player (" + newPlayer.name + ") won!");
                    Console.WriteLine("\nTotal attack count: " + Entity.attackCount);
                    break;
                }

                //combatInstance.PrintRemainingHealth(newPlayer.health);
            }
        }


        public static int GetSTR()
        {
            string enteredSTR;

            do
            {
                Console.WriteLine("\nEnter how much STR you would like to have: (between 0 and 20)");
                enteredSTR = Console.ReadLine();
                newPlayer.strength = Convert.ToInt32(enteredSTR);
            }
            while (Convert.ToInt32(enteredSTR) > 20);
            return Convert.ToInt32(enteredSTR);
        }

        public static void PrintSlow(string text)
        {
            foreach (char character in text)
            {
                Console.Write(character);
                Thread.Sleep(45);
            }
            //Console.WriteLine("");
        }

        public static void Start()
        {

            PrintSlow("**********************\n*** ");
            PrintSlow("The game is starting!");
            PrintSlow(" ***\n**********************\n");
            PrintSlow("Please enter your name: ");

            newPlayer.name = Console.ReadLine();
            string inputPlayerClass;
            do
            {
                Console.WriteLine("\nChoose your class: Warrior or Mage");
                inputPlayerClass = Console.ReadLine();
                inputPlayerClass = char.ToUpper(inputPlayerClass[0]) + inputPlayerClass.Substring(1);
            }

            while (inputPlayerClass != "Warrior" && inputPlayerClass != "Mage");
            newPlayer.PlayerClass = inputPlayerClass;

            Console.WriteLine("\nYou have chosen the " + newPlayer.PlayerClass + " class." +
                " You will have: " + newPlayer.health + " health points.");

            int totalStats = 20;

            bool error = true;
            int remainingPoints = 0;
            do
            {
                try
                {
                    remainingPoints = totalStats - Convert.ToInt32(GetSTR());
                    error = false;
                    Console.WriteLine("\nStat points left: " + remainingPoints + " \n");
                }
                catch
                {
                    Console.WriteLine("Enter a correct integer!");

                }

            } while (error);


            string enteredINT;

            do
            {

                Console.WriteLine("Enter how much INT you would like to have: (between 0 and " + remainingPoints + ")");
                enteredINT = Console.ReadLine();
                newPlayer.intelligence = Convert.ToInt32(enteredINT);

            }
            while (Convert.ToInt32(enteredINT) > 20 || Convert.ToInt32(enteredINT) > remainingPoints);


            Console.WriteLine("__________________________________________________");
            Console.WriteLine("\nYou have: " + newPlayer.strength + " STR");
            Console.WriteLine("You have: " + newPlayer.intelligence + " INT");
            Console.WriteLine("You have: " + newPlayer.healthPotion + " health potions");
            Console.WriteLine("You have: " + newPlayer.manaPotion + " mana potions");
            Console.WriteLine("__________________________________________________");
        }
    }

    class Combat
    {
        public void PrintWarriorOptions(int remainingHealth, int remainingStamina, int HealthPotAmount)
        {
            PrintRemainingHealth(remainingHealth);
            Console.WriteLine("You have: " + remainingStamina + " stamina left");
            Console.WriteLine("You have: " + HealthPotAmount + " health potions");
            Console.WriteLine("__________________________________________________");
            Console.WriteLine("You have six options: \n(1) Sword Bash - Special Attack " +
                "\n(2) Basic Attack " +
                "\n(3) Use a HP potion " +
                "\n(4) Defend " +
                "\n(5) Use an item(ranged) " +
                "\n(6) Escape" +
                "\n\nEnter the according number to execute the desired action.");
            Console.WriteLine("__________________________________________________");
        }

        public void PrintMageOptions(int remainingHealth, int remainingMana, int HealthPotAmount, int ManaPotAmount)
        {
            PrintRemainingHealth(remainingHealth);
            Console.WriteLine("You have: " + remainingMana + " MP left");
            Console.WriteLine("You have: " + HealthPotAmount + " health potions");
            Console.WriteLine("You have: " + ManaPotAmount + " mana potions");
            Console.WriteLine("__________________________________________________");
            Console.WriteLine("\nYou have seven options: \n(1) Lightning Storm - Special Attack " +
                "\n(2) Basic Attack " +
                "\n(3) Use a HP potion " +
                "\n(4) Use a mana potion " +
                "\n(5) Defend " +
                "\n(6) Use an item(ranged) " +
                "\n(7) Escape" +
                "\n\nEnter the according number to execute the desired action.");
            Console.WriteLine("__________________________________________________");

        }

        public void PrintRemainingHealth(int remainingHealth)
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("You have: " + remainingHealth + " HP left");

        }

        public void PrintEnemyRemainingHealth(int remainingEnemyHealth)
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("Enemy has: " + remainingEnemyHealth + " HP left");
        }

    }

   //Abstract, because no Entity objects will be needed
   //it will only be used as a base for Player and Enemy classes 

    public abstract class Entity
    {
        public string name;
        public int strength;
        public int health;
        public int intelligence;
        public int stamina;
        public int mana;
        public bool IsAlive = true;
        public static int attackCount = 0;
        protected int maxHealth = 150;
        protected int maxMana = 100;


        public void TakeDamage(int dmgAmount)
        {
            health -= dmgAmount;
            if (health <= 0)
            {
                health = 0;
                IsAlive = false;

            }

        }
        
    }

    public class Player : Entity
    {
        public Player()
        {

        }
        ~Player()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("\nPlayer " + name + " was reset!\n");

        }


        private string playerClass;
        public int healthPotion = 5;
        public int manaPotion = 4;

        /* public int Stamina
          {
              get
              {
                  return stamina;
              }

              set
              {
                  if(value<0)
                  {
                      this.stamina = 0;
                  }
              }
          }*/

        public void SwordBash(int dmg)
        {
            Console.WriteLine("\n*****************************************\n");
            Console.WriteLine("(Special Attack) You use your sword to bash the enemy!");
            Console.WriteLine("You deal: " + dmg + " damage!");
            stamina -= 20;
            attackCount++;
        }
        public void BasicAttack(int dmg)
        {
            Console.WriteLine("\n*****************************************\n");
            Console.WriteLine("You use a basic attack on the enemy!");
            Console.WriteLine("You deal: " + dmg + " damage!");
            attackCount++;

        }

        public void EscapeSuccess()
        {
            Console.WriteLine("\n*****************************************");
            Console.WriteLine("You successfully escape from the enemy!");
        }

        public void EscapeFail()
        {
            Console.WriteLine("\n*****************************************");
            Console.WriteLine("You fail to escape the enemy!");
        }
        public void Defend()
        {
            Console.WriteLine("\n*****************************************");
            Console.WriteLine("\nYou prepare to defend yourself! " +
                "All damage dealt to you will be halved!");
        }

        public void LightningStorm(int dmg)
        {

            if (mana > 0)
            {
                Console.WriteLine("\n(Special attack) " +
                    "You summon a massive lightning storm to damage your enemy! You use 10 mana");
                Console.WriteLine("You deal: " + dmg + " damage!");
                mana -= 10;
                attackCount++;
            }
            else
            {
                Console.WriteLine("You are out of mana!");

            }
        }

        public void UseHealthPotion()
        {
            if (health + 15 < maxHealth)
            {
                Console.WriteLine("\nYou use a health potion to heal yourself! " +
                    "You gain 15 health points!");
                health += 15;
                healthPotion--;
            }
            else
            {
                health = maxHealth;
                healthPotion--;
            }

        }

        public void UseManaPotion()
        {
            if (mana + 10 < maxMana)
            {
                Console.WriteLine("\nYou use a mana potion to restore your mana! " +
                    "You gain 10 mana points!");
                mana += 10;
                manaPotion--;

            }
            else
            {
                mana = maxMana;
                manaPotion--;
            }

        }
        public string PlayerClass
        {
            get
            {
                return playerClass;
            }

            set
            {
                if (value == "Warrior")
                {
                    this.health = 100;
                    this.strength = 5;
                    this.intelligence = 1;
                    this.stamina = 60;
                    this.manaPotion = 0;
                }
                else if (value == "Mage")
                {
                    this.health = 60;
                    this.strength = 1;
                    this.intelligence = 5;
                    this.mana = 50;
                }
                playerClass = value;
            }
        }
    }

    class RangedWeapons
    {

        public virtual void UseRangedWeapon(int dmg)
        {
            Console.WriteLine("You use the ranged weapon!");
            Console.WriteLine("You deal: " + dmg + " amount of damage!");
        }
    }

    class Sling : RangedWeapons
    {
        private string slingName;
        private string slingAmmo;
        public Sling(string slingName, string slingAmmo)
        {
            this.slingName = slingName;
            this.slingAmmo = slingAmmo;
        }

        public override void UseRangedWeapon(int dmg)
        {
            Console.WriteLine("\nYou shoot " + slingAmmo + " with the " + slingName + " at your opponent!");
            base.UseRangedWeapon(dmg);
        }
    }

    class Enemy : Entity
    {
        private string enemyName;
        public Enemy()
        {

        }

        public void EnemyAttack()
        {
            Console.WriteLine("The enemy attacks");
        }
        public void DragonAttack(int dmg)
        {
            EnemyAttack();
            Console.WriteLine("The dragon spits his fire breath at you!");
            Console.WriteLine("The dragon does: " + dmg + " damage!\n");
            attackCount++;
        }

        public void DragonSpecialAttack(int dmg)
        {
            EnemyAttack();
            Console.WriteLine("The dragon summons a mighty fire meteor and rains down fiery rain upon you!");
            Console.WriteLine("The dragon does: " + dmg + " damage!\n");
            attackCount++;
        }

        public void OrcAttack(int dmg)
        {
            EnemyAttack();
            Console.WriteLine("The orc lunges at you with a wooden club!");
            Console.WriteLine("The orc does: " + dmg + " damage!\n");
            attackCount++;
        }
        public string EnemyName
        {
            get
            {
                return enemyName;
            }

            set
            {
                if (value == "Dragon")
                {
                    health = 120;
                }
                else if (value == "Orc")
                {
                    health = 80;
                }
                enemyName = value;
            }
        }

    }
}
