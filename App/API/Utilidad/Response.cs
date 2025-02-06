namespace Utilidad
{
    public class Response<T>
    {
        public bool status {  get; set; }

        public T value { get; set; }

        public string mensaje { get; set; }
    }
}
