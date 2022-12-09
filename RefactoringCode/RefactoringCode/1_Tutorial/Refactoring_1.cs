
using System.Numerics;

namespace RefactoringCode
{
    internal class Refactoring_1
    {
        // 클래스들은 파일 별로 분리되었다고 걍 치자고
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
            public double TotalVolumeCredits;
            public string TotalAmount;
        }

        public void Statement(Invoice invoice)
        {
            var plays = new Dictionary<int, Player>(); // 지금 이새끼 떄문에 존나 헷갈려 시
            var result = RenderPlainText(StatementUtil.CreateStatementData(plays, invoice));
        }

        public string RenderPlainText(StatementData statementData)
        {
            var result = $"청구 내역 {statementData.Customer}\n";
            string format = $"";

            foreach (var pref in statementData.Performances)
            {
                result += $"{pref.Player.Name}:{pref.Amount}:{pref.Audiance}석";
            }

            result += $"총 {statementData.TotalAmount}";
            result += $"적립 포인트 {statementData.TotalVolumeCredits}";

            return result;
        }

    }
}