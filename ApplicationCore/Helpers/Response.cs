namespace ApplicationCore.Helpers
{
    public class Response
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "Ha ocurrido un error";
        public object? HelperData { get; set; }
    }
}

