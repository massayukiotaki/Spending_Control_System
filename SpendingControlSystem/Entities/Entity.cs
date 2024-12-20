namespace SpendingControlSystem.Entities
{
    public class Entity<T> : IAuditable
    {
        public T Id { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public string UsuarioInclusao { get; set; }
        public DateTime DataHoraAlteracao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public bool IsActive { get; set; }
    }
}
