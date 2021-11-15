using MessageDrop.Core.Interface;
using MessageDrop.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace MessageDrop.Core.Service
{
    public class InMemoryMessageData : IMessageData
    {
        // This can be removed later 
        private int genID = 0;

        // MAKE SURE TO EDIT THIS PATH ON YOUR LOCAL MACHINE CONTEXT 
        private string RANDOM_WORDS_JSON = @"C:\Users\AccedeoPix\source\repos\MessageDrop.Web\MessageDrop.Core\StaticData\RandomWords.json";

        private Random rng = new Random();

        private string[] seedMessages = { "apple", "bannana", "salt", "pepper", "steak", "bacon" };

        readonly List<Message> _messages = new List<Message>();

        public InMemoryMessageData(bool isInitSeedData, [Optional] int numSeedData)
        {
            if (isInitSeedData)
            {
                _messages = GenerateSeedMessages(numSeedData);
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
        private List<Message> GenerateSeedMessages(int numSeedData)
        {
            // Get list of Random Words 
            var randomWords = getListOfDataFromJson(RANDOM_WORDS_JSON, "Words");

            // Empty list of messages
            List<Message> seedData = new List<Message>();


            // Populate messages in seedData
            for (int i = 0; i < numSeedData; i++)
            {
                int index = rng.Next(randomWords.Count);
                int id = i + 1;
                seedData.Add(new Message(id, randomWords[index]));
            }

            return seedData;
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            IEnumerable<Message> messages = new List<Message>();
            messages = _messages;
            return Task.FromResult(messages);
        }

        public Task<Message> Get(int id)
        {
            var messageById =
                from message in _messages
                where message.Id == id
                select message;

            // accountById returns IEnumerable meaning we have to cast it as a single Account 
            return Task.FromResult<Message>((Message)messageById);
        }
    }
}
