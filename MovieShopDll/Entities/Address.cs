namespace MovieShopDll.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        //public Customer Customer { get; set; }
    }
}