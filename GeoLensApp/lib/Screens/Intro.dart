import 'package:data_discovery_for_earth/Screens/splash_screen.dart';
import 'package:data_discovery_for_earth/Services/route_generator.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import '../localizations.dart';
import '../theme.dart';
import 'cookies_class.dart';
import 'data_change_lang.dart';


class Intro extends StatefulWidget {
  final Data data;
  Intro(this.data);
  @override
  _IntroState createState() => _IntroState(data);

}

class _IntroState extends State<Intro> {
  String locale;
  final Data data;
  _IntroState(this.data);
  var introKey = new GlobalKey<ScaffoldState>();
  SpecificLocalizationDelegate _specificLocalizationDelegate;

  @override
  void initState() {
    super.initState();
    setState(() {
      locale = data.lang;
      setLang(locale);
    });
//
    this.setState(() {
      _specificLocalizationDelegate =
          SpecificLocalizationDelegate(new Locale(locale));
    });
  }

  setLang(String value) async {
    await addStringToSF("lang", value);
  }

  @override
  Widget build(BuildContext context) {
    return WillPopScope

      (child:new MaterialApp(
      debugShowCheckedModeBanner: false,

      key: introKey,
      title: 'GeoLens',
      localizationsDelegates: [
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        new FallbackCupertinoLocalisationsDelegate(),
        //app-specific localization
        _specificLocalizationDelegate
      ],
      theme: new ThemeData(
          appBarTheme: AppBarTheme(
            elevation: 0, // This removes the shadow from all App Bars.
          ),
          fontFamily: "Cairo",
          primaryColor: OwnColors.color2[500],
          textSelectionColor: OwnColors.color2[500],
          accentColor: OwnColors.color2[500],
          primaryTextTheme: TextTheme(
            headline6: TextStyle(color: OwnColors.color1[500],fontFamily: "Cairo", fontSize: 12,fontWeight: FontWeight.w600)
          )
      ),
      supportedLocales: [Locale('ar'), Locale('en')],
      locale: _specificLocalizationDelegate.overriddenLocale,
      initialRoute: '/',
      onGenerateRoute: RouteGenerator.generateRoute,
      home: SplashScreen(data),
    ),onWillPop: () async => true,);
  }
}


