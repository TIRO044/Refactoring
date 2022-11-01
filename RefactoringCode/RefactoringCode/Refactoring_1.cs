using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringCode
{
    internal class Refactoring_1
    {
        public class Player
        {
            public string Name;
            public string Type;
        }

        public class Invoice
        {
            public List<Performance> Performances = new();
        }

        public class Performance
        {
            public int PlayerId;
            public double Audiance;
        }

        public void Statement(Invoice invoice, Dictionary<int, Player> plays)
        {
            var totalAmount = 0;
            double volumneCredits = 0;
            var result = "청구 내역";
            const string format = $"";

            foreach (var pref in invoice.Performances)
            {
                double thisAmount = AmountFor(pref, PlayFor(plays, pref));

                volumneCredits += Math.Max(pref.Audiance - 30d, 0d);
                if (PlayFor(plays, pref).Type == "comedy")
                {
                    volumneCredits += Math.Floor(pref.Audiance / 5);
                }

                result += $"{PlayFor(plays, pref).Name}:{thisAmount}:{pref.Audiance}석";
            }
        }

        public Player PlayFor(Dictionary<int, Player> plays, Performance perf)
        {
            return plays[perf.PlayerId];
        }

        public double AmountFor(Performance aPerformance, Player player)
        {
            double result = 0;
            switch (player.Type)
            {
                case "tragedy":
                    result = 3000;
                    if (aPerformance.Audiance > 30)
                    {
                        result += 1000 * (aPerformance.Audiance);
                    }
                    break;
                case "comedy":
                    result = 4000;
                    if (aPerformance.Audiance > 20)
                    {
                        result += 1000 + 500 * (aPerformance.Audiance);
                    }
                    break;
            }

            return result;
        }
    }
}
