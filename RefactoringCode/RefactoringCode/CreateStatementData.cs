using System;
using static RefactoringCode.Refactoring_1;

namespace RefactoringCode
{
    internal class PerformanceCaculator
    {
        public Performance Performance { get; private set; }
        public Player Player { get; private set; }

        public void Init(Performance pref, Dictionary<int, Player> players, Func<Dictionary<int, Player>, Performance, Player> aPlayer, Func<Performance, double> a)
        {
            Performance = pref;
            Player = aPlayer.Invoke(players, Performance);
        }
    }

    internal class StatementUtil
    {
        private static PerformanceCaculator performanceCalculator = new PerformanceCaculator();

        public static StatementData CreateStatementData(Dictionary<int, Player> plays, Invoice invoice)
        {
            var statementData = new StatementData();
            statementData.Customer = invoice.Customer;
            statementData.Performances = EnrichPerformace(plays, invoice.Performances);
            statementData.TotalVolumeCredits = TotalVolumeCredits(statementData);
            statementData.TotalAmount = TotalAmount(statementData);

            return statementData;
        }

        public static List<Performance> EnrichPerformace(Dictionary<int, Player> player, List<Performance> performances)
        {
            var result = new List<Performance>();
            foreach (var performance in performances)
            {
                performanceCalculator.Init(performance, player, PlayFor);

                var r = new Performance();
                r.Player = performanceCalculator.Player;
                r.Amount = AmountFor(performance);
                r.Audiance = VolumeCreditsFor(performance, player);
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

        private static double VolumeCreditsFor(Performance pref, Dictionary<int, Player> plays)
        {
            double volumeCredits = 0;
            volumeCredits += Math.Max(pref.Audiance - 30d, 0d);
            if (pref.Player.Type == "comedy")
            {
                volumeCredits += Math.Floor(pref.Audiance / 5);
            }

            return volumeCredits;
        }

        private static double AmountFor(Performance aPerformance)
        {
            double result = 0;
            switch (aPerformance.Player.Type)
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