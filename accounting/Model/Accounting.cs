namespace accounting.Model
{
    public class Accounting
    {
        public int Id { get; set; }

        public User User;

        public Device Device;

        public ConnectionPermission ConnectionPermission;

        public Accounting()
        {

        }
    }
}
