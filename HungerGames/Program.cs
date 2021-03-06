﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Players;

namespace HungerGames
{
    class Program
    {
        static StreamWriter log = new StreamWriter("log.txt", false);

        private static int PlayerMultiplier = 1;
        private static Random random = new Random();
        private static int MaxRounds = 100000;
        private static int NumberOfSets = 1000;

        static void Main(string[] args)
        {
            var totals = new Dictionary<string, int>();
            for (var s = 0; s < NumberOfSets; s++)
            {
                //init players
                var playerAssembly = Assembly.GetAssembly(typeof(IPlayer));
                var activePlayers = new List<ActivePlayer>();
                foreach (var player in playerAssembly.GetTypes().Where(t => t.GetInterface("IPlayer") != null))
                {
                    for (var p = 0; p < PlayerMultiplier; p++)
                    {
                        activePlayers.Add(new ActivePlayer
                            {
                                Food = 300,
                                Hunts = 0,
                                Slacks = 0,
                                Player = (IPlayer)Activator.CreateInstance(player)
                            });
                    }
                }
                foreach (var player in activePlayers)
                {
                    player.Food *= activePlayers.Count - 1;
                }
                //while people have food
                var i = 1;
                var remainingPlayers = activePlayers.Where(p => p.Food > 0).ToList();
                while (remainingPlayers.Count > 1 && i < MaxRounds)
                {
                    var playerChoices = new List<char[]>();
                    //Log("Round {0}", i);
                    //randomly pair players for hunts
                    var randomArray = remainingPlayers.OrderBy(p => random.Next()).ToList();
                    var m = random.Next(1, (randomArray.Count * (randomArray.Count - 1)));
                    //get each player's choices
                    foreach (var player in remainingPlayers)
                    {
                        var repArray = randomArray.Where(p => p != player).Select(p => p.Reputation).ToArray();
                        var choices = player.Player.HuntChoices(i, player.Food, player.Reputation, m, repArray);
                        playerChoices.Add(choices);
                    }
                    //eval matches
                    var totalHuntsThisRound = 0;
                    for (var j = 0; j < remainingPlayers.Count; j++)
                    {
                        var player = remainingPlayers[j];
                        var choices = playerChoices[j];
                        var playerRandomIndex = randomArray.IndexOf(player);
                        var outcomes = new List<int>();
                        for (var k = 0; k < randomArray.Count; k++)
                        {
                            var choiceIndex = k + (k > playerRandomIndex ? -1 : 0);
                            var otherPlayer = randomArray[k];
                            //skip self
                            if (otherPlayer == player)
                                continue;
                            var playersChoice = choices[choiceIndex];

                            var otherPlayersChoiceIndex = remainingPlayers.IndexOf(otherPlayer);
                            var otherPlayersChoices = playerChoices[otherPlayersChoiceIndex];
                            var otherPlayersRandomIndex = randomArray.IndexOf(otherPlayer);
                            var otherPlayersPlayerRandomIndex = playerRandomIndex +
                                                                (playerRandomIndex > otherPlayersRandomIndex ? -1 : 0);
                            var otherPlayersChoice = otherPlayersChoices[otherPlayersPlayerRandomIndex];

                            var outcome = 0;
                            if (playersChoice == 'h')
                            {
                                totalHuntsThisRound++;
                                player.Hunts++;
                                outcome -= 6;
                                if (otherPlayersChoice == 'h')
                                {
                                    outcome += 6;
                                }
                                else
                                {
                                    outcome += 3;
                                }
                            }
                            else
                            {
                                player.Slacks++;
                                outcome -= 2;
                                if (otherPlayersChoice == 'h')
                                {
                                    outcome += 3;
                                }
                                else
                                {
                                    //no one hunted
                                    outcome += 0;
                                }
                            }
                            outcomes.Add(outcome);
                            player.Food += outcome;
                        }
                        player.Player.HuntOutcomes(outcomes.ToArray());
                    }
                    //bonus food
                    //Log("Round results");
                    foreach (var player in remainingPlayers)
                    {
                        if (totalHuntsThisRound >= m)
                        {
                            player.Player.RoundEnd(2 * (remainingPlayers.Count - 1), m, remainingPlayers.Count);
                            player.Food += 2 * (remainingPlayers.Count - 1);
                        }
                        else
                        {
                            player.Player.RoundEnd(0, m, remainingPlayers.Count);
                        }
                        //Log("Player {0}: Food {1}, Hunts {2}, Slacks {3}, Reputation {4}", player.Player.GetType().FullName, player.Food, player.Hunts, player.Slacks, player.Reputation);
                    }
                    if (totalHuntsThisRound >= m)
                    {
                        //Log("Got bonus food");
                    }
                    else
                    {
                        //Log("Didn't get bonus food");
                    }
                    i++;
                    var deadPlayers = remainingPlayers.Where(p => p.Food <= 0);
                    if (deadPlayers.Count() > 0)
                    {
                        //Log("Dead Players:");
                        foreach (var deadPlayer in deadPlayers)
                        {
                            //Log("{0}", deadPlayer.Player.GetType().FullName);
                        }
                    }
                    remainingPlayers = activePlayers.Where(p => p.Food > 0).ToList();
                    //Log("");
                }
                //print winner(s)
                Log("Winning Player(s)");
                foreach (var player in remainingPlayers.OrderByDescending(p => p.Food))
                {
                    Log("{0}, Food: {1}", player.Player.GetType().FullName, player.Food);
                    var key = player.Player.GetType().FullName;
                    if (totals.ContainsKey(key))
                    {
                        totals[key]++;
                    }
                    else
                    {
                        totals.Add(key, 1);
                    }
                }
            }
            foreach (var winner in totals.OrderByDescending(w => w.Value))
            {
                Log("{0} won {1} times", winner.Key, winner.Value);
            }
            log.Close();
            Console.ReadLine();
        }

        static void Log(string s)
        {
            Console.WriteLine(s);
            log.WriteLine(s);
        }

        static void Log(string s, params object[] objs)
        {
            Console.WriteLine(s, objs);
            log.WriteLine(s, objs);
        }
    }

    class ActivePlayer
    {
        public int Food { get; set; }
        public int Hunts { get; set; }
        public int Slacks { get; set; }
        public double Reputation
        {
            get
            {
                if (Hunts + Slacks == 0) return 0;
                else return (Hunts / (double)(Hunts + Slacks));
            }
        }
        public IPlayer Player { get; set; }
    }
}
