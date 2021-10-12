namespace ProductService.Rest.Models.Resources
{
    #region Public Class Definition
    public abstract class BaseResponse
    {
        #region Public Property Definition
        public bool Success { get; protected set; }
        #endregion

        #region Public Constructor Definition
        public BaseResponse(bool success)
        {
            Success = success;
        }
        #endregion
    }
    #endregion
}