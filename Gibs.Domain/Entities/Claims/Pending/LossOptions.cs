namespace Gibs.Domain.Entities
{
	public class LossType
	{
		public required string Id { get; set; }
		public required string Name { get; set; }
	}

    public class LossCause
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        //public string ProductID { get; set; }
        //public Product Product { get; set; }
    }

    public class LossNature
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        //public string ProductID { get; set; }
        //public Product Product { get; set; }
    }
}
