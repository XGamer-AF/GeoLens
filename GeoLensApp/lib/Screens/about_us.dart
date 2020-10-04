import 'package:auto_size_text/auto_size_text.dart';
import 'package:data_discovery_for_earth/Models/about_us_model.dart';
import 'package:data_discovery_for_earth/Services/data_loading_util.dart';
import 'package:data_discovery_for_earth/Services/network_util.dart';
import 'package:data_discovery_for_earth/localizations.dart';
import 'package:flutter/material.dart';

import '../theme.dart';
import 'CookiesClass.dart';

class AboutUs extends StatefulWidget {
  @override
  _AboutUsState createState() => _AboutUsState();
}

class _AboutUsState extends State<AboutUs> {
  String _lang = "";
  List<AboutUsModel> _data = [];
  Future<String> getLang() async {
    return await getValuesSF("lang");
  }

  _GetEventType() async {
    List<AboutUsModel> result = [];
    final query = "AAboutUs";
    var resultJson = await NetworkUtil.internal().get(query);
    if (resultJson == false || resultJson == 'noConnection') {
      setState(() {
        _data = [];
      });
    } else {
      for (var item in resultJson) {
        result.add(AboutUsModel.fromJson(item));
      }
      setState(() {
        _data = result;
      });
    }
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getLang().then((value) {
      setState(() {
        _lang = value;
      });
    });
    _GetEventType();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        centerTitle: true,
        title: Text(AppLocalizations.of(context).about_us),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Center(
            child: Column(
              children: [
                new Image.asset(
                  "assets/Images/logo.png",
                  width: 120,
                  height: 120,
                ),
                _data.length > 0
                    ? Padding(
                        padding: const EdgeInsets.all(3.0),
                        child: Container(
                            child: _lang == "ar"
                                ? Text(
                                    _data.first.aboutUsDescAr,
                                    textAlign: TextAlign.justify,
                                    style: TextStyle(height: 1.7),
                                  )
                                : Text(
                                    _data.first.aboutUsDescEn,
                                    textAlign: TextAlign.justify,
                                    style: TextStyle(height: 1.7),
                                  )),
                      )
                    : Padding(
                        padding: const EdgeInsets.only(top: 200),
                        child: DataLoadingUtil().LoadingData(),
                      ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
