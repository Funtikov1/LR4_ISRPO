using System;
namespace ShopLibrary.Exceptions
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message)
        {
        }
    }
}