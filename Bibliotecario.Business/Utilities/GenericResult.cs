namespace Bibliotecario.Business.Utilities
{
    public  class GenericResult<T>
    {
        public GenericResult(bool isSucces= true)
        {
            IsSucces = isSucces;
        }

        public GenericResult(bool isSucces, T data)
        {
            IsSucces = isSucces;
            Data = data;
        }

        public GenericResult(bool isSucces, string message)
        {
            IsSucces = isSucces;
            Message = message;
        }

        public string Message { get; set; }
        public T Data { get; set; }

        public bool IsSucces { get; set; }
    }
}
