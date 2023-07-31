namespace ticket_management.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string merrorMessage) : base(merrorMessage) { }

        public EntityNotFoundException(string merrorMessage, Exception innerException) : base(merrorMessage, innerException) { }

        public EntityNotFoundException(long entityId, string entityName) : base(FormattableString.Invariant($"'{entityName}' with ID '{entityId}' was not found.")) { }

        public EntityNotFoundException(string name, string entityName) : base(FormattableString.Invariant($"'{entityName}' with name '{name}' was not found.")) { }
    }
}