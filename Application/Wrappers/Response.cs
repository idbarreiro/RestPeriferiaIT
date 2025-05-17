﻿namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Success = false;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }               
    }
    
}
