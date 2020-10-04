import 'package:data_discovery_for_earth/localizations.dart';
import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/material.dart';

import '../theme.dart';
import 'CookiesClass.dart';

class TeamWork extends StatefulWidget {
  @override
  _TeamWorkState createState() => _TeamWorkState();
}

class _TeamWorkState extends State<TeamWork> {
  List<TeamModel> _data = [];
  String _lang = "";
  Future<String> getLang() async {
    return await getValuesSF("lang");
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
    setState(() {
      _data = GetTeam();
      print(_data.length);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          centerTitle: true,
          title: Text(AppLocalizations.of(context).team_work),
        ),
        body: _data.length > 0
            ? Padding(
                padding: const EdgeInsets.only(top: 6),
                child: ListView.builder(
                    itemCount: _data != null ? _data.length : 0,
                    itemBuilder: (BuildContext context, int index) {
                      return _listItem(index);
                    }),
              )
            : Container());
  }

  _listItem(index) {
    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.only(right: 5, left: 5),
          child: Container(
            decoration: BoxDecoration(
                color: OwnColors.color2[500],
                borderRadius: BorderRadius.circular(6.0)),
            child: Padding(
              padding: const EdgeInsets.all(4.0),
              child: Row(
                children: [
                  Container(
                      width: MediaQuery.of(context).size.width * 0.30 - 12,
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Padding(
                            padding: const EdgeInsets.only(right: 8, left: 8),
                            child: Container(
                              decoration: BoxDecoration(
                                  color: OwnColors.color1[500],
                                  borderRadius: BorderRadius.circular(40.0)),
                              child: Padding(
                                padding: const EdgeInsets.all(3.0),
                                child: ClipOval(
                                    child: Image.asset(
                                  _data[index].Image,
                                  width: 50,
                                  height: 50,
                                )),
                              ),
                            ),
                          )
                        ],
                      )),
                  Container(
                      width: MediaQuery.of(context).size.width * 0.70 - 12,
                      child: Padding(
                        padding: const EdgeInsets.all(3.0),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                              _lang == "ar"
                                  ? _data[index].DepartmentAr
                                  : _data[index].DepartmentEn,
                              style: OwnColors.bigWhite(),
                            ),
                            SizedBox(
                              height: 5,
                            ),
                            Text(
                                _lang == "ar"
                                    ? _data[index].NameAr
                                    : _data[index].NameEn,
                                style: OwnColors.bigWhite()),
                            SizedBox(
                              height: 5,
                            ),
                            Text(_data[index].Email,
                                style: OwnColors.bigWhite()),
                            SizedBox(
                              height: 5,
                            ),
                          ],
                        ),
                      ))
                ],
              ),
            ),
          ),
        ),
        SizedBox(
          height: 7,
        )
      ],
    );
  }
}

/*----------- DATAA --------- */
class TeamModel {
  String Image;
  String NameEn;
  String NameAr;
  String Email;
  String DepartmentEn;
  String DepartmentAr;

  TeamModel(this.Image, this.NameEn, this.NameAr, this.Email, this.DepartmentEn,
      this.DepartmentAr);

  void SetNameEn(String GetNameEn) {
    NameEn = GetNameEn;
  }

  void SetNameAr(String GetNameAr) {
    NameAr = GetNameAr;
  }

  void SetEmail(String GetEmail) {
    Email = GetEmail;
  }

  void SetDepartmentEn(String GetDepartmentEn) {
    DepartmentEn = GetDepartmentEn;
  }

  void SetDepartmentAr(String GetDepartmentAr) {
    DepartmentAr = GetDepartmentAr;
  }

  String GetNameEn() {
    return NameEn;
  }

  String GetNameAr() {
    return NameAr;
  }

  String GetEmail() {
    return Email;
  }

  String GetDepartmentEn() {
    return DepartmentEn;
  }

  String GetDepartmentAr() {
    return DepartmentAr;
  }
}

List<TeamModel> GetTeam() {
  List<TeamModel> TeamModels = new List<TeamModel>();

  double _IconSize = 150;
  // --------**** 1st ***---------------------

  TeamModel TeamModel1 = new TeamModel(
      'assets/Images/male.png',
      "Mohamed Adel Gomaa",
      'محمد عادل جمعة',
      'mohamed.adel36@msa.edu.eg',
      'Programming & Coding',
      'الكود ولغات البرمجة');

  TeamModels.add(TeamModel1);

  // --------**** 2nd ***---------------------
  TeamModel TeamModel2 = new TeamModel(
      "assets/Images/female.png",
      "Reem Ehab El-Tohamy",
      ' ريم إيهاب التهامي',
      'reemehab12385@students.mans.edu.eg',
      'Graphic designer',
      'مصممة جرافيكس');

  TeamModels.add(TeamModel2);

  // --------**** 3rd ***---------------------
  TeamModel TeamModel3 = new TeamModel(
      "assets/Images/female.png",
      "Yasmin ibrahim Alashkar",
      'ياسمين إبراهيم الأشقر',
      'yasmin.ibrahim@msa.edu.eg',
      'Data Entry',
      'مدخلة بيانات');

  TeamModels.add(TeamModel3);

  return TeamModels;
}
