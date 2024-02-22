namespace Files.DummyFiles
{
    public abstract class DummyFile : File
    {

        protected DummyFile(string name) : base(name)
        {
        }

        public abstract void WriteDummy();

    }
}