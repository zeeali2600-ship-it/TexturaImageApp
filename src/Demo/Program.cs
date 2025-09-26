using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace TexturaAI.Demo
{
    class DemoProgram
    {
        private static int _freeGenerationsUsed = 0;
        private const int FREE_GENERATION_LIMIT = 3;
        private static bool _hasSubscription = false;

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Textura AI - Demo Workflow ===");
            Console.WriteLine("AI-Powered Text to Image Generator\n");

            // Simulate the complete user journey
            await DemoCompleteWorkflow();
        }

        private static async Task DemoCompleteWorkflow()
        {
            Console.WriteLine("🔄 Simulating complete user workflow...\n");

            // Initial state
            DisplayStatus();

            // Demo free generations
            Console.WriteLine("\n📝 Testing Free Generation #1:");
            await GenerateImageDemo("A beautiful sunset over mountains");
            
            Console.WriteLine("\n📝 Testing Free Generation #2:");
            await GenerateImageDemo("A colorful dragon in space");
            
            Console.WriteLine("\n📝 Testing Free Generation #3:");
            await GenerateImageDemo("A peaceful garden with flowers");

            DisplayStatus();

            // Try to generate when limit reached
            Console.WriteLine("\n❌ Attempting generation after free limit:");
            await GenerateImageDemo("This should fail");

            // Subscribe
            Console.WriteLine("\n💳 Simulating subscription purchase...");
            await MockSubscribe();

            DisplayStatus();

            // Test unlimited access
            Console.WriteLine("\n✅ Testing unlimited access:");
            await GenerateImageDemo("A futuristic city at night");
            await GenerateImageDemo("A magical forest with unicorns");

            Console.WriteLine("\n🎉 Demo completed successfully!");
            Console.WriteLine("All features working: Free trial → Subscription → Unlimited access");
        }

        private static void DisplayStatus()
        {
            int remainingFree = Math.Max(0, FREE_GENERATION_LIMIT - _freeGenerationsUsed);

            if (_hasSubscription)
            {
                Console.WriteLine("📊 Status: ✅ Premium Subscription Active - Unlimited Generations");
            }
            else if (remainingFree > 0)
            {
                Console.WriteLine($"📊 Status: Free Generations Remaining: {remainingFree}");
            }
            else
            {
                Console.WriteLine("📊 Status: ❌ Free trial expired. Subscription required!");
            }
        }

        private static async Task GenerateImageDemo(string prompt)
        {
            // Check if user can generate images
            if (!_hasSubscription && _freeGenerationsUsed >= FREE_GENERATION_LIMIT)
            {
                Console.WriteLine($"   ❌ Generation failed: Free trial expired");
                return;
            }

            Console.WriteLine($"   🔄 Generating: \"{prompt}\"");
            
            // Simulate AI image generation
            await Task.Delay(1000);

            // Mock image generation result
            var hash = Math.Abs(prompt.GetHashCode());
            var colorCode = $"#{hash:X6}".Substring(0, 7);
            
            Console.WriteLine($"   ✅ Success! Generated {colorCode} themed image");

            // Increment free generation count if not subscribed
            if (!_hasSubscription)
            {
                _freeGenerationsUsed++;
                Console.WriteLine($"   📈 Free generations used: {_freeGenerationsUsed}/{FREE_GENERATION_LIMIT}");
            }
            else
            {
                Console.WriteLine($"   ♾️  Unlimited subscription active");
            }
        }

        private static async Task MockSubscribe()
        {
            Console.WriteLine("   🛒 Opening Microsoft Store...");
            await Task.Delay(1000);

            Console.WriteLine("   💳 Processing subscription...");
            await Task.Delay(1500);

            _hasSubscription = true;
            Console.WriteLine("   🎉 Subscription activated successfully!");
        }
    }
}