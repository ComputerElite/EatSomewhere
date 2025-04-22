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
  static String formatDateTime(DateTime date) {
    return "${date.day.toString().padLeft(2, '0')}.${date.month.toString().padLeft(2, '0')}.${date.year} ${date.hour.toString().padLeft(2, '0')}:${date.minute.toString().padLeft(2, '0')}";
  }
}