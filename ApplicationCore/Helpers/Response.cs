using System;
namespace ApplicationCore.Helpers
{
	public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public object? HelperData { get; set; }
    }
}

