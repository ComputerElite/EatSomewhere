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