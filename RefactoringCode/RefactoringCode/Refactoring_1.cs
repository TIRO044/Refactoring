

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

        private Dictionary<int, Player> plays;
        public void Statement(Invoice invoice)
        {
            var totalAmount = 0;
            
            var result = "청구 내역";
            string format = $"";

            foreach (var pref in invoice.Performances)
            {
                double thisAmount = AmountFor(pref);
                result += $"{PlayFor(pref).Name}:{thisAmount}:{pref.Audiance}석";
            }

            double volumeCredits =  0;
            foreach (var pref in invoice.Performances)
            {
                volumeCredits += VolumeCreditsFor(pref, plays);
            }
        }

        public double VolumeCreditsFor(Performance pref, Dictionary<int, Player> plays)
        {
            double volumeCredits = 0;
            volumeCredits += Math.Max(pref.Audiance - 30d, 0d);
            if (PlayFor(pref).Type == "comedy")
            {
                volumeCredits += Math.Floor(pref.Audiance / 5);
            }

            return volumeCredits;
        }

        public Player PlayFor(Performance perf)
        {
            return plays[perf.PlayerId];
        }

        public double AmountFor(Performance aPerformance)
        {
            double result = 0;
            switch (PlayFor(aPerformance).Type)
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