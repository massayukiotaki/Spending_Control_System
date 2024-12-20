namespace SpendingControlSystem.Entities
{
    public interface IAuditable
    {
        DateTime DataHoraInclusao { get; set; }
        string UsuarioInclusao { get; set; }
        DateTime DataHoraAlteracao { get; set; }
        string UsuarioAlteracao { get; set; }
        bool IsActive { get; set; }
    }
}
