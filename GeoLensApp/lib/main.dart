import 'package:flutter/material.dart';
import 'Screens/Intro.dart';
import 'Screens/cookies_class.dart';
import 'Screens/data_change_lang.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  WidgetsFlutterBinding.ensureInitialized();
  String _lang = "";
  String _screen = "";

  _lang = await getValuesSF("lang");
  // _customerID = await VaildLogin();

  if (_lang.isEmpty) {
    _lang = "en";
    _screen = "lang";
  } else {
    _screen = "home";
  }

  runApp(Intro(Data(lang: _lang, sec: 0, screen: _screen, sec2: 5)));
}
