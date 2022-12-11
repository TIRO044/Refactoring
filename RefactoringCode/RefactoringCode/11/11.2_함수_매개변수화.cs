using System;
namespace RefactoringCode._11
{
    public class _11_2_Before
    {
        public float BaseCharge(float usage)
        {
            if (usage < 0) return 0;
            float amount =
                BottomBand(usage) * 0.3f
                + MiddleBand(usage) * 0.5f
                + TopBand(usage) * 0.7f;

            return amount;
        }

        public float BottomBand(float usage)
        {
            return Math.Min(usage, 100);
        }

        public float MiddleBand(float usage)
        {
            return usage > 100 ? Math.Min(usage, 200) - 100 : 0;
        }

        public float TopBand(float usage)
        {
            return usage > 200 ? usage - 200 : 0;
        }
    }

    public class _11_2_After
    {
        public float BaseCharge(float usage)
        {
            if (usage < 0) return 0;
            float amount =
                WithinBand(usage, 0, 100) * 0.3f +
                WithinBand(usage, 100, 200) * 0.5f +
                WithinBand(usage, 200, float.MaxValue) * 0.7f;

            return amount;
        }

        public float WithinBand(float usage, float bottom, float top)
        {
            return usage > bottom ? Math.Min(usage, top) - bottom : 0;
        }
    }
}

