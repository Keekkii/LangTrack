using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using WpfApp2.Repositories;

namespace WpfApp2.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class PredavacView : UserControl
    {
        private bool isFlippedCard1 = false; // Track card state for Card 1
        private bool isFlippedCard2 = false; // Track card state for Card 2
        private bool isFlippedCard3 = false; // Track card state for Card 3
        public PredavacView()
        {
            InitializeComponent();
        }
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border clickedCard)
            {
                // Get the animation resource based on which card was clicked
                Storyboard? flipAnimation = clickedCard.Tag switch
                {
                    "Card 1" => (Storyboard?)clickedCard.Resources["FlipAnimationCard1"],
                    "Card 2" => (Storyboard?)clickedCard.Resources["FlipAnimationCard2"],
                    "Card 3" => (Storyboard?)clickedCard.Resources["FlipAnimationCard3"],
                    _ => null
                };

                if (flipAnimation != null)
                {
                    flipAnimation.Begin();
                }
            }
        }
        // Flip method for Card 1
        private void FlipHalfwayCard1(object sender, EventArgs e)
        {
            // Toggle visibility between front and back of Card 1
            FrontSide.Visibility = isFlippedCard1 ? Visibility.Visible : Visibility.Hidden;
            BackSide.Visibility = isFlippedCard1 ? Visibility.Hidden : Visibility.Visible;

            // Play second half of the flip animation for Card 1
            ScaleTransform scaleTransform = (ScaleTransform)FindName("Card1Scale");
            if (scaleTransform != null)
            {
                DoubleAnimation secondHalf = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, secondHalf);
            }

            isFlippedCard1 = !isFlippedCard1; // Toggle state
        }

        // Flip method for Card 2
        private void FlipHalfwayCard2(object sender, EventArgs e)
        {
            // Toggle visibility between front and back of Card 2
            FrontSide2.Visibility = isFlippedCard2 ? Visibility.Visible : Visibility.Hidden;
            BackSide2.Visibility = isFlippedCard2 ? Visibility.Hidden : Visibility.Visible;

            // Play second half of the flip animation for Card 2
            ScaleTransform scaleTransform = (ScaleTransform)FindName("Card2Scale");
            if (scaleTransform != null)
            {
                DoubleAnimation secondHalf = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, secondHalf);
            }

            isFlippedCard2 = !isFlippedCard2; // Toggle state
        }

        // Flip method for Card 3
        private void FlipHalfwayCard3(object sender, EventArgs e)
        {
            // Toggle visibility between front and back of Card 3
            FrontSide3.Visibility = isFlippedCard3 ? Visibility.Visible : Visibility.Hidden;
            BackSide3.Visibility = isFlippedCard3 ? Visibility.Hidden : Visibility.Visible;

            // Play second half of the flip animation for Card 3
            ScaleTransform scaleTransform = (ScaleTransform)FindName("Card3Scale");
            if (scaleTransform != null)
            {
                DoubleAnimation secondHalf = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, secondHalf);
            }

            isFlippedCard3 = !isFlippedCard3; // Toggle state
        }
        private void DodajPredavaca(object sender, RoutedEventArgs e)
        {
            // Get the values from the TextBoxes
            string idText = IdTextBox.Text;
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string course = CourseTextBox.Text;

            // Check if the ID only contains digits
            if (System.Text.RegularExpressions.Regex.IsMatch(idText, @"^\d+$"))
            {
                // Hide error message
                ErrorPoruka1.Visibility = Visibility.Collapsed;

                // Convert ID to integer
                int id = int.Parse(idText);

                // Create an instance of PolaznikRepository to interact with the database
                PredavacRepository predavacRepo = new PredavacRepository();

                // Check if the Polaznik already exists
                if (predavacRepo.IsPredavacExists(id))
                {
                    // If the Polaznik exists, show an error message
                    MessageBox.Show("Predavač sa ovim ID-om već postoji u bazi podataka!");
                }
                else
                {
                    // If the Polaznik doesn't exist, add it to the database
                    bool isInserted = predavacRepo.AddPredavacToDatabase(id, name, surname, course);

                    if (isInserted)
                    {
                        // Show success message
                        MessageBox.Show("Predavač je upisan u bazu podataka.");

                        // Optionally clear textboxes
                        NameTextBox.Clear();
                        SurnameTextBox.Clear();
                        IdTextBox.Clear();
                        CourseTextBox.Clear();
                    }
                    else
                    {
                        // Show error if insertion fails
                        MessageBox.Show("Greška pri unosu u bazu podataka.");
                    }
                }
            }
            else
            {
                // If the ID is invalid (not a number), show an error message
                ErrorPoruka1.Visibility = Visibility.Visible;
                ErrorPoruka1.Text = "ID mora sadržavati samo brojke";
            }
        }




        private void IzbrisiPredavaca(object sender, RoutedEventArgs e)
        {
            // Get the value from the ID TextBox
            string idText = IdTextBox1.Text;

            // Check if the ID only contains digits
            if (System.Text.RegularExpressions.Regex.IsMatch(idText, @"^\d+$"))
            {
                // If valid, hide the error message
                ErrorPoruka2.Visibility = Visibility.Collapsed;

                // Convert the ID to an integer
                int id = int.Parse(idText);

                // Create an instance of PolaznikRepository
                PredavacRepository predavacRepository = new PredavacRepository();

                // Call the method to delete the Polaznik
                bool isDeleted = predavacRepository.DeletePredavacById(id);

                if (isDeleted)
                {
                    MessageBox.Show("Predavač je izbrisan iz baze podataka.");
                }
                else
                {
                    MessageBox.Show("Predavač nije pronađen.");
                }

                // Optionally clear the textboxes
                NameTextBox1.Clear();
                SurnameTextBox1.Clear();
                IdTextBox1.Clear();
                CourseTextBox1.Clear();
            }
            else
            {
                // If invalid, show the error message
                ErrorPoruka2.Visibility = Visibility.Visible;
                ErrorPoruka2.Text = "ID mora sadržavati samo brojke";
            }
        }


        private void PromijeniPredavaca(object sender, RoutedEventArgs e)
        {
            // Get the value from the ID TextBox
            string idText = IdTextBox2.Text;

            // Check if the ID only contains digits
            if (System.Text.RegularExpressions.Regex.IsMatch(idText, @"^\d+$"))
            {
                // Convert the ID to an integer
                int id = int.Parse(idText);

                // Get the values from the other TextBoxes
                string name = NameTextBox2.Text;
                string surname = SurnameTextBox2.Text;
                string course = CourseTextBox2.Text;

                // Create an instance of PolaznikRepository
                PredavacRepository predavacRepository = new PredavacRepository();

                // Call the method to update the Polaznik
                bool isUpdated = predavacRepository.UpdatePredavac(id, name, surname, course);

                if (isUpdated)
                {
                    MessageBox.Show("Podaci su ažurirani.");
                }
                else
                {
                    MessageBox.Show("Predavač nije pronađen ili nije ažuriran.");
                }

                // Optionally clear the textboxes
                NameTextBox2.Clear();
                SurnameTextBox2.Clear();
                IdTextBox2.Clear();
                CourseTextBox2.Clear();
            }
            else
            {
                // If invalid, show the error message
                ErrorPoruka.Visibility = Visibility.Visible;
                ErrorPoruka.Text = "ID mora sadržavati samo brojke";
            }
        }
    }
}
