using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MVVM_app.Behaviors
{
    public class EmailValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry); // устанавливается обработчик события TextChanged, который будет вызывать метод OnEntryTextChanged, когда текст в Entry изменяется
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry); // происходит очистка ресурсов и отмена подписки на событие TextChanged, чтобы избежать утечек памяти и проблем с производительностью
            entry.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Проведем валидацию формата электронной почты
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hot\.ee|yandex\.ru|tthk\.ee|mail\.ru)$";
            Regex regex = new Regex(emailPattern);

            if (!string.IsNullOrEmpty(e.NewTextValue) && !regex.IsMatch(e.NewTextValue))
            {
                ((Entry)sender).TextColor = Color.Red; // Устанавливаем цвет текста в красный при неверном формате
            }
            else
            {
                ((Entry)sender).TextColor = Color.Default; // Сбрасываем цвет текста, если формат верен
            }
        }
    }
}

