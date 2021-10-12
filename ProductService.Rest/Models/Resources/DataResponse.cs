namespace ProductService.Rest.Models.Resources
{
    #region Public Class Definition
    public class DataResponse<T> : BaseResponse
    {
        #region Public Property Definition
        public T Data { get; protected set; }
        public int? TotalCount { get; protected set; }
        #endregion

        #region Public Constructor Definition
        public DataResponse(T data) : this(data, null) { }

        public DataResponse(T data, int? totalCount) : base(true)
        {
            Data = data;
            TotalCount = totalCount;
        }
        #endregion
    }
    #endregion
}