namespace Contracts.Constants
{
    public static class ValidationMessages
    {
        public const string PasswordsLengthMessage = "Must be between 5 and 255 characters.";
        public const string ConfirmationPasswordsMessage = "Password and ConfirmPassword do not match.";

        public const string InCorrectPasswordsMessage = "Incorrect password";
        public const string UserDoesNotHaveEnoughPermission = "You don't have enough permission";

        public static string UserWithEmailDoesNotExist(string email) => $"User with email: {email} does not exist";
        public static string UserWithIdDoesNotExist(string id) => $"User with id: {id} does not exist";
        public static string RecordIdDoesNotExist(int id) => $"Record with id: {id} does not exist";
        public static string UserWithEmailOrUserNameAlreadyExist(string email, string username) => $"User with email: {email} or username: {username} already exist";
    }
}
