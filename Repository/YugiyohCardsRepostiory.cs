namespace WpfApp2Test2.Repository
{
    public class YugiyohCardsRepostiory
    {

        private static YugiyohCardsRepostiory? _instance = null;

        public static YugiyohCardsRepostiory Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new();

                return _instance;
            }
        }

        private YugiyohCardsRepostiory()
        {
            //lade hier dinge :D
        }

        ~YugiyohCardsRepostiory()
        {
            //Dispose... lösche alles um speicher freizubekommen
        }
    }
}
