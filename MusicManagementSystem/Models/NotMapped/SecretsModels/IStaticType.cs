namespace MusicManagementSystem.Models.NotMapped.SecretsModels
{
    //TODO think about changing to IStaticType<T> and default realization
    public interface IStaticType
    {
        public Type GetStaticType { get; }
    }
}
