namespace Server.Units
{
    public class Shield
    {
        public string type;

        public Shield(string type)
        {
            this.type = type;
        }
        public void ChangeType (string type)
        {
            this.type = type;
        }
    }
}