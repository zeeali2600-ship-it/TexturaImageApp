using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace TexturaAI.ConsoleTest
{
    class Program
    {
        private static int _freeGenerationsUsed = 0;
        private const int FREE_GENERATION_LIMIT = 3;
        private static bool _hasSubscription = false;
        private const string SETTINGS_FILE = "user_settings.json";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Textura AI - Console Test ===");
            Console.WriteLine("AI-Powered Text to Image Generator");
            Console.WriteLine();

            LoadUserSettings();

            while (true)
            {
                DisplayStatus();
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Generate Image");
                Console.WriteLine("2. Subscribe (Mock)");
                Console.WriteLine("3. Reset Trial (Testing)");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter your choice (1-4): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GenerateImage();
                        break;
                    case "2":
                        await MockSubscribe();
                        break;
                    case "3":
                        ResetTrial();
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using Textura AI!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void DisplayStatus()
        {
            int remainingFree = Math.Max(0, FREE_GENERATION_LIMIT - _freeGenerationsUsed);

            if (_hasSubscription)
            {
                Console.WriteLine("✓ Premium Subscription Active - Unlimited Generations");
            }
            else if (remainingFree > 0)
            {
                Console.WriteLine($"Free Generations Remaining: {remainingFree}");
            }
            else
            {
                Console.WriteLine("Free trial expired. Subscribe for unlimited generations!");
            }
        }

        private static async Task GenerateImage()
        {
            // Check if user can generate images
            if (!_hasSubscription && _freeGenerationsUsed >= FREE_GENERATION_LIMIT)
            {
                Console.WriteLine("❌ Free trial expired. Please subscribe for unlimited access.");
                return;
            }

            Console.Write("Enter your image description: ");
            var prompt = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(prompt))
            {
                Console.WriteLine("❌ Please enter a valid description.");
                return;
            }

            Console.WriteLine("🔄 Generating image...");
            
            // Simulate AI image generation
            await Task.Delay(2000);

            // Mock image generation result
            var hash = Math.Abs(prompt.GetHashCode());
            var colorCode = $"#{hash:X6}".Substring(0, 7);
            
            Console.WriteLine("✅ Image generated successfully!");
            Console.WriteLine($"📸 Generated a {colorCode} themed image for: \"{prompt}\"");
            Console.WriteLine($"🎨 Mock image URL: https://via.placeholder.com/512x512/{colorCode.Substring(1)}/FFFFFF?text={Uri.EscapeDataString(prompt.Substring(0, Math.Min(prompt.Length, 10)))}");

            // Increment free generation count if not subscribed
            if (!_hasSubscription)
            {
                _freeGenerationsUsed++;
                SaveUserSettings();
            }
        }

        private static async Task MockSubscribe()
        {
            if (_hasSubscription)
            {
                Console.WriteLine("✅ You already have an active subscription!");
                return;
            }

            Console.WriteLine("🛒 Opening Microsoft Store for subscription...");
            await Task.Delay(1500);

            Console.WriteLine("💳 Processing subscription...");
            await Task.Delay(2000);

            _hasSubscription = true;
            SaveUserSettings();

            Console.WriteLine("🎉 Subscription activated! You now have unlimited image generations.");
        }

        private static void ResetTrial()
        {
            _freeGenerationsUsed = 0;
            _hasSubscription = false;
            SaveUserSettings();
            Console.WriteLine("🔄 Trial reset for testing purposes.");
        }

        private static void LoadUserSettings()
        {
            try
            {
                if (File.Exists(SETTINGS_FILE))
                {
                    var json = File.ReadAllText(SETTINGS_FILE);
                    var settings = JsonSerializer.Deserialize<UserSettings>(json);
                    _freeGenerationsUsed = settings.FreeGenerationsUsed;
                    _hasSubscription = settings.HasSubscription;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error loading settings: {ex.Message}");
            }
        }

        private static void SaveUserSettings()
        {
            try
            {
                var settings = new UserSettings
                {
                    FreeGenerationsUsed = _freeGenerationsUsed,
                    HasSubscription = _hasSubscription
                };
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SETTINGS_FILE, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error saving settings: {ex.Message}");
            }
        }
    }

    public class UserSettings
    {
        public int FreeGenerationsUsed { get; set; }
        public bool HasSubscription { get; set; }
    }
}