namespace DataLayers.Contract
{
    public class SuccessResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Item { get; set; }
    }
    

}