namespace ProductService.Database.Constant
{
    #region Public Enum Definition
    public enum EntityStatus : byte
    {
        Inactive = 0,
        Active = 1,
        Pending = 2,
        Suspend = 3,
        Delete = 4,
        // All = Inactive | Active,
        // Everything = Inactive | Active | Delete
    }
    #endregion
}