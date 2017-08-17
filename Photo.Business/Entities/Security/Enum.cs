namespace Photo.Business.Entities.Security
{
	/// <summary>
	/// An enumeration for the actions a user can perform based on the assigned roles
	/// </summary>
	public enum UserAction
	{
		UserCreate = 1,
		ChangeMyPassword = 2,
		UserManage = 3,
        SubscriptionView = 4,
        SubscriptionManage = 5,
    }
}