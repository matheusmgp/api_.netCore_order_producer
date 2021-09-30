namespace orderProducerAPI.Entities
{
    public class Order
    {
        public int OrderNumber { get; set; }

        public string ItemName { get; set; }

        public string Cnpj { get; set; }

        public int Quantity { get; set; }

       
    }
}
