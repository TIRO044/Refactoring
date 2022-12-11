namespace RefactoringCode._6
{
    namespace RefactoringCode._6._Ex1
    {
        internal class _6_11_Before
        {
            Dictionary<string, int> priceList = new Dictionary<string, int>();

            public int Before(string orderString)
            {
                var orderData = orderString.Split("s+");
                var productPrice = priceList[orderData[0].Split("-")[1]];
                var orderPrice = int.Parse(orderData[0]) * productPrice;
                return orderPrice;
            }
        }

        // ---------------------- ---------------------- ---------------------- ---------------------- ---------------------- ----------------------
        // ---------------------- ---------------------- ---------------------- ---------------------- ---------------------- ----------------------
        // ---------------------- ---------------------- ---------------------- ---------------------- ---------------------- ----------------------

        // 추가 설명을 기제하기 위해서 변경점 만듬ㅋ
        internal class _6_11_After
        {
            class OrderDataClass
            {
                public string ProductID = string.Empty;
                public int Quantity;
            }

            Dictionary<string, int> priceList = new Dictionary<string, int>();

            public int After(string orderString)
            {
                var orderDataClass = ParseOrder(orderString);
                return Price(orderDataClass, priceList);

                OrderDataClass ParseOrder(string aString)
                {
                    var values = aString.Split("s+");
                    return new OrderDataClass { ProductID = values[1], Quantity = int.Parse(values[0]) };
                }

                int Price(OrderDataClass order, Dictionary<string, int> priceOrder)
                {
                    return order.Quantity * priceOrder[order.ProductID];
                }
            }
        }
    }
}
