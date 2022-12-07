using System;
using static RefactoringCode.Refactoring_1;

namespace RefactoringCode
{
    internal class PerformanceCalculator
    {
        public Performance Performance { get; private set; }
        public Player Player { get; private set; }

        public PerformanceCalculator(Performance pref, Dictionary<int, Player> players, Func<Dictionary<int, Player>, Performance, Player> aPlayer)
        {
            Performance = pref;
            Player = aPlayer.Invoke(players, Performance);
        }

        public double Amount
        {
            get {
                double result = 0;
                switch (Player.Type)
                {
                    case "tragedy":
                        result = 3000;
                        if (Performance.Audiance > 30)
                        {
                            result += 1000 * (Performance.Audiance);
                        }
                        break;
                    case "comedy":
                        result = 4000;
                        if (Performance.Audiance > 20)
                        {
                            result += 1000 + 500 * (Performance.Audiance);
                        }
                        break;
                }

                return result;
            }
        }

        public double VolumeCredits
        {
            get
            {
                double volumeCredits = 0;
                volumeCredits += Math.Max(Performance.Audiance - 30d, 0d);
                if (Performance.Player.Type == "comedy")
                {
                    volumeCredits += Math.Floor(Performance.Audiance / 5);
                }

                return volumeCredits;
            }
        }
    }

    internal class StatementUtil
    {
        public static StatementData CreateStatementData(Dictionary<int, Player> plays, Invoice invoice)
        {
            var statementData = new StatementData();
            statementData.Customer = invoice.Customer;
            statementData.Performances = EnrichPerformance(plays, invoice.Performances);
            statementData.TotalVolumeCredits = TotalVolumeCredits(statementData);
            statementData.TotalAmount = TotalAmount(statementData);

            return statementData;
        }

        public static List<Performance> EnrichPerformance(Dictionary<int, Player> player, List<Performance> performances)
        {
            var result = new List<Performance>();
            foreach (var performance in performances)
            {
                var performanceCaculator = new PerformanceCalculator(performance, player, PlayFor);

                var r = new Performance
                {
                    Player = performanceCaculator.Player,
                    Amount = performanceCaculator.Amount,
                    Audiance = performanceCaculator.VolumeCredits
                };
                result.Add(r);
            }

            return result;
        }

        public static Player PlayFor(Dictionary<int, Player> plays, Performance perf)
        {
            return plays[perf.PlayerId];
        }

        private static double TotalVolumeCredits(StatementData statementData)
        {
            double volumeCredits = 0;
            statementData.Performances.ForEach(pref => volumeCredits += pref.Audiance);
            return volumeCredits;
        }

        private static string TotalAmount(StatementData statementData)
        {
            string totalAmount = string.Empty;
            statementData.Performances.ForEach(pref => totalAmount += $"{pref.Player.Name}:{pref.Amount}:{pref.Audiance}석\n");
            return totalAmount;
        }
    }
}