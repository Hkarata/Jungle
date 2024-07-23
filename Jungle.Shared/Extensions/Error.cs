namespace Jungle.Shared.Extensions
{
    public record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

        public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");

        public static readonly Error ExistentCategory = new("Error.ExistentCategory", "The specified category already exists");

        public static readonly Error NoneExistentCategory = new("Error.NoneExistentCategory", "The specified category does not exist");

        public static readonly Error NoneExistentProduct = new("Error.NoneExistentProduct", "The specified product does not exist");

        public static readonly Error DuplicateTenantName = new Error("Error.DuplicateTenantName", "The specified tenant name already exists.");
        public static readonly Error DuplicateTenantInfo = new Error("Error.DuplicateTenantPhone", "The specified tenant phone or email already exists.");
    }
}
