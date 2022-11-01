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
                var player = plays[pref.PlayerId];
                double thisAmount = 0;

                switch (player.Type)
                {
                    case "tragedy":
                        thisAmount = 3000;
                        if (pref.Audiance > 30)
                        {
                            thisAmount += 1000 * (pref.Audiance);
                        }
                        break;
                    case "comedy":
                        thisAmount = 4000;
                        if (pref.Audiance > 20)
                        {
                            thisAmount += 1000 + 500 * (pref.Audiance);
                        }
                        break;
                }

                volumneCredits += Math.Max(pref.Audiance - 30d, 0d);
                if (player.Type == "comedy")
                {
                    volumneCredits += Math.Floor(pref.Audiance / 5);
                }

                result += $"{player.Name}:{thisAmount}:{pref.Audiance}석";
            }
        }
    }
}
