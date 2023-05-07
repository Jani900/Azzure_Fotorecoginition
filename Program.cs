using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;


class Program
{
    // API to your Azure account 
    static string key = "877880b085f44457899a94eeb36a0aea";
    private static string endpoint =
        "https://image-recognition-cars.cognitiveservices.azure.com/";
  

    private static void Main(string[] args)
    {
        //Local hard coded images 

        List<string> imagePaths = new List<string>
        {
            @"/Users/janantolik/Desktop/IMG_2348.jpeg",
            @"/Users/janantolik/Desktop/2731a20e-d69c-4a59-8676-423e561ea841.jpg",
            @"/Users/janantolik/Desktop/IMG_2348.jpeg",

        };

        //client object
        var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials
            (key)){ Endpoint = endpoint };

        foreach (var imagePath in imagePaths)
        {   //imagePath is your api for your images either onwebiste or any path
            //where images are stored 
            AnalyzeImage(client, imagePath).Wait();

        };

        //to read the output
        Console.ReadLine();
    }

    private static async Task AnalyzeImage(ComputerVisionClient client, string
       imagePath)
    {
        var features = new List<VisualFeatureTypes?>()
        {
            // this is your image recognition training here you can add
            //many more params
            VisualFeatureTypes.Description,
            VisualFeatureTypes.Tags,
            VisualFeatureTypes.Categories,
            VisualFeatureTypes.Faces,
            
            
            
        };


        //If using local images must use stream for image path
        using (Stream stream = File.OpenRead(imagePath))
        {
            /*calling API AnalyzeImageAsync if for images online
             since I am using local images then you must use
            AnalyzeImageStream()*/

            var results = await client.AnalyzeImageInStreamAsync(stream,
                visualFeatures: features);

            Console.WriteLine("\nDescription");
            foreach (var caption in results.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} and confidence {caption.Confidence}");
            };

            Console.WriteLine("/nTags");
            foreach (var tag in results.Tags)
            {
                Console.WriteLine($"{tag.Name}");
            };

            Console.WriteLine("/nCategories");
            foreach (var cat in results.Categories)
            {
                Console.WriteLine($"{cat.Name} confidence {cat.Score}");
            };

            Console.WriteLine("/nFaces");
            foreach (var face in results.Faces)
            {
                Console.WriteLine(face.Age);
            };
        }
    }

}

