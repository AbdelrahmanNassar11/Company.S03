namespace Company.S03.PL.Services
{
    public interface ITransientServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
