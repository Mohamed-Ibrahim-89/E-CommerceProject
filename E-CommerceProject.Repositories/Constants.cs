namespace E_CommerceProject.Repositories;

public class Constants
{
    public struct DataTableParams
    {
        public const string Draw = "draw";

        public const string Search = "search[value]";

        public const string Start = "start";

        public const string Length = "length";

        public const string Order = "order[0][column]";

        public const string OrderDir = "order[0][dir]";

        public const string ExtraSearch = "ExtraSearch";
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
