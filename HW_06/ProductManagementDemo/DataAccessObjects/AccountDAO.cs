using BusinessObjects;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountID)
        {
            AccountMember accountMember = new AccountMember();
            // Chỉ demo, bạn thay bằng logic truy vấn DB thật sau này
            if (accountID.Equals("PS0001"))
            {
                accountMember.MemberId = accountID;
                accountMember.MemberPassword = "@1";
                accountMember.MemberRole = 1;
            }
            return accountMember;
        }
    }
}
