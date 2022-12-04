
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
            public Player Player;
            public double Amount;
        }

        public struct StatementData
        {
            public string Customer;
            public List<Performance> Performances;
        }

        public void Statement(Invoice invoice)
        {
            var plays = new Dictionary<int, Player>(); // 지금 이새끼 떄문에 존나 헷갈려 시

            var statementData = new StatementData();
            statementData.Customer = invoice.Customer;
            statementData.Performances = EnrichPerformace(plays, invoice.Performances);
            var result = RenderPlainText(statementData, plays, invoice);

            List<Performance> EnrichPerformace(Dictionary<int, Player> player, List<Performance> performances)
            {
                var result = new List<Performance>();
                foreach(var preformance in performances)
                {
                    var r = new Performance();
                    r.Player = PlayFor(player, preformance);
                    r.Amount = AmountFor(preformance);
                    result.Add(r);
                }

                return result;
            }

            Player PlayFor(Dictionary<int, Player> plays, Performance perf)
            {
                return plays[perf.PlayerId];
            }
        }

        public string RenderPlainText(StatementData statementData, Dictionary<int, Player> plays, Invoice invoice)
        {
            var result = $"청구 내역 {statementData.Customer}\n";
            string format = $"";

            foreach (var pref in statementData.Performances)
            {
                result += $"{pref.Player.Name}:{pref.Amount}:{pref.Audiance}석";
            }

            result += TotalAmount(statementData);
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

        private string TotalAmount(StatementData statementData)
        {
            string totalAmount = string.Empty;
            foreach (var pref in statementData.Performances)
            {
                totalAmount += $"{pref.Player.Name}:{pref.Amount}:{pref.Audiance}석";
            }

            return totalAmount;
        }

        public double VolumeCreditsFor(Performance pref, Dictionary<int, Player> plays)
        {
            double volumeCredits = 0;
            volumeCredits += Math.Max(pref.Audiance - 30d, 0d);
            if (pref.Player.Type == "comedy")
            {
                volumeCredits += Math.Floor(pref.Audiance / 5);
            }

            return volumeCredits;
        }


        public double AmountFor(Performance aPerformance)
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