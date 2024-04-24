using Unity.VisualScripting;

namespace data {
    public class TestData: ManagableData
    {
        public TestData()
        {
            name = typeof(TestData).FullName;
        }

        public override void Pack()
        {
            throw new System.NotImplementedException();
        }
        public override void UnPack()
        {
            throw new System.NotImplementedException();
        }
    }
}
