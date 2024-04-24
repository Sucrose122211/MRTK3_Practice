
namespace data
{
    public class FittsData : ManagableData
    {
        public float x;         // x direction angle
        public float y;         // y direction angle
        public float Time;      // task time
        public float Width;     // Target width
        public float dist;      // Target Dist

        public FittsData()
        {
            name = typeof(FittsData).FullName;
        }

        public override void Pack()
        {
            Add(nameof(x), x.ToString());
            Add(nameof(y), y.ToString());
            Add(nameof(Time), Time.ToString());
            Add(nameof(Width), Width.ToString());
            Add(nameof(dist), dist.ToString());
        }

        public override void UnPack()
        {
            x = float.Parse(Datas[nameof(x)]); 
            y = float.Parse(Datas[nameof(y)]); 
            Time = float.Parse(Datas[nameof(Time)]);
            Width = float.Parse(Datas[nameof(Width)]);
            dist = float.Parse(Datas[nameof(dist)]);
        }
    }
}
