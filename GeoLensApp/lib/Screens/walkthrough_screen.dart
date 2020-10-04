import 'dart:io';
import 'package:data_discovery_for_earth/Screens/main_screen.dart';
import 'package:data_discovery_for_earth/localizations.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import 'CookiesClass.dart';

class SliderHome extends StatefulWidget {
  @override
  _SliderHomeState createState() => _SliderHomeState();
}

class _SliderHomeState extends State<SliderHome> {
  List<SliderModel> Slides = new List<SliderModel>();
  int CurrentIndex = 0;
  PageController PageConroller = new PageController(initialPage: 0);
  String _lang = '';
  Future<String> getLang() async {
    return await getValuesSF("lang");
  }

  @override
  void initState() {
    // TODO: implement initState
    getLang().then((value) {
      _lang = value;
    });
    super.initState();
    {
      Slides = GetSlides();
    }
  }

  @override
  void dispose() {
    // TODO: implement dispose
    super.dispose();
    PageConroller ?? PageConroller.dispose();
  }

  Widget PageIndexIndicator(bool IsCurrentPage) {
    return Container(
      margin: EdgeInsets.symmetric(horizontal: 5.0),
      width: IsCurrentPage ? 10.0 : 6.0,
      height: IsCurrentPage ? 10.0 : 6.0,
      decoration: BoxDecoration(
          color: IsCurrentPage ? Colors.grey : Colors.grey[300],
          borderRadius: BorderRadius.circular(12)),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: PageView.builder(
        controller: PageConroller,
        itemCount: Slides.length,
        onPageChanged: (index) {
          setState(() {
            CurrentIndex = index;
          });
        },
        itemBuilder: (context, index) {
          return SliderTile(
            imagePath: Slides[index].imagePath,
            titleEn: Slides[index].titleEn,
            titleAr: Slides[index].titleAr,
            descEn: Slides[index].descEn,
            descAr: Slides[index].descAr,
          );
        },
      ),
      bottomSheet: CurrentIndex != Slides.length - 1
          ? Container(
              height: Platform.isIOS ? 70 : 60,
              padding: EdgeInsets.symmetric(horizontal: 20),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  GestureDetector(
                      onTap: () {
                        PageConroller.animateToPage(Slides.length - 1,
                            duration: Duration(milliseconds: 300),
                            curve: Curves.linear);
                      },
                      child: Text(AppLocalizations.of(context).skip_label)),
                  Row(
                    children: [
                      for (int i = 0; i < Slides.length; i++)
                        CurrentIndex == i
                            ? PageIndexIndicator(true)
                            : PageIndexIndicator(false)
                    ],
                  ),
                  GestureDetector(
                      onTap: () {
                        PageConroller.animateToPage(CurrentIndex + 1,
                            duration: Duration(milliseconds: 300),
                            curve: Curves.linear);
                      },
                      child: Text(AppLocalizations.of(context).next_label))
                ],
              ),
            )
          : InkWell(
              onTap: () {
                Navigator.of(context).pushReplacement(MaterialPageRoute(
                    builder: (BuildContext context) => MainScreen()));
              },
              child: Container(
                height: Platform.isIOS ? 70 : 60,
                alignment: Alignment.center,
                color: Colors.blueAccent,
                width: MediaQuery.of(context).size.width,
                child: Text(
                  AppLocalizations.of(context).getStarted_label,
                  style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 18,
                      color: Colors.white),
                ),
              ),
            ),
    );
  }
}
/*------------------------------*/

class SliderTile extends StatefulWidget {
  String imagePath;
  String titleEn, titleAr, descEn, descAr;

  SliderTile(
      {this.imagePath, this.titleEn, this.titleAr, this.descEn, this.descAr});

  @override
  _SliderTileState createState() => _SliderTileState();
}

class _SliderTileState extends State<SliderTile> {
  String _lang = '';
  Future<String> getLang() async {
    return await getValuesSF("lang");
  }

  @override
  void initState() {
    // TODO: implement initState
    getLang().then((value) {
      setState(() {
        _lang = value;
      });
    });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 20),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Image.asset(
            widget.imagePath,
            width: 70,
            height: 70,
          ),
          SizedBox(
            height: 10,
          ),
          Text(
            _lang == 'en' ? widget.titleEn : widget.titleAr,
            style: TextStyle(
              fontWeight: FontWeight.w600,
              fontSize: 20,
            ),
          ),
          SizedBox(
            height: 10,
          ),
          Text(
            _lang == 'en' ? widget.descEn : widget.descAr,
            style: TextStyle(fontWeight: FontWeight.w400, fontSize: 18),
            textAlign: TextAlign.center,
          )
        ],
      ),
    );
  }
}

// Slider Data *****------------------------------------*********
class SliderModel {
  String imagePath;
  String titleEn;
  String titleAr;
  String descEn;
  String descAr;

  SliderModel(
      this.imagePath, this.titleEn, this.titleAr, this.descEn, this.descAr);

  void SetimagePath(String GetimagePath) {
    imagePath = GetimagePath;
  }

  void SettitleEn(String GettitleEn) {
    titleEn = GettitleEn;
  }

  void SettitleAr(String GettitleAr) {
    titleAr = GettitleAr;
  }

  void SetdescEn(String GetdescEn) {
    descEn = GetdescEn;
  }

  void SetdescAr(String GetdescAr) {
    descAr = GetdescAr;
  }

  String GetimagePath() {
    return imagePath;
  }

  String GettitleEn() {
    return titleEn;
  }

  String GettitleAr() {
    return titleAr;
  }

  String GetdescEn() {
    return descEn;
  }
}

List<SliderModel> GetSlides() {
  List<SliderModel> Slides = new List<SliderModel>();

  double _IconSize = 150;

  // --------**** 1st ***---------------------

  SliderModel SliderModel1 = new SliderModel(
      'assets/Images/Events.png',
      'Choose Event',
      'اختر الحدث',
      'Find the event you are interested in or want to study about',
      'ابحث عن حدث يهمك أو تريد دراسته');

  Slides.add(SliderModel1);

  // --------**** 2nd ***---------------------
  SliderModel SliderModel2 = new SliderModel(
      'assets/Images/Search_events.png',
      'Search Event',
      'ابحث عن الحدث',
      'Filter the events according to the event type or find it by its name',
      'رتب الأحداث طبقا لنوع الحدث أو ابحث عن الحدث بواسطة الاسم');

  Slides.add(SliderModel2);

  // --------**** 3rd ***---------------------
  SliderModel SliderModel3 = new SliderModel(
      'assets/Images/Extra_Links.png',
      'Use extra resources',
      'استخدم مصادر اكثر',
      'The app provides you with links for furthering your research on the events you desire',
      'البرنامج يتيح لك قائمة من الروابط للبحث اكثر عن الحدث و فهمه اكثر');

  Slides.add(SliderModel3);

  // ---------  4TH ---------------
  SliderModel SliderModel4 = new SliderModel(
      'assets/Images/allocate_events.png',
      'Allocate events',
      'حدد مكان الحدث ',
      'Find events near you and look up the location of the events occured',
      'ابحث عن الأحداث القريبة منك و تمكن من النظر لأماكن الأحداث التي حدثت بالفعل');

  Slides.add(SliderModel4);

  // ---------  5th ---------------
  SliderModel SliderModel5 = new SliderModel(
      'assets/Images/Save_Events.png',
      'Save events',
      'احفظ الحدث',
      'Star the events you liked and save it to read it later',
      'احفظ الأحداث التي اعجبتك للنظر إليها وقراءتها لاحقا');

  Slides.add(SliderModel5);
  return Slides;
}
