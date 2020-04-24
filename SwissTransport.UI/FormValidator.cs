using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SwissTransport.UI
{
    public class FormValidator
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z0-9]*$");

        public static bool FormComponentsValid(ComboBox[] comboBoxes = null, DateTimePicker[] dateTimePickers = null)
        {
            var comboBoxesValid = true;
            if (comboBoxes != null)
            {
                foreach (var comboBox in comboBoxes)
                {
                    if (string.IsNullOrEmpty(comboBox.Text))
                    {
                        comboBoxesValid = false;
                    }
                    else if (!Regex.IsMatch(comboBox.Text))
                    {
                        comboBoxesValid = false;
                    }
                }
            }

            var dateTimePickerValid = true;
            if (comboBoxes != null)
            {
                foreach (var dateTimePicker in dateTimePickers)
                {
                    if (string.IsNullOrEmpty(dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm")))
                    {
                        dateTimePickerValid = false;
                    }
                }
            }

            return comboBoxesValid && dateTimePickerValid;
        }
    }
}