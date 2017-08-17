using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Security;
using Photo.Business.DataProvider;
using System.Linq;

namespace Photo.Business.Entities.Security
{
	/// <summary>
	/// class to perform activities realted to UserInfo object
	/// </summary>
	public class UserController
	{
		#region Private Methods

		private static void Validate(UserInfo user)
		{
			if (user.ID == Guid.Empty)
				throw new Exception("Invalid User Content.");
		}

		/// <summary>
		/// Saves the object in the database by either inserting it if it was a newly created object or updating it if it has an exisiting record
		/// </summary>
		/// <param name="user"></param>
		/// <param name="transaction">A SQL transaction to be used while performing this operation, so that it can be either commited or rolled-back with all the other calls when called from a batch operation function</param>
		private static void Save(UserInfo user, IDbTransaction transaction)
		{
			Validate(user);
			DataProviderManager.Provider.SaveUser(user, transaction);
		}

		/// <summary>
		/// Saves the object in the database by either inserting it if it was a newly created object or updating it if it has an exisiting record
		/// </summary>
		/// <param name="user"></param>
		private static void Save(UserInfo user)
		{
			IDbTransaction transaction = DataProviderManager.Provider.NewDataTransaction;
			IDbConnection connection = transaction.Connection;

			try
			{
				Save(user, transaction);
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
			finally
			{
				connection.Close();
			}
		}

		#endregion


		#region Public Methods

	    public static UserInfo Create(string firstNameEn, string lastNameEn, string username,
	        string password, string email, List<RoleInfo> rolesList, IDbTransaction transaction)
	    {
	        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
	            throw new Exception("Missing user information");

	        var membershipUser = Membership.CreateUser(username, password, email);

	        var user = new UserInfo
	        {
	            MembershipUser = membershipUser,
	            FirstNameEN = firstNameEn,
	            LastNameEN = lastNameEn
	        };

	        Save(user, transaction);

	        if (rolesList == null || rolesList.Count <= 0) return user;
	        foreach (RoleInfo role in rolesList)
	            RoleController.Instance.AddUserToRole(user, role);

	        return user;
	    }

	    /// <summary>
		/// Method to generate random password
		/// </summary>
		/// <param name="length">int</param>
		/// <param name="numberOfNonAlphanumericCharacters">int</param>
		/// <returns>string</returns>
		public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
		{
			return Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
		}

		/// <summary>
		/// Get all CK users
		/// </summary>
		/// <returns></returns>
		public static List<UserInfo> UserSelectAllByUserType()
		{
			return DataProviderManager.Provider.UserSelectAllByUserType();
		}


		/// <summary>
		/// Method to get user
		/// </summary>
		/// <param name="userID">object</param>
		/// <returns>UserInfo</returns>
		public static UserInfo GetByID(Guid userID)
		{
			return DataProviderManager.Provider.UserGetByID(userID).FirstOrDefault();
		}

		/// <summary>
		/// Gets the user based on the provided userName
		/// </summary>
		/// <param name="userName">string</param>
		/// <returns>UserInfo</returns>
		public static UserInfo GetByUserName(string userName)
		{
			return DataProviderManager.Provider.UserGetByUserName(userName).FirstOrDefault();
		}

		/// <summary>
		/// Gets the user based on the provided Reference Code
		/// </summary>
		/// <param name="referenceCode">string</param>
		/// <returns>UserInfo</returns>
		public static UserInfo GetByReferenceCode(string referenceCode)
		{
			//TODO CBO
			//return CBO.FillObject<UserInfo>(DataProviderManager.Provider.GetUserByReferenceCode(referenceCode));
			return null;
		}

		/// <summary>
		/// Gets the currently logged-in user (if any)
		/// </summary>
		/// <returns>UserInfo</returns>
		public static UserInfo GetCurrentUser()
		{
			UserInfo user = null;
			MembershipUser membershipUser = Membership.GetUser();
			if (membershipUser != null)
			{				
				user = GetByID(new Guid(membershipUser.ProviderUserKey.ToString()));
				if (user != null)
					user.MembershipUser = membershipUser;
			}

			return user;
		}

		/// <summary>
		/// Method to update user
		/// </summary>
		/// <param name="user">UserInfo</param>
		public static void Update(UserInfo user)
		{
			Membership.UpdateUser(user.MembershipUser);
			Save(user);
		}

	    /// <summary>
	    /// Method to validate user login
	    /// </summary>
	    /// <param name="username">string</param>
	    /// <param name="password">string</param>
	    /// <returns>bool</returns>
	    public static bool Validate(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				return false;

			UserInfo user = GetByUserName(username);
			if (user == null)
				return false;

			return Membership.ValidateUser(username, password);
		}

		/// <summary>
		/// Method to delete user (logically). It will simply set the IsApproved property to false
		/// </summary>
		/// <param name="user">UserInfo</param>
		/// <returns>bool</returns>
		public static void Delete(UserInfo user)
		{
			if (user != null)
			{
				//if (user.RolesList != null && user.RolesList.Count > 0)
				//	RoleController.RemoveUserFromRoles(user.UserName, user.RolesList);

				user.MembershipUser.IsApproved = false;
				Membership.UpdateUser(user.MembershipUser);
			}
		}

		/// <summary>
		/// Method to change user's password
		/// </summary>
		/// <param name="user">UserInfo</param>
		/// <param name="oldPassword">string</param>
		/// <param name="newPassword">string</param>
		/// <returns>bool</returns>
		public static bool ChangePassword(UserInfo user, string oldPassword, string newPassword)
		{
			return user.MembershipUser.ChangePassword(oldPassword, newPassword);
		}

		/// <summary>
		/// Method to check if user account is locked
		/// </summary>
		/// <returns></returns>
		public static bool IsActive(UserInfo user)
		{
			if (user != null)
			{
				MembershipUser membershipUser = Membership.GetUser(user.UserName);
				return (membershipUser != null && !membershipUser.IsLockedOut && membershipUser.IsApproved);
			}

			return false;
		}

		/// <summary>
		/// Check whether the given reference code is already exists
		/// </summary>
		/// <param name="referenceCode"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static bool IsReferenceCodeValid(string referenceCode, Guid? userId)
		{
			UserInfo user = GetByReferenceCode(referenceCode);

			if (user != null && (!userId.HasValue || user.ID != userId))
				return false;

			return true;
		}

		#endregion
	}
}