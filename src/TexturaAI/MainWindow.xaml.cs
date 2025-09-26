using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace TexturaAI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private int _freeGenerationsUsed = 0;
        private const int FREE_GENERATION_LIMIT = 3;
        private bool _hasSubscription = false;

        public MainWindow()
        {
            this.InitializeComponent();
            LoadUserSettings();
        }

        private async void LoadUserSettings()
        {
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("FreeGenerationsUsed"))
                {
                    _freeGenerationsUsed = (int)localSettings.Values["FreeGenerationsUsed"];
                }
                if (localSettings.Values.ContainsKey("HasSubscription"))
                {
                    _hasSubscription = (bool)localSettings.Values["HasSubscription"];
                }
                
                UpdateUI();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error loading settings: {ex.Message}";
            }
        }

        private void SaveUserSettings()
        {
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["FreeGenerationsUsed"] = _freeGenerationsUsed;
                localSettings.Values["HasSubscription"] = _hasSubscription;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error saving settings: {ex.Message}";
            }
        }

        private void UpdateUI()
        {
            int remainingFree = Math.Max(0, FREE_GENERATION_LIMIT - _freeGenerationsUsed);
            
            if (_hasSubscription)
            {
                FreeCountTextBlock.Text = "✓ Premium Subscription Active - Unlimited Generations";
                GenerateButton.IsEnabled = true;
                SubscribeButton.Visibility = Visibility.Collapsed;
            }
            else if (remainingFree > 0)
            {
                FreeCountTextBlock.Text = $"Free Generations Remaining: {remainingFree}";
                GenerateButton.IsEnabled = true;
                SubscribeButton.Visibility = Visibility.Visible;
            }
            else
            {
                FreeCountTextBlock.Text = "Free trial expired. Subscribe for unlimited generations!";
                GenerateButton.IsEnabled = false;
                SubscribeButton.Visibility = Visibility.Visible;
            }
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PromptTextBox.Text))
            {
                StatusTextBlock.Text = "Please enter a text prompt for image generation.";
                return;
            }

            // Check if user can generate images
            if (!_hasSubscription && _freeGenerationsUsed >= FREE_GENERATION_LIMIT)
            {
                StatusTextBlock.Text = "Free trial expired. Please subscribe for unlimited access.";
                return;
            }

            GenerateButton.IsEnabled = false;
            StatusTextBlock.Text = "Generating image...";
            LoadingProgressRing.IsActive = true;

            try
            {
                // Simulate AI image generation (placeholder implementation)
                await GenerateImageAsync(PromptTextBox.Text);

                // Increment free generation count if not subscribed
                if (!_hasSubscription)
                {
                    _freeGenerationsUsed++;
                    SaveUserSettings();
                }

                UpdateUI();
                StatusTextBlock.Text = "Image generated successfully!";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error generating image: {ex.Message}";
            }
            finally
            {
                GenerateButton.IsEnabled = !(!_hasSubscription && _freeGenerationsUsed >= FREE_GENERATION_LIMIT);
                LoadingProgressRing.IsActive = false;
            }
        }

        private async Task GenerateImageAsync(string prompt)
        {
            // Placeholder implementation - in real app, this would call an AI API
            await Task.Delay(2000); // Simulate API call delay

            // For now, create a placeholder colored rectangle as the "generated" image
            var color = GetColorFromPrompt(prompt);
            var bitmap = new WriteableBitmap(512, 512);
            
            // Fill the bitmap with a solid color based on the prompt
            using (var stream = bitmap.PixelBuffer.AsStream())
            {
                var pixels = new byte[512 * 512 * 4]; // BGRA format
                var (b, g, r) = color;
                
                for (int i = 0; i < pixels.Length; i += 4)
                {
                    pixels[i] = b;     // Blue
                    pixels[i + 1] = g; // Green
                    pixels[i + 2] = r; // Red
                    pixels[i + 3] = 255; // Alpha
                }
                
                await stream.WriteAsync(pixels, 0, pixels.Length);
            }

            GeneratedImage.Source = bitmap;
        }

        private (byte b, byte g, byte r) GetColorFromPrompt(string prompt)
        {
            // Simple hash-based color generation from prompt
            var hash = prompt.GetHashCode();
            var r = (byte)((hash >> 16) & 0xFF);
            var g = (byte)((hash >> 8) & 0xFF);
            var b = (byte)(hash & 0xFF);
            
            // Ensure colors are not too dark
            r = (byte)Math.Max(r, 100);
            g = (byte)Math.Max(g, 100);
            b = (byte)Math.Max(b, 100);
            
            return (b, g, r);
        }

        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "Opening Microsoft Store for subscription...";
            
            try
            {
                // In a real app, this would open the Microsoft Store subscription page
                // For now, simulate the subscription process
                await Task.Delay(1000);
                
                // Mock subscription success
                _hasSubscription = true;
                SaveUserSettings();
                UpdateUI();
                StatusTextBlock.Text = "Subscription activated! You now have unlimited image generations.";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error processing subscription: {ex.Message}";
            }
        }

        private void ResetTrialButton_Click(object sender, RoutedEventArgs e)
        {
            // This is for testing purposes only - would not be in production app
            _freeGenerationsUsed = 0;
            _hasSubscription = false;
            SaveUserSettings();
            UpdateUI();
            StatusTextBlock.Text = "Trial reset for testing purposes.";
        }
    }
}