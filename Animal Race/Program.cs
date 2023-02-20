/*

Program Author: Zachary Shamp

USM ID: 0010002778

Assignment: Program 2: The Tortoise and the Hare

Description:

Simulates a race between a tortoise and a hare utilizing inheritance to construct the animals in question.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Animal_Race
{
    abstract class RaceAnimal
    {
        private char[] track = new char[70];
        private char symbol;
        private int position;
        public string name;

        public RaceAnimal(string n, char s)
        {
            position = 0;
            symbol = s;
            name = n;
            for(int i = 0; i < 70; i++)
            {
                if (i == position)
                    track[i] = symbol;
                else
                    track[i] = '=';
            }
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 70; i++)
            {
                sb.Append(track[i]);
            }
            return sb.ToString();
        }
        protected void ChangePos(int steps)
        {
            position += steps;
            if (position < 0)
                position = 0;
            if (position > 69)
                position = 69;
            for (int i = 0; i < 70; i++)
            {
                if (i == position)
                    track[i] = symbol;
                else
                    track[i] = '-';
            }
        }

        public abstract void Move();
    }

    class Hare : RaceAnimal
    {
        public Hare(string n, char s) : base(n, s)
        {
            //name = n;
            //symbol = s;
        }

        override public void Move()
        {
            Random rng = new Random();
            int number = rng.Next(1, 101);
            if (number < 31)
                ChangePos(1); //Small hop
            else if (number > 30 && number < 51)
                ChangePos(0); //Sleep (wait isn't this pointless?)
            else if (number > 50 && number < 71)
                ChangePos(9); //Large hop
            else if (number > 70 && number < 91)
                ChangePos(-2); //Small slip
            else
                ChangePos(-12); //Small slip
        }
    }

    class Tortoise : RaceAnimal
    {
        public Tortoise(string n, char s) : base(n, s)
        {
            //name = n;
            //symbol = s;
        }

        override public void Move()
        {
            Random rng = new Random();
            int number = rng.Next(1, 101);
                if (number < 51)
                    ChangePos(3); //Dash
                else if (number > 50 && number < 81)
                    ChangePos(1); //Walk
                else
                    ChangePos(-6); //Slip
        }
    }

    class Race
    {
        private RaceAnimal animal1;
        private RaceAnimal animal2;
        private RaceAnimal winner;
        private bool over;
        public Race(RaceAnimal anim1, RaceAnimal anim2)
        {
            animal1 = anim1;
            animal2 = anim2;
            over = false;
            winner = null;
        }

        public void UpdateStatus()
        {
            if (animal1.ToString().IndexOf('h') == 69 && animal2.ToString().IndexOf('t') != 69)
            {
                over = true;
                winner = animal1;
            }
            else if (animal2.ToString().IndexOf('t') == 69 && animal1.ToString().IndexOf('h') != 69)
            {
                over = true;
                winner = animal2;
            }
            else if(animal1.ToString().IndexOf('h') == 69 && animal2.ToString().IndexOf('t') == 69)
            {
                over = true;
            }
        }

        public void Simulate()
        {
            while (!over)
            {
                Console.Clear();
                Console.WriteLine(animal1.ToString());
                Console.WriteLine(animal2.ToString());

                animal1.Move();
                animal2.Move();

                UpdateStatus();
                Thread.Sleep(1000);
            }
            if(winner == null)
            {
                Console.WriteLine("@==================================================================@\n" +
                                  "                        Tie! Nobody wins!\n" +
                                  "@==================================================================@\n");
            }
            else
            {
                Console.WriteLine("@==================================================================@\n" +
                                  "                        " + winner.name + " wins the race!\n" +
                                  "@==================================================================@\n");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            RaceAnimal anim1 = new Hare("The Hare", 'h');
            RaceAnimal anim2 = new Tortoise("The Tortoise", 't');
            Race race = new Race(anim1, anim2);

            race.Simulate();

            Console.WriteLine("Process completed successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}
