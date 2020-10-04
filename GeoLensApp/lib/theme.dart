import 'package:flutter/material.dart';

final ThemeData OwnThemeData = new ThemeData(
    brightness: Brightness.light,
    primarySwatch: OwnColors.color1[100],
    primaryColor: OwnColors.color1[500],
    primaryColorBrightness: Brightness.light,
    accentColor: OwnColors.color2[500],
    accentColorBrightness: Brightness.light,
    textSelectionColor: OwnColors.color2[300],
    appBarTheme: AppBarTheme(color: OwnColors.color1[500]),
    scaffoldBackgroundColor: OwnColors.color2[500],
    primaryTextTheme:
        TextTheme(title: TextStyle(color: OwnColors.color2[500])));

class OwnColors {
  OwnColors._(); // this basically makes it so you can instantiate this class
  static const Map<int, Color> color1 = const <int, Color>{
    50: const Color(0xFFfa9f44),
    100: const Color(0xFFf0e4d6),
    200: const Color(0xFFc86605),
    300: const Color(0xFFfa9f44),
    400: const Color(0xFFfa9f44),
    500: const Color(0xFFf0e4d6),
    600: const Color(0xFFfa9f44),
    700: const Color(0xFFfa9f44),
    800: const Color(0xFFfa9f44),
    900: const Color(0xFFffb9e45)
  };

  static const Map<int, Color> color2 = const <int, Color>{
    50: const Color(0xFFf7f7f7),
    100: const Color(0xFF314b5c),
    200:  const Color(0xFF6f7d8a),
    300: const Color(0xFF737271),
    400: const Color(0xFFfa9f44),
    500: const Color(0xFF314b5c),
    600: const Color(0xFF212529),
    700: const Color(0xFFfa9f44),
    800: const Color(0xFFfa9f44),
    900: const Color(0xFFf0e4d6)
  };

  static title() {
    return TextStyle(
        fontSize: 12.0,
        fontWeight: FontWeight.w600,
        color: color2[500],
        fontFamily: "Cairo");
  }

  static buttonCaption() {
    return TextStyle(
        fontSize: 12.0,
        fontWeight: FontWeight.w600,
        color: Colors.white,
        fontFamily: "Cairo");
  }

  static textBlack() {
    return TextStyle(
        fontSize: 12.0,
        fontWeight: FontWeight.w600,
        color: Colors.black,
        fontFamily: "Cairo");
  }

  static textWhite() {
    return TextStyle(
        fontSize: 12.0,
        fontWeight: FontWeight.w600,
        color: Colors.white,
        fontFamily: "Cairo");
  }

  static textColor() {
    return TextStyle(
        fontSize: 12.0,
        fontWeight: FontWeight.w600,
        color: color1[500],
        fontFamily: "Cairo");
  }

  static normalBlack() {
    return TextStyle(fontSize: 12.0, color: Colors.black, fontFamily: "Cairo");
  }

  static normalWhite() {
    return TextStyle(fontSize: 12.0, color: Colors.white, fontFamily: "Cairo");
  }

  static normalColor() {
    return TextStyle(fontSize: 12.0, color: color1[500], fontFamily: "Cairo");
  }

  static bigBlack() {
    return TextStyle(
        fontSize: 14.0,
        fontWeight: FontWeight.w600,
        color: Colors.black,
        fontFamily: "Cairo");
  }

  static bigWhite() {
    return TextStyle(
        fontSize: 14.0,
        fontWeight: FontWeight.w600,
        color: Colors.white,
        fontFamily: "Cairo");
  }

  static bigColor() {
    return TextStyle(
        fontSize: 14.0,
        fontWeight: FontWeight.w600,
        color: color1[500],
        fontFamily: "Cairo");
  }

  static smallBlack() {
    return TextStyle(fontSize: 10.0, color: Colors.black, fontFamily: "Cairo");
  }

  static smallWhite() {
    return TextStyle(fontSize: 10.0, color: Colors.white, fontFamily: "Cairo");
  }

  static smallColor() {
    return TextStyle(fontSize: 10.0, color: color1[500], fontFamily: "Cairo");
  }
}
