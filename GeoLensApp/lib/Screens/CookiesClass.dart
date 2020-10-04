import 'dart:async';
import 'package:shared_preferences/shared_preferences.dart';

addStringToSF(String key, String value) async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  prefs.setString(key, value);
}

Future<String> getValuesSF(String key) async {
  String result = "";
  SharedPreferences prefs = await SharedPreferences.getInstance();
  bool CheckValue = prefs.containsKey(key);
  if (CheckValue) {
    return prefs.getString(key);
  }
  return result;

  //Return String
}
