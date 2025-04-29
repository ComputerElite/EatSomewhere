class PriceHelper {
  static String formatPriceWithUnit(int? price) {
    return '${formatPriceWithoutUnit(price)} €';
  }
  static String formatPriceWithoutUnit(int? price) {
    if (price == null) {
      return "null";
    }
    return (price / 100).toStringAsFixed(2);
  }
}

class DateHelper {
  static List<String> days = [
    "Sun",
    "Mon",
    "Tue",
    "Wed",
    "Thu",
    "Fri",
    "Sat",
    "Sun"
  ];
  static String formatDateTime(DateTime date) {
    return "${days[date.weekday]}, ${date.day.toString().padLeft(2, '0')}.${date.month.toString().padLeft(2, '0')}.${date.year} ${date.hour.toString().padLeft(2, '0')}:${date.minute.toString().padLeft(2, '0')}";
  }
}