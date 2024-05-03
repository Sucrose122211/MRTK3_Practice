using Unity.VisualScripting;

namespace data {
    public class TestData: ManagableData
    {
        public int Trial;
        public bool Success;

        public TestData()
        {
            name = typeof(TestData).FullName;
        }

        public override void Pack()
        {
            Add(nameof(Trial), Trial.ToString());
            Add(nameof(Success), Success.ToString());
        }
        public override void UnPack()
        {
            Trial = int.Parse(Datas[nameof(Trial)]);
            Success = bool.Parse(Datas[nameof(Success)]);
        }
    }
}
