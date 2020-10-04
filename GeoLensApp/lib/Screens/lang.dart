import 'package:data_discovery_for_earth/Screens/Intro.dart';
import 'package:flutter/material.dart';

import '../localizations.dart';
import '../theme.dart';
import 'CookiesClass.dart';
import 'data_change_lang.dart';

class Language extends StatefulWidget {
  @override
  _LanguageState createState() => _LanguageState();
}

class _LanguageState extends State<Language> {
  double _icon_size = 18;
  double _icon_space = 18;
  static const double _padding = 5.0;
  final _font_style = OwnColors.normalBlack();
  final _icon_color = OwnColors.color2[500];
  String _lang;
  Future<String> getLang() async {
    return await getValuesSF("lang");
  }

  Future<String> getNew() async {
    return await getValuesSF("isNew");
  }

  String _screen = "home";
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getNew().then((value) {
      if (value.isEmpty) {
        addStringToSF("isNew", "false");
        setState(() {
          _screen = "slider";
        });
      } else {
        setState(() {
          _screen = "home";
        });
      }
    });
    getLang().then((value) {
      setState(() {
        _lang = value;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: InkWell(
        onTap: () {
          if (_lang == "en") {
            addStringToSF("lang", "en");
            Navigator.of(context).pushReplacement(MaterialPageRoute(
                builder: (BuildContext context) =>
                    Intro(Data(lang: 'en', screen: _screen, sec2: 0))));
            //            Navigator.of(context).pushNamed('/intro',
            //                arguments: Data(lang: 'en', screen: _screen, sec2: 0));
          } else {
            addStringToSF("lang", "ar");
            Navigator.of(context).pushReplacement(MaterialPageRoute(
                builder: (BuildContext context) =>
                    Intro(Data(lang: 'ar', screen: _screen, sec2: 0))));
            //            Navigator.of(context).pushNamed('/intro',
            //                arguments: Data(lang: 'ar', screen: _screen, sec2: 0));
          }
        },
        child: Padding(
          padding: const EdgeInsets.all(0),
          child: Container(
            width: MediaQuery.of(context).size.width,
            color: OwnColors.color2[500],
            child: Padding(
              padding: const EdgeInsets.all(15.0),
              child: Text(
                AppLocalizations.of(context).change,
                style: TextStyle(color: OwnColors.color1[500]),
                textAlign: TextAlign.center,
              ),
            ),
          ),
        ),
      ),
      backgroundColor: OwnColors.color2[50],
      appBar: AppBar(
        centerTitle: true,
        title: Text(AppLocalizations.of(context).change_lang),
        leading: Container(),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: ListView(
          children: [
            Padding(
              padding: const EdgeInsets.only(bottom: 10),
              child: _group1(),
            ),
          ],
        ),
      ),
    );
  }

  Widget _group1() {
    return Padding(
      padding: const EdgeInsets.all(0.0),
      child: Container(
        child: Column(
          children: [
            Container(
              decoration: BoxDecoration(
                color: Colors.white,
                //   border: Border.all(color: Colors.grey, width: 0.1)
              ),
              child: Column(
                children: [
                  InkWell(
                    onTap: () {
                      setState(() {
                        _lang = "en";
                      });
                    },
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Padding(
                          padding: const EdgeInsets.only(right: 5, left: 5),
                          child: Row(
                            children: [
                              CircleAvatar(
                                backgroundColor: OwnColors.color2[500],
                                radius: 15.0,
                                child: Text(
                                  "EN",
                                  style:
                                      TextStyle(color: OwnColors.color1[500]),
                                ),
                              ),
                              SizedBox(
                                width: _icon_space,
                              ),
                              Text(
                                "English",
                                style: _font_style,
                              ),
                            ],
                          ),
                        ),
                        _lang == "en" ? Icon(Icons.done) : Container()
                      ],
                    ),
                  ),
                  Divider(),
                  InkWell(
                    onTap: () {
                      setState(() {
                        _lang = "ar";
                      });
                    },
                    child: Padding(
                      padding: const EdgeInsets.only(right: 5, left: 5),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Row(
                            children: [
                              CircleAvatar(
                                backgroundColor: OwnColors.color2[500],
                                radius: 15.0,
                                child: Text("AR",
                                    style: TextStyle(
                                        color: OwnColors.color1[500])),
                              ),
                              SizedBox(
                                width: _icon_space,
                              ),
                              Text(
                                "اللغة العربية",
                                style: _font_style,
                              ),
                            ],
                          ),
                          _lang == "ar" ? Icon(Icons.done) : Container()
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
