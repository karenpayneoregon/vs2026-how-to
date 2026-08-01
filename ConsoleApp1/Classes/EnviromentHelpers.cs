using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace ConsoleApp1.Classes;

public static class EnviromentHelpers
{
    /// <summary>
    /// Retrieves the first name of the currently logged-in user.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> representing the first name of the current user.
    /// If the first name cannot be determined, the username is returned instead.
    /// </returns>
    /// <remarks>
    /// This method attempts to determine the user's first name by:
    /// <list type="number">
    /// <item>Checking the given name of the user in the domain or local machine.</item>
    /// <item>Extracting the first name from the display name if available.</item>
    /// <item>Falling back to the username if no other information is available.</item>
    /// </list>
    /// The important limitation is that Windows may simply not store the user’s first name.
    /// For local accounts, GivenName is normally null. In that case, the only reliable choices
    /// are to use the account name or ask the user to supply their preferred name.
    /// </remarks>
    /// <exception cref="System.Security.Principal.IdentityNotMappedException">
    /// Thrown if the current user identity cannot be resolved.
    /// </exception>
    /// <exception cref="System.DirectoryServices.AccountManagement.PrincipalServerDownException">
    /// Thrown if the domain server is unavailable when attempting to retrieve domain user information.
    /// </exception>
    /// <exception cref="System.InvalidOperationException">
    /// Thrown if an invalid operation occurs while accessing user information.
    /// </exception>
    public static string GetCurrentUserFirstName()
    {
        string identityName = WindowsIdentity.GetCurrent().Name ?? Environment.UserName;

        string userName = identityName.Contains('\\')
            ? identityName.Split('\\')[1]
            : identityName;

        UserPrincipal? user = FindDomainUser(userName) ?? FindLocalUser(userName);

        if (!string.IsNullOrWhiteSpace(user?.GivenName))
        {
            return user.GivenName;
        }

        if (!string.IsNullOrWhiteSpace(user?.DisplayName))
        {
            return ExtractFirstName(user.DisplayName);
        }

        return userName;
    }

    /// <summary>
    /// Attempts to find a domain user by their username.
    /// </summary>
    /// <param name="userName">
    /// A <see cref="string"/> representing the username of the domain user to locate.
    /// </param>
    /// <returns>
    /// A <see cref="UserPrincipal"/> object representing the domain user if found; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method uses a <see cref="PrincipalContext"/> to search for the user within the domain.
    /// If the domain server is unavailable or an invalid operation occurs, the method returns <c>null</c>.
    /// </remarks>
    /// <exception cref="System.DirectoryServices.AccountManagement.PrincipalServerDownException">
    /// Thrown if the domain server is unavailable during the search.
    /// </exception>
    /// <exception cref="System.InvalidOperationException">
    /// Thrown if an invalid operation occurs while accessing the domain context.
    /// </exception>
    private static UserPrincipal? FindDomainUser(string userName)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Domain);

            return UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName);
        }
        catch (PrincipalServerDownException)
        {
            return null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <summary>
    /// Attempts to find a local user by their username.
    /// </summary>
    /// <param name="userName">
    /// A <see cref="string"/> representing the username of the local user to locate.
    /// </param>
    /// <returns>
    /// A <see cref="UserPrincipal"/> object representing the local user if found; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method uses a <see cref="PrincipalContext"/> to search for the user within the local machine.
    /// If an error occurs during the search, the method returns <c>null</c>.
    /// </remarks>
    /// <exception cref="System.DirectoryServices.AccountManagement.PrincipalOperationException">
    /// Thrown if an error occurs during the operation on the local machine context.
    /// </exception>
    /// <exception cref="System.InvalidOperationException">
    /// Thrown if an invalid operation occurs while accessing the local machine context.
    /// </exception>
    private static UserPrincipal? FindLocalUser(string userName)
    {
        try
        {
            using var context = new PrincipalContext(ContextType.Machine);

            return UserPrincipal.FindByIdentity(
                context,
                IdentityType.SamAccountName,
                userName);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Extracts the first name from a given display name string.
    /// </summary>
    /// <param name="displayName">
    /// A <see cref="string"/> representing the display name of a user.
    /// The display name can be in formats such as "Last, First" or "First Last".
    /// </param>
    /// <returns>
    /// A <see cref="string"/> containing the extracted first name. 
    /// If the display name is in the "Last, First" format, the method returns the "First" part.
    /// If the display name is in the "First Last" format, the method returns the "First" part.
    /// If the display name does not match these formats, the original display name is returned.
    /// </returns>
    /// <remarks>
    /// This method handles two common formats for display names:
    /// <list type="bullet">
    /// <item>"Last, First" - The first name is extracted after the comma.</item>
    /// <item>"First Last" - The first name is extracted as the first word.</item>
    /// </list>
    /// </remarks>
    private static string ExtractFirstName(string displayName)
    {
        // Supports "Last, First" format.
        if (displayName.Contains(','))
        {
            string[] parts = displayName.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return parts.Length > 1
                ? parts[1].Split(' ')[0]
                : displayName;
        }

        // Supports "First Last" format.
        return displayName.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
    }
}
