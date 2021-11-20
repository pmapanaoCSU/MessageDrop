using MessageDrop.Core.Interface;
using MessageDrop.EF;
using MessageDrop.EF.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace MessageDrop.API.Service
{
    public class DBMessageData : IMessageData
    {
        // This can be removed later 
        private int genID = 0;

        // MAKE SURE TO EDIT THIS PATH ON YOUR LOCAL MACHINE CONTEXT 
        private string RANDOM_WORDS_JSON = @"C:\Users\emarl\Source\Repos\pmapanaoCSU\MessageDrop\MessageDrop.Core\StaticData\RandomWords.json";

        private Random rng = new Random();

        // Circular dependency problem on getting a DB context 
        private readonly MessageDropDataContext _context;

        public DBMessageData(MessageDropDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
            GenerateSeedMessages(20);
        }

        public DBMessageData(bool isInitSeedData, [Optional] int numSeedData, MessageDropDataContext context)
        {
            if (isInitSeedData)
            {
                _context = context ?? throw new ArgumentNullException(nameof(_context));

                if (isInitSeedData) GenerateSeedMessages(numSeedData);
            }

        }

        // Get a list based on the key of JSON file 
        private List<string> getListOfDataFromJson(string pathToJson, string dataToExtract)
        {
            var jsonInMem = String.Empty;
            using (StreamReader r = new StreamReader(pathToJson))
            {
                jsonInMem = r.ReadToEnd();
            }

            JObject json = (JObject)JsonConvert.DeserializeObject(jsonInMem);
            var listObjFromJson = json.GetValue(dataToExtract).ToList();

            List<string> stringsFromJson = new List<string>();
            foreach (var obj in listObjFromJson)
            {
                stringsFromJson.Add(obj.ToString());
            }

            return stringsFromJson;

        }
        private void GenerateSeedMessages(int numSeedData)
        {
            // Get list of Random Words 
            var randomWords = getListOfDataFromJson(RANDOM_WORDS_JSON, "Words");


            // Populate messages in seedData
            for (int i = 0; i < numSeedData; i++)
            {
                int index = rng.Next(randomWords.Count);
                Message messageToAdd = new Message(randomWords[index]);

                _context.Add(messageToAdd);
            }

            this.Save();

        }

        public Task<IEnumerable<Message>> GetAll()
        {
            IEnumerable<Message> messages = _context.Messages.ToList();
            return Task.FromResult(messages);
        }

        public Task<Message> Get(int id)
        {
            var messageById =
                from message in _context.Messages
                where message.Id == id
                select message;

            Message messageFromContext = messageById.FirstOrDefault();

            return Task.FromResult<Message>(messageFromContext);
        }

        public Task<bool> Insert(Message message)
        {
            if (message == null)
            {
                return Task.FromResult(false);
            }

            Message messageToAdd = new Message(message.Id, message.MessageString);
            _context.Add(messageToAdd);
            return Task.FromResult(true);
        }

        public Task<bool> Save()
        {
            int numSaved = _context.SaveChanges();

            if (numSaved > 0) return Task.FromResult(true);
            else return Task.FromResult(false);
        }

    }
}
