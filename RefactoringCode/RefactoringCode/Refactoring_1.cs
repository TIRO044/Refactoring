
using static RefactoringCode.Refactoring_1;

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
            public string Customer;
            public List<Performance> Performances = new();
        }

        public class Performance
        {
            public int PlayerId;
            public double Audiance;
        }

        public struct StatementData
        {
            public string Customer;
            public List<Performance> performances;
        }

        public void Statement(Invoice invoice)
        {
            var statementData = new StatementData();
            statementData.Customer = invoice.Customer;
            statementData.performances = new List<Performance>(invoice.Performances);

            var plays = new Dictionary<int, Player>();
            var result = RenderPlainText(statementData, plays, invoice);
        }

        public string RenderPlainText(StatementData statementData, Dictionary<int, Player> plays, Invoice invoice)
        {
            var result = $"청구 내역 {statementData.Customer}\n";
            string format = $"";

            foreach (var pref in statementData.performances)
            {
                double thisAmount = AmountFor(pref);
                result += $"{PlayFor(plays, pref).Name}:{thisAmount}:{pref.Audiance}석";
            }

            result += TotalAmount(plays, invoice);
            result += $"적립 포인트 {TotalVolumeCredits(plays, invoice)}";
            return result;
        }

        private double TotalVolumeCredits(Dictionary<int, Player> plays, Invoice invoice)
        {
            double volumeCredits = 0;
            foreach (var pref in invoice.Performances)
            {
                volumeCredits += VolumeCreditsFor(pref, plays);
            }

            return volumeCredits;
        }

        private string TotalAmount(Dictionary<int, Player> plays, Invoice invoice)
        {
            string totalAmount = string.Empty;
            foreach (var pref in invoice.Performances)
            {
                double thisAmount = AmountFor(pref);
                totalAmount += $"{PlayFor(plays, pref).Name}:{thisAmount}:{pref.Audiance}석";
            }

            return totalAmount;
        }

        public double VolumeCreditsFor(Performance pref, Dictionary<int, Player> plays)
        {
            double volumeCredits = 0;
            volumeCredits += Math.Max(pref.Audiance - 30d, 0d);
            if (PlayFor(plays, pref).Type == "comedy")
            {
                volumeCredits += Math.Floor(pref.Audiance / 5);
            }

            return volumeCredits;
        }

        public Player PlayFor(Dictionary<int, Player> plays, Performance perf)
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